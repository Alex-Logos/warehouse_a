using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Linq;

using System.Runtime.InteropServices;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBN to generate excel file and reflect values for single work order
    /// </summary>
    class OutputWorkOrderFileCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(OutputWorkOrderFileCbm));

        /// <summary>
        /// 1. Copy template excel file with new file name
        /// 2. Open copied excel workbook
        /// 3. Set header sheet values
        /// 4. Copy the line sheet to match the number of lines then set values for each sheet
        /// 5. Save workbook and close application
        /// 6. Return file name and directory as value object
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            WorkOrderVo inVo = vo as WorkOrderVo;

            WorkOrderHeaderVo headerValues = inVo?.Header;

            List<WorkOrderLineVo> lineValues = inVo?.Lines;

            if (inVo == null || headerValues == null || lineValues == null || lineValues.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(OutputWorkOrderFileCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 1. Copy template excel file with new file name

            string newFileNameForUser = GetWorkOrderNumberWithSubNumber(headerValues, lineValues) + ".xlsm";

            string newFileDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string newFileName = Path.Combine(newFileDirectory, newFileNameForUser);

            string appFileDirectory = System.Windows.Forms.Application.StartupPath;

            string templateFileName = Path.Combine(appFileDirectory, "Common", "WorkOrderExcelFormat.xlsm");

            File.Copy(templateFileName, newFileName, true);


            // 2. Open copied excel workbook

            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            application.Visible = true;

            _Workbook newWorkBook = application.Workbooks.Open(newFileName, 0, false, 5, "", "", false,
                XlPlatform.xlWindows, "", true, false, 0, true, false, false);

            Sheets sheets = newWorkBook.Worksheets;


            // 3. Set header sheet values

            _Worksheet headerSheet = (Worksheet)sheets.get_Item("表紙");

            SetHeaderSheetValues(ref headerSheet, headerValues, lineValues);


            // 4. Copy the line sheet to match the number of lines then set values for each sheet

            List<_Worksheet> lineSheets = CopyLineSheetToMatchWorkOrderPageCount(ref newWorkBook, "P01", "P", lineValues.Max(l => l.PageWithinWorkOrderSubNumber));

            SetLineSheetValues(ref lineSheets, headerValues, lineValues);


            // 5. Send print instruction to printer, then save workbook and close application

            newWorkBook.PrintOut(Preview: false, Collate: true);

            application.Visible = false;
            application.UserControl = false;
            newWorkBook.Save();
            newWorkBook.Close();
            application.Quit();


            // 6. Return file name and directory as value object

            WorkOrderOutputVo outVo = new WorkOrderOutputVo();
            outVo.FileName = newFileNameForUser;
            outVo.Directory = newFileDirectory;

            return outVo;
        }


        /// <summary>
        /// Concatenate work order number and work order sub number
        /// </summary>
        /// <param name="headerValues"></param>
        /// <param name="lineValues"></param>
        /// <returns></returns>
        private string GetWorkOrderNumberWithSubNumber(WorkOrderHeaderVo headerValues, List<WorkOrderLineVo> lineValues)
        {
            int workOrderSubNumber = lineValues.First().WorkOrderSubNumber;

            if (workOrderSubNumber != 1)
            {
                return "WO" + headerValues.WorkOrderNumber + "-" + workOrderSubNumber.ToString();
            }
            else
            {
                return "WO" + headerValues.WorkOrderNumber;
            }
        }



        /// <summary>
        /// Set header sheet values
        /// </summary>
        /// <param name="headerSheet"></param>
        /// <param name="header"></param>
        /// <param name="lines"></param>
        private void SetHeaderSheetValues(ref _Worksheet headerSheet, WorkOrderHeaderVo header, List<WorkOrderLineVo> lines)
        {
            headerSheet.get_Range("hWorkOrderNumber").Value = GetWorkOrderNumberWithSubNumber(header, lines);

            headerSheet.get_Range("hShippingNoticeTrackingNumber").Value = header.ShippingNoticeTrackingNumber;

            headerSheet.get_Range("hPurachaseOrderNumber").Value = string.Join(", ", header.PurchaseOrderNumber);

            headerSheet.get_Range("hCommercialInvoiceNumber").Value = string.Join(", ", header.CommercialInvoiceNumber);


            List<string> productCategories = lines.Select(l => l.ProductCategory).Distinct().ToList();

            headerSheet.get_Range("hProductCategory").Value = string.Join(", ", productCategories);


            headerSheet.get_Range("hWorkOrderLineCount").Value = lines.Count;

            headerSheet.get_Range("hWorkOrderProductQuantityTotal").Value = lines.Sum(l => l.LotQuantity);


            if (!string.IsNullOrWhiteSpace(header.PackingMaterial1))
            {
                headerSheet.get_Range("hPackingMaterial1").Value = header.PackingMaterial1;

                headerSheet.get_Range("hWorkOrderPackingMaterial1QuantityTotal").Value = lines.Sum(l => l.LotQuantity);
            }


            if (lines.Any(l => !string.IsNullOrWhiteSpace(l.PackingMaterial2)))
            {
                List<WorkOrderLineVo> targetLines = lines.Where(l => !string.IsNullOrWhiteSpace(l.PackingMaterial2)).ToList();

                List<string> pakingMaterials2 = targetLines.Select(l => l.PackingMaterial2).Distinct().ToList();

                headerSheet.get_Range("hPackingMaterial2").Value = string.Join(", ", pakingMaterials2);

                headerSheet.get_Range("hWorkOrderPackingMaterial2QuantityTotal").Value = targetLines.Sum(l => l.LotQuantity);
            }


            List<string> additionalInstructions = lines.Select(l => l.AdditionalWorkInstruction).Distinct().ToList();

            headerSheet.get_Range("hStandardAndAdditionalWorkInstruction").Value = 
                header.StandardWorkInstruction + Environment.NewLine + string.Join(", ", additionalInstructions);
        }


        /// <summary>
        /// Copy the line sheet to match the number of lines
        /// </summary>
        /// <param name="workBook"></param>
        /// <param name="lineSheetName"></param>
        /// <param name="sheetNamePrefix"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        private List<_Worksheet> CopyLineSheetToMatchWorkOrderPageCount(ref _Workbook workBook, string lineSheetName, string sheetNamePrefix, int pageCount)
        {
            Sheets sheets = workBook.Worksheets;

            _Worksheet originalSheet = sheets.get_Item(lineSheetName) as Worksheet;

            List<_Worksheet> lineSheets = new List<_Worksheet>();

            lineSheets.Add(originalSheet);


            for (int i = 0; i < pageCount - 1; i++)
            {
                originalSheet.Copy(Type.Missing, sheets[sheets.Count]);

                _Worksheet copiedSheet = sheets[sheets.Count];

                copiedSheet.Name = sheetNamePrefix + (i + 2).ToString("00");

                lineSheets.Add(copiedSheet);
            }

            return lineSheets;
        }


        /// <summary>
        /// Set line values for each sheet
        /// </summary>
        /// <param name="lineSheets"></param>
        /// <param name="header"></param>
        /// <param name="linesInOrder"></param>
        private void SetLineSheetValues(ref List<_Worksheet> lineSheets, WorkOrderHeaderVo header, List<WorkOrderLineVo> linesInOrder)
        {
            int sheetCount = lineSheets.Count;

            int pageInOrder = linesInOrder.Max(l => l.PageWithinWorkOrderSubNumber);

            if (sheetCount != pageInOrder)
            {
                var messageData = new MessageData("zwce00020", Properties.Resources.zwce00020, nameof(OutputWorkOrderFileCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            foreach (_Worksheet sheet in lineSheets)
            {
                //Set values in header space

                int sheetIndex = lineSheets.IndexOf(sheet) + 1;

                string workOrderNumberWithSubNumber = GetWorkOrderNumberWithSubNumber(header, linesInOrder);

                sheet.get_Range("dWorkOrderNumber").Value = workOrderNumberWithSubNumber; 

                sheet.get_Range("dPageAndPageTotal").Value = "'" + sheetIndex.ToString() + "/" + sheetCount.ToString();


                //Set values in line space

                List<WorkOrderLineVo> linesInPage = linesInOrder.Where(l => l.PageWithinWorkOrderSubNumber == sheetIndex).OrderBy(l => l.SerialWithinWorkOrderSubNumber).ToList();

                foreach (WorkOrderLineVo line in linesInPage)
                {
                    string lineIndex = (linesInPage.IndexOf(line) + 1).ToString();

                    sheet.get_Range("d" + lineIndex + "ItemDescription").Value = line.ItemDescriptionJapanese;

                    sheet.get_Range("d" + lineIndex + "ItemNumber").Value = "'" + line.ItemNumber;

                    sheet.get_Range("d" + lineIndex + "SupplierItemNumber").Value = "'" + line.SupplierItemNumber;

                    sheet.get_Range("d" + lineIndex + "LotNumber").Value = "'" + line.LotNumber;


                    bool expirationDateEmpty = line.LotExpirationDate == DateTime.MinValue;

                    if (!expirationDateEmpty)
                    {
                        sheet.get_Range("d" + lineIndex + "ExpirationDate").Value = line.LotExpirationDate;
                    }

                    sheet.get_Range("d" + lineIndex + "Quantity").Value = line.LotQuantity;

                    sheet.get_Range("d" + lineIndex + "PackingMaterial1").Value = header.PackingMaterial1;

                    sheet.get_Range("d" + lineIndex + "PackingMaterial2").Value = line.PackingMaterial2;

                    sheet.get_Range("d" + lineIndex + "StandardWorkInstruction").Value = header.StandardWorkInstruction;

                    sheet.get_Range("d" + lineIndex + "AdditionalWorkInstruction").Value = line.AdditionalWorkInstruction;

                    sheet.get_Range("d" + lineIndex + "ProductLabelPrintingQuantity").Value = line.LabelType != 0 ? line.LotQuantity + 1 : 0;


                    string gs1128 = expirationDateEmpty ?
                        "=CODE128(\"" + "01" + line.JanNumber + "10" + line.LotNumber + "\")" :
                        "=CODE128(\"" + "01" + line.JanNumber + "17" + line.LotExpirationDate.ToString("yyMMdd") + "10" + line.LotNumber + "\")";

                    sheet.get_Range("d" + lineIndex + "GS1128").Value = gs1128;

                    
                    sheet.get_Range("d" + lineIndex + "ItemNumberBarcode").Value = "=CODE128(\"" + line.ItemNumber + "\")";

                    sheet.get_Range("d" + lineIndex + "LotNumberBarcode").Value = "=CODE128(\"" + line.LotNumber + "\")";

                    sheet.get_Range("d" + lineIndex + "SerialWithinWorkOrderSubNumber").Value = "明細" + line.SerialWithinWorkOrderSubNumber.ToString();


                    // Work varification form picks up work order number as 16 digits, thus it is padded with spaces as below

                    sheet.get_Range("d" + lineIndex + "WorkOrderNumberAndSerialAndProductQuantity").Value 
                        = "=CODE128(\"" + workOrderNumberWithSubNumber.PadRight(16) + line.SerialWithinWorkOrderSubNumber.ToString("000") + line.LotQuantity.ToString("000000") + "\")";
                }

                // Clear excel line areas not having corresponding values

                if (linesInPage.Count <= 2)
                {
                    sheet.get_Range("d3Area").Cells.Clear();

                    if (linesInPage.Count == 1)
                    {
                        sheet.get_Range("d2Area").Cells.Clear();
                    }
                }

            }

        }

    }
}
