using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to generate excel files by copying template file then reflect the values for each work orders
    /// </summary>
    class OutputMultipleWorkOrderFilesCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(OutputMultipleWorkOrderFilesCbm));

        /// <summary>
        /// Instantiate CBM to generate excel file and reflect values for single work order
        /// </summary>
        private readonly CbmController outputWorkOrderFileCbm = new OutputWorkOrderFileCbm();

        /// <summary>
        /// Generate excel files by copying template file then reflect the values for each work orders
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


            // Generate excel file and reflect values for single work order

            List<WorkOrderOutputVo> filesGenerated = new List<WorkOrderOutputVo>();

            foreach (WorkOrderVo workOrder in workOrders)
            {

                WorkOrderHeaderVo headerValues = workOrder.Header;
                List<WorkOrderLineVo> lineValues = workOrder.Lines;

                if (headerValues == null || lineValues == null || lineValues.Count <= 0)
                {
                    var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(outputWorkOrderFileCbm));
                    logger.Error(messageData);
                    throw new Framework.ApplicationException(messageData);
                }


                WorkOrderOutputVo file = outputWorkOrderFileCbm.Execute(trxContext, workOrder) as WorkOrderOutputVo;


                if (file == null || string.IsNullOrWhiteSpace(file.FileName) || string.IsNullOrWhiteSpace(file.Directory))
                {
                    var messageData = new MessageData("zwce00023", Properties.Resources.zwce00023, nameof(headerValues.WorkOrderNumber));
                    logger.Error(messageData);
                    throw new Framework.ApplicationException(messageData);
                }


                filesGenerated.Add(file);
            }

            ValueObjectList<WorkOrderOutputVo> outVo = new ValueObjectList<WorkOrderOutputVo>();
            outVo.SetNewList(filesGenerated);

            return outVo;
        }

    }
}
