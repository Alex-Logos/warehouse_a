using System;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to insert RTV pallet current location
    /// </summary>
    public class CreateWorkOrderCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(CreateShippingNoticeCbm));


        private readonly DataAccessObject createWorkOrderHeaderDao = new CreateWorkOrderHeaderDao();


        private readonly DataAccessObject createWorkOrderLineDao = new CreateWorkOrderLineDao();


        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            var inVo = vo as ValueObjectList<WorkOrderVo>;

            List<WorkOrderVo> orders = inVo.GetList();

            foreach (var order in orders)
            {
                WorkOrderHeaderVo headerVo = order.Header;

                var outVo = createWorkOrderHeaderDao.Execute(trxContext, headerVo) as ResultVo;

                if (outVo == null || outVo.AffectedCount <= 0)
                {
                    var messageData = new MessageData("zwce00002", Properties.Resources.zwce00002, "");

                    logger.Error(messageData);
                    throw new Framework.ApplicationException(messageData);
                }


                List<WorkOrderLineVo> lines = order.Lines;
                lines.ForEach(l => l.WorkOrderId = outVo.ResultId);

                var lineVo = new ValueObjectList<WorkOrderLineVo>();
                lineVo.SetNewList(lines);

                var outVo2 = createWorkOrderLineDao.Execute(trxContext, lineVo) as ResultVo;

                if (outVo2 == null || outVo2.AffectedCount <= 0)
                {
                    var messageData = new MessageData("zwce00002", Properties.Resources.zwce00002, "");

                    logger.Error(messageData);
                    throw new Framework.ApplicationException(messageData);
                }
            }

            return inVo;
        }

    }
}
