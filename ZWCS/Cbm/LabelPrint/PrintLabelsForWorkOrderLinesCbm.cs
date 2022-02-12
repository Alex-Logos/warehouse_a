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
    class PrintLabelsForWorkOrderLinesCbm : CbmController
    {
        /// <summary>
        /// Instantiate logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(PrintLabelsForWorkOrderLinesCbm));

        /// <summary>
        /// Instantiate CBM to generate labels
        /// </summary>
        private readonly CbmController generateLabelsFromWorkOrderLinesCbm = new GenerateLabelsFromWorkOrderLinesCbm();

        /// <summary>
        /// Instantiate CBM to print labels
        /// </summary>
        private readonly CbmController printLabelCbm = new PrintLabelsCbm();


        /// <summary>
        /// Generate labels from work orders then send print instruction to label printer
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            ValueObjectList<WorkOrderVo> inVo = vo as ValueObjectList<WorkOrderVo>;

            List<WorkOrderVo> workOrders = inVo?.GetList();

            if (workOrders == null || workOrders.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(OutputMultipleWorkOrderFilesCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // Generate labels.  Label value object could be ProductLabelVo or InternalLogisticsLabelVo.

            ValueObjectList<ValueObject> labelVo = generateLabelsFromWorkOrderLinesCbm.Execute(trxContext, inVo) as ValueObjectList<ValueObject>;

            List<ValueObject> labels = labelVo?.GetList();

            if (labels == null || labels.Count <= 0)
            {
                var messageData = new MessageData("zwce00027", Properties.Resources.zwce00027, nameof(generateLabelsFromWorkOrderLinesCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            foreach (ValueObject label in labels)
            {
                if (!(label is ProductLabelVo || label is InternalLogisticsLabelVo))
                {
                    var messageData = new MessageData("zwce00028", Properties.Resources.zwce00028, nameof(generateLabelsFromWorkOrderLinesCbm));
                    logger.Error(messageData);
                    throw new Framework.ApplicationException(messageData);
                }
            }


            // Print labels. 

            PrintLabelsResultVo printResult = printLabelCbm.Execute(trxContext, labelVo) as PrintLabelsResultVo;

            if (printResult == null)
            {
                var messageData = new MessageData("zwce00029", Properties.Resources.zwce00029, nameof(printLabelCbm));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            return printResult;

        }

    }
}
