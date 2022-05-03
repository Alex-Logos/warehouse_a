using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class CreateWorkOrderLineDao : AbstractDataAccessObject
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(CreateWorkOrderLineDao));

        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            ValueObjectList<WorkOrderLineVo> inVo = arg as ValueObjectList<WorkOrderLineVo>;

            List<WorkOrderLineVo> lines = inVo?.GetList();

            if (lines == null || lines.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(CreateWorkOrderLineDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT INTO t_work_order_line ");
            sqlQuery.Append("( ");
            sqlQuery.Append(" work_order_id, ");
            sqlQuery.Append(" serial_within_work_order, ");
            sqlQuery.Append(" work_order_sub_number, ");
            sqlQuery.Append(" serial_within_work_order_sub_number, ");
            sqlQuery.Append(" page_within_work_order_sub_number, ");
            sqlQuery.Append(" item_number, ");
            sqlQuery.Append(" supplier_item_number, ");
            sqlQuery.Append(" item_description_japanese, ");
            sqlQuery.Append(" jan_number, ");
            sqlQuery.Append(" pdoruct_name, ");
            sqlQuery.Append(" pdoruct_category, ");
            sqlQuery.Append(" lot_number, ");
            sqlQuery.Append(" lot_expiration_date, ");
            sqlQuery.Append(" lot_quantity, ");
            sqlQuery.Append(" packing_material_2, ");
            sqlQuery.Append(" additional_work_instruction, ");
            sqlQuery.Append(" label_type, ");
            sqlQuery.Append(" registration_user_cd,");
            sqlQuery.Append(" registration_date_time,");
            sqlQuery.Append(" warehouse_cd ");
            sqlQuery.Append(") ");
            sqlQuery.Append("VALUES ");

            foreach (WorkOrderLineVo line in lines)
            {
                string index = lines.IndexOf(line).ToString();
                sqlQuery.Append("( ");
                sqlQuery.Append(" :workOrderId" + index + ",");
                sqlQuery.Append(" :serialWithinWorkOrder" + index + ",");
                sqlQuery.Append(" :workOrderSubNumber" + index + ",");
                sqlQuery.Append(" :serialWithinWorkOrderSubNumber" + index + ",");
                sqlQuery.Append(" :pageWithinWorkOrderSubNumber" + index + ",");
                sqlQuery.Append(" :itemNumber" + index + ",");
                sqlQuery.Append(" :supplierItemNumber" + index + ",");
                sqlQuery.Append(" :itemDescriptionJapanese" + index + ",");
                sqlQuery.Append(" :janNumber" + index + ",");
                sqlQuery.Append(" :productName" + index + ",");
                sqlQuery.Append(" :productCategory" + index + ",");
                sqlQuery.Append(" :lotNumber" + index + ",");
                sqlQuery.Append(" :lotExpirationDate" + index + ",");
                sqlQuery.Append(" :lotQuantity" + index + ",");
                sqlQuery.Append(" :packingMaterial2" + index + ",");
                sqlQuery.Append(" :additonalWorkInstruction" + index + ",");
                sqlQuery.Append(" :labelType" + index + ",");
                sqlQuery.Append(" :registrationUserCode" + index + ",");
                sqlQuery.Append(" :registrationDateTime" + index + ",");
                sqlQuery.Append(" :warehouseCode" + index);
                sqlQuery.Append(")");
                if (line == lines.Last()) break;
                sqlQuery.Append(", ");
            }

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();

            foreach (WorkOrderLineVo line in lines)
            {
                string index = lines.IndexOf(line).ToString();
                sqlParameter.AddParameterInteger("workOrderId" + index, line.WorkOrderId);
                sqlParameter.AddParameterInteger("serialWithinWorkOrder" + index, line.SerialWithinWorkOrder);
                sqlParameter.AddParameterInteger("workOrderSubNumber" + index, line.WorkOrderSubNumber);
                sqlParameter.AddParameterInteger("serialWithinWorkOrderSubNumber" + index, line.SerialWithinWorkOrderSubNumber);
                sqlParameter.AddParameterInteger("pageWithinWorkOrderSubNumber" + index, line.PageWithinWorkOrderSubNumber);
                sqlParameter.AddParameterString("itemNumber" + index, line.ItemNumber);
                sqlParameter.AddParameterString("supplierItemNumber" + index, line.SupplierItemNumber);
                sqlParameter.AddParameterString("itemDescriptionJapanese" + index, line.ItemDescriptionJapanese);
                sqlParameter.AddParameterString("janNumber" + index, line.JanNumber);
                sqlParameter.AddParameterString("productName" + index, line.ProductName);
                sqlParameter.AddParameterString("productCategory" + index, line.ProductCategory);
                sqlParameter.AddParameterString("lotNumber" + index, line.LotNumber);
                sqlParameter.AddParameterDateTime("lotExpirationDate" + index, line.LotExpirationDate);
                sqlParameter.AddParameterInteger("lotQuantity" + index, line.LotQuantity);
                sqlParameter.AddParameterString("packingMaterial2" + index, line.PackingMaterial2);
                sqlParameter.AddParameterString("additonalWorkInstruction" + index, line.AdditionalWorkInstruction);
                sqlParameter.AddParameterInteger("labelType" + index, line.LabelType);
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