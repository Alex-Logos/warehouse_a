using System;
using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class AggregateShippingNoticeToWorkOrderLineDao : AbstractDataAccessObject
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(AggregateShippingNoticeToWorkOrderLineDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            WorkOrderLineCreationVo orderLineInVo = arg as WorkOrderLineCreationVo;

            if (orderLineInVo == null || orderLineInVo.ShippingNoticeId == 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(AggregateShippingNoticeToWorkOrderLineDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" i.attached_document_control_number, ");
            sqlQuery.Append(" sl.item_number, ");
            sqlQuery.Append(" sl.lot_number, ");
            sqlQuery.Append(" SUM(sl.lot_quantity) ::INTEGER AS lot_quantity, ");
            sqlQuery.Append(" MAX(sl.lot_expiration_date) AS lot_expiration_date, ");
            sqlQuery.Append(" MAX(i.supplier_item_number) AS supplier_item_number, ");
            sqlQuery.Append(" MAX(i.item_description_japanese) AS item_description_japanese, ");
            sqlQuery.Append(" MAX(i.jan_number) AS jan_number, ");
            sqlQuery.Append(" MAX(i.pdoruct_name) AS pdoruct_name, ");
            sqlQuery.Append(" MAX(i.pdoruct_category) AS pdoruct_category, ");
            sqlQuery.Append(" MAX(i.packing_material_1) AS packing_material_1, ");
            sqlQuery.Append(" MAX(i.packing_material_2) AS packing_material_2, ");
            sqlQuery.Append(" MAX(i.standard_work_instruction) AS standard_work_instruction, ");
            sqlQuery.Append(" MAX(i.additional_work_instruction) AS additional_work_instruction, ");
            sqlQuery.Append(" MAX(i.label_type) AS label_type ");
            sqlQuery.Append("FROM t_shipping_notice_line sl ");
            sqlQuery.Append(" INNER JOIN m_item i USING(item_number) ");
            sqlQuery.Append("WHERE sl.warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND sl.shipping_notice_id = :shippingNoticeId ");
            sqlQuery.Append("GROUP BY ");
            sqlQuery.Append(" i.attached_document_control_number, ");
            sqlQuery.Append(" sl.item_number, ");
            sqlQuery.Append(" sl.lot_number ");
            sqlQuery.Append("ORDER BY ");
            sqlQuery.Append(" i.attached_document_control_number, ");
            sqlQuery.Append(" sl.item_number, ");
            sqlQuery.Append(" sl.lot_number ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameterInteger("shippingNoticeId", orderLineInVo.ShippingNoticeId);

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            ValueObjectList<WorkOrderLineVo> outVo = new ValueObjectList<WorkOrderLineVo>();

            while (dataReader.Read())
            {
                WorkOrderLineVo vo = new WorkOrderLineVo();
                vo.AttachedDocumentControlNumber = ConvertDBNull<string>(dataReader, "attached_document_control_number");
                vo.ItemNumber = ConvertDBNull<string>(dataReader, "item_number");
                vo.LotNumber = ConvertDBNull<string>(dataReader, "lot_number");
                vo.LotQuantity = ConvertDBNull<int>(dataReader, "lot_quantity");
                vo.LotExpirationDate = ConvertDBNull<DateTime>(dataReader, "lot_expiration_date");
                vo.SupplierItemNumber = ConvertDBNull<string>(dataReader, "supplier_item_number");
                vo.ItemDescriptionJapanese = ConvertDBNull<string>(dataReader, "item_description_japanese");
                vo.JanNumber = ConvertDBNull<string>(dataReader, "jan_number");
                vo.ProductName = ConvertDBNull<string>(dataReader, "pdoruct_name");
                vo.ProductCategory = ConvertDBNull<string>(dataReader, "pdoruct_category");
                vo.PackingMaterial1 = ConvertDBNull<string>(dataReader, "packing_material_1");
                vo.PackingMaterial2 = ConvertDBNull<string>(dataReader, "packing_material_2");
                vo.StandardWorkInstruction = ConvertDBNull<string>(dataReader, "standard_work_instruction");
                vo.AdditionalWorkInstruction = ConvertDBNull<string>(dataReader, "additional_work_instruction");
                vo.LabelType = ConvertDBNull<int>(dataReader, "label_type");
                outVo.add(vo);
            }
            dataReader.Close();

            return outVo;
        }
    }
}
