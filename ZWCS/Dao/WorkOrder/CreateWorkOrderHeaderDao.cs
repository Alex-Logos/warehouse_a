using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{

    class CreateWorkOrderHeaderDao : AbstractDataAccessObject
    {

        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(CreateWorkOrderHeaderDao));


        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            ValueObjectList<WorkOrderHeaderVo> inVo = arg as ValueObjectList<WorkOrderHeaderVo>;

            List<WorkOrderHeaderVo> headers = inVo?.GetList();

            if (headers == null || headers.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(CreateWorkOrderHeaderDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT INTO t_work_order ");
            sqlQuery.Append("( ");
            sqlQuery.Append(" work_order_number, ");
            sqlQuery.Append(" shipping_notice_id, ");
            sqlQuery.Append(" purchase_order_number, ");
            sqlQuery.Append(" commercial_invoice_number, ");
            sqlQuery.Append(" packing_material_1, ");
            sqlQuery.Append(" standard_work_instruction, ");
            sqlQuery.Append(" registration_user_cd,");
            sqlQuery.Append(" registration_date_time,");
            sqlQuery.Append(" warehouse_cd ");
            sqlQuery.Append(") ");
            sqlQuery.Append("VALUES ");

            foreach (WorkOrderHeaderVo header in headers)
            {
                string index = headers.IndexOf(header).ToString();
                sqlQuery.Append("( ");
                sqlQuery.Append(" :workOrderNumber" + index + ",");
                sqlQuery.Append(" :shippingNoticeId" + index + ",");
                sqlQuery.Append(" :purchaseOrderNumber" + index + ",");
                sqlQuery.Append(" :commercialInvoiceNumber" + index + ",");
                sqlQuery.Append(" :packingMaterial1" + index + ",");
                sqlQuery.Append(" :standardWorkInstruction" + index + ",");
                sqlQuery.Append(" :registrationUserCode" + index + ",");
                sqlQuery.Append(" :registrationDateTime" + index + ",");
                sqlQuery.Append(" :warehouseCode" + index);
                sqlQuery.Append(") ");
                if (header == headers.Last()) break;
                sqlQuery.Append(", ");
            }

            sqlQuery.Append("RETURNING work_order_number, work_order_id;");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();

            foreach (WorkOrderHeaderVo header in headers)
            {
                string index = headers.IndexOf(header).ToString();
                sqlParameter.AddParameterString("workOrderNumber" + index, header.WorkOrderNumber);
                sqlParameter.AddParameterInteger("shippingNoticeId" + index, header.ShippingNoticeId);
                sqlParameter.AddParameterString("purchaseOrderNumber" + index, header.PurchaseOrderNumber);
                sqlParameter.AddParameterString("commercialInvoiceNumber" + index, header.CommercialInvoiceNumber);
                sqlParameter.AddParameterString("packingMaterial1" + index, header.PackingMaterial1);
                sqlParameter.AddParameterString("standardWorkInstruction" + index, header.StandardWorkInstruction);
                sqlParameter.AddParameterString("registrationUserCode" + index, UserData.GetUserData().UserCode);
                sqlParameter.AddParameterDateTime("registrationDateTime" + index, trxContext.ProcessingDBDateTime);
                sqlParameter.AddParameterString("warehouseCode" + index, UserData.GetUserData().FactoryCode);
            }

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            Dictionary<string, int> workOrderNumerIdPairs = new Dictionary<string, int>();

            while (dataReader.Read())
            {
                string orderNumber = ConvertDBNull<string>(dataReader, "work_order_number");
                int orderId = ConvertDBNull<int>(dataReader, "work_order_id");

                workOrderNumerIdPairs.Add(orderNumber, orderId);
            }
            dataReader.Close();


            WorkOrderNumerIdPairVo outVo = new WorkOrderNumerIdPairVo 
            { 
                WorkOrderNumerIdPairDictionary = workOrderNumerIdPairs 
            };

            return outVo;

        }

    }
}
