using System.Collections.Generic;
using System.Windows.Forms;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Linq;
using SATO.MLComponent;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBN to print product label
    /// </summary>
    class PrintProductLabelCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(PrintProductLabelCbm));

        //private readonly string protcolAndPrinterName = "DRV:SATO CL4NX-J Plus 609dpi";

        private readonly string protcolAndPrinterName = "DRV:SATO CL4NX-J Plus 609dpi Tokyo Medical Center";

        private readonly int timeoutSeconds = 3;

        private readonly string folderPath = Application.StartupPath + "\\" + "Common";

        private readonly string layoutFileName = "JptProductLabelLayout.mllayx";

        /// <summary>
        /// Print product label
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            ProductLabelVo inVo = vo as ProductLabelVo;

            if (inVo == null)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(PrintProductLabelCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            MLComponent mLComponent = new MLComponent();
            mLComponent.Setting = protcolAndPrinterName;
            mLComponent.Timeout = timeoutSeconds;
            mLComponent.HeaderTailSetting = true;
            mLComponent.LayoutFile = folderPath + "\\" + layoutFileName;

            // Open port
            int openPortResult = mLComponent.OpenPort(1);

            if (openPortResult != 0)
            {
                var messageData = new MessageData("zwce00031", Properties.Resources.zwce00031, nameof(PrintProductLabelCbm), openPortResult.ToString());
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            // Set print data and execute output
            List<string> values = new List<string>();
            values.Add(inVo.ProductName);
            values.Add(inVo.ProductCategory);
            values.Add(inVo.ClassCategory);
            values.Add(inVo.ReuseCategory);
            values.Add(inVo.RegulatoryApprovalNumber);
            values.Add(inVo.ItemNumber);
            values.Add(inVo.LotNumber);
            values.Add(inVo.JmdnNumber);

            bool dateEmpty = inVo.ExpirationDate == DateTime.MinValue;
            string expirationDate = dateEmpty ? string.Empty : inVo.ExpirationDate.ToString("yyMMdd");
            values.Add(expirationDate);

            values.Add(inVo.ControlCategory);
            values.Add(inVo.SterilizationCategory);
            values.Add(inVo.ManufacturerName);
            values.Add(inVo.JanNumber);

            string expirationDateDisplay = dateEmpty ? string.Empty : inVo.ExpirationDate.ToString("yy/MM/dd");
            values.Add(expirationDateDisplay);

            values.Add(inVo.ItemNumberWithLegacyItemNumber);

            values.Add(inVo.LabelQunaity.ToString());

            mLComponent.PrnData = string.Join("\t", values);

            // Set printer label cut option as cut at the end of the printing quantity
            mLComponent.MultiCut = inVo.LabelQunaity;

            // Send print instruction
            int printResult = mLComponent.Output();

            if (printResult != 0)
            {
                mLComponent.ClosePort();

                var messageData = new MessageData("zwce00032", Properties.Resources.zwce00032, nameof(PrintProductLabelCbm), printResult.ToString());
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            Application.DoEvents();

            mLComponent.ClosePort();

            ResultVo outVo = new ResultVo();
            outVo.AffectedCount = inVo.LabelQunaity;

            return outVo;

        }
    }
}
