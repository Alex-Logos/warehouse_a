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
    /// CBN to print product label or logistics label
    /// </summary>
    class PrintLabelsCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(PrintLabelsCbm));

        /// <summary>
        /// Instantiate CBM to generate labels
        /// </summary>
        private readonly CbmController printProductLabelCbm = new PrintProductLabelCbm();

        /// <summary>
        /// Instantiate CBM to print labels
        /// </summary>
        private readonly CbmController printInternalLogisticsLabelCbm  = new PrintInternalLogisticsLabelCbm();

        /// <summary>
        /// Print product label or logistics label
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            ValueObjectList<ValueObject> inVo = vo as ValueObjectList<ValueObject>;

            List<ValueObject> labels = inVo?.GetList();

            if (inVo == null || labels == null || labels.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(PrintLabelsCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // Print product label or logistics label

            int productLabelSetCount = 0;
            int productLabelQuantityTotal = 0;
            List<string> productLabelAttachedDocumentWorkOrders = new List<string>();

            int logisticsLabelSetCount = 0;
            int logisticsLabelQuantityTotal = 0;
            List<string> logisticsLabelAttachedDocumentWorkOrders = new List<string>();

            foreach (ValueObject label in labels)
            {
                if (label is ProductLabelVo)
                {
                    ProductLabelVo productLabel = (ProductLabelVo)label;

                    ResultVo printResult = printProductLabelCbm.Execute(trxContext, productLabel) as ResultVo;

                    if (printResult == null || printResult.AffectedCount <= 0)
                    {
                        var messageData = new MessageData("zwce00029", Properties.Resources.zwce00029, nameof(printProductLabelCbm));
                        logger.Error(messageData);
                        //throw new Framework.ApplicationException(messageData);
                    }

                    productLabelSetCount++;
                    productLabelQuantityTotal += printResult.AffectedCount;
                    productLabelAttachedDocumentWorkOrders.Add(productLabel.AttachedDocumentControlNumber + "-" + productLabel.WorkOrderNumber);
                }
                else if (label is InternalLogisticsLabelVo)
                {
                    InternalLogisticsLabelVo logisticsLabel = (InternalLogisticsLabelVo)label;

                    ResultVo printResult = printInternalLogisticsLabelCbm.Execute(trxContext, logisticsLabel) as ResultVo;

                    if (labels == null || labels.Count <= 0)
                    {
                        var messageData = new MessageData("zwce00029", Properties.Resources.zwce00029, nameof(printProductLabelCbm));
                        logger.Error(messageData);
                        //throw new Framework.ApplicationException(messageData);
                    }

                    logisticsLabelSetCount++;
                    logisticsLabelQuantityTotal += printResult.AffectedCount;
                    logisticsLabelAttachedDocumentWorkOrders.Add(logisticsLabel.AttachedDocumentControlNumber + "-" + logisticsLabel.WorkOrderNumber);
                }
            }

            PrintLabelsResultVo outVo = new PrintLabelsResultVo();
            outVo.ProductLabelSetCount = productLabelSetCount;
            outVo.ProductLabelQuantityTotal = productLabelQuantityTotal;
            outVo.ProductLabelAttachedDocumentWorkOrders = productLabelAttachedDocumentWorkOrders.Distinct().ToList();
            outVo.InternalLogisticsLabelSetCount = logisticsLabelSetCount;
            outVo.InternalLogisticsLabelQuantityTotal = logisticsLabelQuantityTotal;
            outVo.InternalLogisticsLabelAttachedDocumentWorkOrders = logisticsLabelAttachedDocumentWorkOrders.Distinct().ToList();

            return outVo;

        }
    }
}
