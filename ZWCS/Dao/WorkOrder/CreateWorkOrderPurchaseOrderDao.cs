using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    /// <summary>
    /// 
    /// </summary>
    class CreateWorkOrderPurchaseOrderDao : AbstractDataAccessObject
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(CreateWorkOrderPurchaseOrderDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            ValueObjectList<WorkOrderPurchaseOrderVo> inVo = arg as ValueObjectList<WorkOrderPurchaseOrderVo>;

            List<WorkOrderPurchaseOrderVo> relations = inVo?.GetList();

            if (relations == null || relations.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(CreateWorkOrderPurchaseOrderDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT INTO t_work_order_purchase_order ");
            sqlQuery.Append("( ");
            sqlQuery.Append(" work_order_id, ");
            sqlQuery.Append(" purchase_order_number, ");
            sqlQuery.Append(" registration_user_cd,");
            sqlQuery.Append(" registration_date_time,");
            sqlQuery.Append(" warehouse_cd ");
            sqlQuery.Append(") ");
            sqlQuery.Append("VALUES ");

            foreach (WorkOrderPurchaseOrderVo relation in relations)
            {
                string index = relations.IndexOf(relation).ToString();
                sqlQuery.Append("( ");
                sqlQuery.Append(" :workOrderId" + index + ",");
                sqlQuery.Append(" :purchaseOrderNumber" + index + ",");
                sqlQuery.Append(" :registrationUserCode" + index + ",");
                sqlQuery.Append(" :registrationDateTime" + index + ",");
                sqlQuery.Append(" :warehouseCode" + index);
                sqlQuery.Append(")");
                if (relation == relations.Last()) break;
                sqlQuery.Append(", ");
            }

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();

            foreach (WorkOrderPurchaseOrderVo relation in relations)
            {
                string index = relations.IndexOf(relation).ToString();
                sqlParameter.AddParameterInteger("workOrderId" + index, relation.WorkOrderId);
                sqlParameter.AddParameterString("purchaseOrderNumber" + index, relation.PurchaseOrderNumber);
                sqlParameter.AddParameterString("registrationUserCode" + index, UserData.GetUserData().UserCode);
                sqlParameter.AddParameterDateTime("registrationDateTime" + index, trxContext.ProcessingDBDateTime);
                sqlParameter.AddParameterString("warehouseCode" + index, UserData.GetUserData().FactoryCode);
            }

            //execute SQL
            var outVo = new ResultVo();
            outVo.AffectedCount = sqlCommandAdapter.ExecuteNonQuery(sqlParameter);

            return outVo;
        }

    }
}
