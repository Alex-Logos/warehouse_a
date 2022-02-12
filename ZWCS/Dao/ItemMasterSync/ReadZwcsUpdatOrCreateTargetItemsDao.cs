using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadZwcsUpdatOrCreateTargetItemsDao : AbstractDataAccessObject
    {

        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ReadZwcsUpdatOrCreateTargetItemsDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            ItemNumbersVo inVo = arg as ItemNumbersVo;

            List<string> itemsNumbers = inVo?.ItemNumbers;

            if (itemsNumbers == null || itemsNumbers.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(itemsNumbers));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" item_number, ");
            sqlQuery.Append(" item_description_japanese,");
            sqlQuery.Append(" item_organization,");
            sqlQuery.Append(" locator,");
            sqlQuery.Append(" item_status_oracle,");
            sqlQuery.Append(" item_status_local,");
            sqlQuery.Append(" reuse_category,");
            sqlQuery.Append(" remark_local,");
            sqlQuery.Append(" source_type,");
            sqlQuery.Append(" supplier_number,");
            sqlQuery.Append(" supplier_name,");
            sqlQuery.Append(" supplier_item_number,");
            sqlQuery.Append(" attached_document_control_number, ");
            sqlQuery.Append(" attached_document_locator,  ");
            sqlQuery.Append(" standard_work_instruction,");
            sqlQuery.Append(" additional_work_instruction,");
            sqlQuery.Append(" packing_material_1,");
            sqlQuery.Append(" packing_material_2,");
            sqlQuery.Append(" distribution_category,");
            sqlQuery.Append(" order_to_order,");
            sqlQuery.Append(" jan_number,");
            sqlQuery.Append(" pdoruct_name,");
            sqlQuery.Append(" pdoruct_category,");
            sqlQuery.Append(" regulatory_approval_number,");
            sqlQuery.Append(" jmdn_number,");
            sqlQuery.Append(" class_category,");
            sqlQuery.Append(" reuse_category,");
            sqlQuery.Append(" control_category,");
            sqlQuery.Append(" sterilization_category,");
            sqlQuery.Append(" manufacturer_name,");
            sqlQuery.Append(" legacy_item_number,");
            sqlQuery.Append(" legacy_item_number_display_necessary, ");
            sqlQuery.Append(" label_type, ");
            sqlQuery.Append(" registration_user_cd, ");
            sqlQuery.Append(" registration_date_time, ");
            sqlQuery.Append(" warehouse_cd  ");
            sqlQuery.Append("FROM m_item ");          
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND item_number = ANY(:itemList) ");
            sqlQuery.Append("ORDER BY item_number ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameter("itemList", itemsNumbers);

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            List<ItemMasterVo> zwcsItems = new List<ItemMasterVo>();

            while (dataReader.Read())
            {
                ItemMasterVo item = new ItemMasterVo();
                item.ItemNumber = ConvertDBNull<string>(dataReader, "item_number");
                item.ItemDescriptionJapanes = ConvertDBNull<string>(dataReader, "item_description_japanese");
                item.ItemOrganization = ConvertDBNull<string>(dataReader, "item_organization");
                item.Locator = ConvertDBNull<string>(dataReader, "locator");
                item.ItemStatusOracle = ConvertDBNull<string>(dataReader, "item_status_oracle");
                item.ItemStatusLocal = ConvertDBNull<string>(dataReader, "item_status_local");
                item.ReuseCategory = ConvertDBNull<string>(dataReader, "reuse_category");
                item.RemarkLocal = ConvertDBNull<string>(dataReader, "remark_local");
                item.SourceType = ConvertDBNull<string>(dataReader, "source_type");
                item.SupplierNumber = ConvertDBNull<string>(dataReader, "supplier_number");
                item.SupplierName = ConvertDBNull<string>(dataReader, "supplier_name");
                item.ProductName = ConvertDBNull<string>(dataReader, "supplier_item_number");
                item.SupplierItemNumber = ConvertDBNull<string>(dataReader, "attached_document_control_number");
                item.AttachedDocumentLocator = ConvertDBNull<string>(dataReader, "attached_document_locator");
                item.StandardWorkInstruction = ConvertDBNull<string>(dataReader, "standard_work_instruction");
                item.AdditionalWorkInstruction = ConvertDBNull<string>(dataReader, "additional_work_instruction");
                item.PackingMaterial1 = ConvertDBNull<string>(dataReader, "packing_material_1");
                item.PackingMaterial2 = ConvertDBNull<string>(dataReader, "packing_material_2");
                item.DistributionCategory = ConvertDBNull<string>(dataReader, "distribution_category");
                item.OrderToOrder = ConvertDBNull<bool>(dataReader, "order_to_order");
                item.ProductName = ConvertDBNull<string>(dataReader, "pdoruct_name");
                item.ProductCategory = ConvertDBNull<string>(dataReader, "pdoruct_category");
                item.RegulatoryApprovalNumber = ConvertDBNull<string>(dataReader, "regulatory_approval_number");
                item.JmdnNumber = ConvertDBNull<string>(dataReader, "jmdn_number");
                item.ClassCategory = ConvertDBNull<string>(dataReader, "class_category");
                item.ReuseCategory = ConvertDBNull<string>(dataReader, "reuse_category");
                item.ControlCategory = ConvertDBNull<string>(dataReader, "control_category");
                item.SterilizationCategory = ConvertDBNull<string>(dataReader, "sterilization_category");
                item.ManufacturerName = ConvertDBNull<string>(dataReader, "manufacturer_name");
                item.JanNumber = ConvertDBNull<string>(dataReader, "jan_number");
                item.LegacyItemNumber = ConvertDBNull<string>(dataReader, "legacy_item_number");
                item.LegacyItemNumberDisplayNecessary = ConvertDBNull<bool>(dataReader, "legacy_item_number_display_necessary");
                item.LabelType = ConvertDBNull<int>(dataReader, "label_type");
                item.RegistrationUserCode = ConvertDBNull<string>(dataReader, "registration_user_cd");
                item.RegistrationDateTime = ConvertDBNull<DateTime>(dataReader, "registration_date_time");
                item.WarehouseCode = ConvertDBNull<string>(dataReader, "warehouse_cd");
                zwcsItems.Add(item);
            }
            dataReader.Close();

            ValueObjectList<ItemMasterVo> outVo = new ValueObjectList<ItemMasterVo>();
            outVo.SetNewList(zwcsItems);

            return outVo;

        }
    }
}
