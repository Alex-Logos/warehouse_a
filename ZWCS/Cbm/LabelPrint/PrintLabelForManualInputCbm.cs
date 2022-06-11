using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to generate label objects then send print instruction to label printer
    /// </summary>
    class PrintLabelForManualInputCbm : CbmController
    {
        /// <summary>
        /// Instantiate logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(PrintLabelForManualInputCbm));

        /// <summary>
        /// Instantiate CBM to generate labels
        /// </summary>
        private readonly CbmController printProductLabelCbm = new PrintProductLabelCbm();

        /// <summary>
        /// Instantiate CBM to print labels
        /// </summary>
        private readonly CbmController printInternalLogisticsLabelCbm = new PrintInternalLogisticsLabelCbm();

        /// <summary>
        /// Generate label then send print instruction to label printer
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            PrintLabelForManualInputVo inVo = vo as PrintLabelForManualInputVo;

            if (inVo == null || string.IsNullOrWhiteSpace(inVo.ItemNumber) || string.IsNullOrWhiteSpace(inVo.LotNumber) || inVo.LabelFieldsVo == null)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(PrintLabelForManualInputCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 2. Generate label value objects

            ItemMasterLabelFieldsVo labelInfo = inVo.LabelFieldsVo; ;

            if (labelInfo.LabelType == 1)
            {
                ProductLabelVo label = GenerateProductLabel(inVo, labelInfo);

                ResultVo printResult = printProductLabelCbm.Execute(trxContext, label) as ResultVo;

                if (printResult == null || printResult.AffectedCount <= 0)
                {
                    var messageData = new MessageData("zwce00029", Properties.Resources.zwce00029, nameof(printProductLabelCbm));
                    logger.Error(messageData);
                    //throw new Framework.ApplicationException(messageData);
                }

                return printResult;
            }
            else if (labelInfo.LabelType == 2)
            {
                InternalLogisticsLabelVo label = GenerateInternalLogisticsLabel(inVo, labelInfo);

                ResultVo printResult = printInternalLogisticsLabelCbm.Execute(trxContext, label) as ResultVo;

                if (printResult == null || printResult.AffectedCount <= 0)
                {
                    var messageData = new MessageData("zwce00029", Properties.Resources.zwce00029, nameof(printProductLabelCbm));
                    logger.Error(messageData);
                    //throw new Framework.ApplicationException(messageData);
                }

                return printResult;
            }
            else
            {
                var messageData = new MessageData("zwce00026", Properties.Resources.zwce00026, nameof(labelInfo.LabelType), labelInfo.LabelType.ToString());
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ProductLabelVo GenerateProductLabel(PrintLabelForManualInputVo userInput, ItemMasterLabelFieldsVo master)
        {
            ProductLabelVo label = new ProductLabelVo();

            label.LotNumber = userInput.LotNumber;
            label.ExpirationDate = userInput.ExpirationDate;
            label.LabelQunaity = userInput.LabelQuantity;

            label.ProductCategory = master.ProductCategory;
            label.ProductName = master.ProductName;
            label.RegulatoryApprovalNumber = master.RegulatoryApprovalNumber;
            label.JmdnNumber = master.JmdnNumber;
            label.ClassCategory = master.ClassCategory;
            label.ReuseCategory = master.ReuseCategory;
            label.ControlCategory = master.ControlCategory;
            label.SterilizationCategory = master.SterilizationCategory;
            label.ManufacturerName = master.ManufacturerName;
            label.JanNumber = master.JanNumber;

            label.ItemNumber = master.ItemNumber;

            bool legacyItemNecessary = master.LegacyItemNumber != null && master.LegacyItemNumberDisplayNecessary;
            label.ItemNumberWithLegacyItemNumber = legacyItemNecessary ? master.ItemNumber + "(" + master.LegacyItemNumber + ")" : master.ItemNumber;

            return label;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InternalLogisticsLabelVo GenerateInternalLogisticsLabel(PrintLabelForManualInputVo userInput, ItemMasterLabelFieldsVo master)
        {

            InternalLogisticsLabelVo label = new InternalLogisticsLabelVo();

            label.LotNumber = userInput.LotNumber;
            label.ExpirationDate = userInput.ExpirationDate;
            label.LabelQunaity = userInput.LabelQuantity;
            label.ProductName = master.ProductName;

            label.ItemNumber = master.ItemNumber;

            bool legacyItemNecessary = master.LegacyItemNumber != null && master.LegacyItemNumberDisplayNecessary;
            label.ItemNumberWithLegacyItemNumber = legacyItemNecessary ? master.ItemNumber + "(" + master.LegacyItemNumber + ")" : master.ItemNumber;

            return label;

        }
    }
}
