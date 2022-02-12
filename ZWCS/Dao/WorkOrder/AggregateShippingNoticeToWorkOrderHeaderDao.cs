using System;
using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class AggregateShippingNoticeToWorkOrderHeaderDao : AbstractDataAccessObject
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(AggregateShippingNoticeToWorkOrderHeaderDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            WorkOrderHeaderCreationVo inVo = arg as WorkOrderHeaderCreationVo;

            List<string> items = inVo?.ShippingNoticeLineItems;

            if (inVo == null || items == null || items.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(AggregateShippingNoticeToWorkOrderHeaderDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            //create SQL
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" attached_document_control_number, ");
            sqlQuery.Append(" attached_document_locator ");
            sqlQuery.Append("FROM m_item ");
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND item_number = ANY(:itemNumberList) ");
            sqlQuery.Append("GROUP BY ");
            sqlQuery.Append(" attached_document_control_number, ");
            sqlQuery.Append(" attached_document_locator ");
            sqlQuery.Append("ORDER BY ");
            sqlQuery.Append(" attached_document_control_number, ");
            sqlQuery.Append(" attached_document_locator ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameter("itemNumberList", inVo.ShippingNoticeLineItems);
                       
            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            ValueObjectList<WorkOrderHeaderVo> outVo = new ValueObjectList<WorkOrderHeaderVo>();

            while (dataReader.Read())
            {
                WorkOrderHeaderVo vo = new WorkOrderHeaderVo();
                vo.AttachedDocumentControlNumber = ConvertDBNull<string>(dataReader, "attached_document_control_number");
                vo.AttachedDocumentLocator = ConvertDBNull<string>(dataReader, "attached_document_locator");
                outVo.add(vo);
            }
            dataReader.Close();

            return outVo;
        }
    }
}
