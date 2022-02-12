/*
 * Copyright 2021 by Taku Fujii, All rights reserved.
 *
 *  Change Tracking
 *  2021/09/14 <Change NO.0001> Newly created by Takusuke Fujii(ZBD G.K.)
 */
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class CreateZwcsItemHistoryDao : AbstractDataAccessObject
    {
        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(CreateZwcsItemHistoryDao));


        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            ValueObjectList<ItemMasterVo> inVo = arg as ValueObjectList<ItemMasterVo>;

            List<ItemMasterVo> items = inVo?.GetList();

            if (items == null || items.Count <= 0)
            {
                MessageData messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(items));
                logger.Error(messageData);
                throw new ApplicationException(messageData);
            }

            //create SQL
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT INTO m_item_history ");
            sqlQuery.Append("( ");
            sqlQuery.Append(" item_number, ");
            sqlQuery.Append(" item_description_japanese,");
            sqlQuery.Append(" item_organization,");
            sqlQuery.Append(" locator,");
            sqlQuery.Append(" item_status_oracle,");
            sqlQuery.Append(" item_status_local,");
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
            sqlQuery.Append(") ");
            sqlQuery.Append("VALUES ");

            foreach (ItemMasterVo item in items)
            {
                string index = items.IndexOf(item).ToString();
                sqlQuery.Append("( ");
                sqlQuery.Append(" :itemNumber" + index + ",");
                sqlQuery.Append(" :itemDescriptionJapanese" + index + ",");
                sqlQuery.Append(" :itemOrganization" + index + ",");
                sqlQuery.Append(" :locator" + index + ",");
                sqlQuery.Append(" :itemStatusOracle" + index + ",");
                sqlQuery.Append(" :itemStatusLocal" + index + ",");
                sqlQuery.Append(" :remarkLocal" + index + ",");
                sqlQuery.Append(" :sourceType" + index + ",");
                sqlQuery.Append(" :supplierNumber" + index + ",");
                sqlQuery.Append(" :supplierName" + index + ",");
                sqlQuery.Append(" :supplierItemNumber" + index + ",");
                sqlQuery.Append(" :attachedDocumentControlNumber" + index + ",");
                sqlQuery.Append(" :attachedDocumentLocator" + index + ",");
                sqlQuery.Append(" :standardWorkInstruction" + index + ",");
                sqlQuery.Append(" :additionalWorkInstruction" + index + ",");
                sqlQuery.Append(" :packingMaterial1" + index + ",");
                sqlQuery.Append(" :packingMaterial2" + index + ",");
                sqlQuery.Append(" :distributionCategory" + index + ",");
                sqlQuery.Append(" :orderToOrder" + index + ",");
                sqlQuery.Append(" :janNumber" + index + ",");
                sqlQuery.Append(" :pdoructName" + index + ",");
                sqlQuery.Append(" :pdoructCategory" + index + ",");
                sqlQuery.Append(" :regulatoryApprovalNumber" + index + ",");
                sqlQuery.Append(" :jmdnNumber" + index + ",");
                sqlQuery.Append(" :classCategory" + index + ",");
                sqlQuery.Append(" :reuseCategory" + index + ",");
                sqlQuery.Append(" :controlCategory" + index + ",");
                sqlQuery.Append(" :sterilizationCategory" + index + ",");
                sqlQuery.Append(" :manufacturerName" + index + ",");
                sqlQuery.Append(" :legacyItemNumber" + index + ",");
                sqlQuery.Append(" :legacyItemNumberDisplayNecessary" + index + ",");
                sqlQuery.Append(" :labelType" + index + ",");
                sqlQuery.Append(" :registrationUserCode" + index + ",");
                sqlQuery.Append(" :registrationDateTime" + index + ",");
                sqlQuery.Append(" :warehouseCode" + index);
                sqlQuery.Append(")");
                if (item == items.Last()) break;
                sqlQuery.Append(", ");
            }

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();

            foreach (ItemMasterVo item in items)
            {
                string index = items.IndexOf(item).ToString();
                sqlParameter.AddParameterString("itemNumber" + index, item.ItemNumber);
                sqlParameter.AddParameterString("itemDescriptionJapanese" + index, item.ItemDescriptionJapanes);
                sqlParameter.AddParameterString("itemOrganization" + index, item.ItemOrganization);
                sqlParameter.AddParameterString("locator" + index, item.Locator);
                sqlParameter.AddParameterString("itemStatusOracle" + index, item.ItemStatusOracle);
                sqlParameter.AddParameterString("itemStatusLocal" + index, item.ItemStatusLocal);
                sqlParameter.AddParameterString("remarkLocal" + index, item.RemarkLocal);
                sqlParameter.AddParameterString("sourceType" + index, item.SourceType);
                sqlParameter.AddParameterString("supplierNumber" + index, item.SupplierItemNumber);
                sqlParameter.AddParameterString("supplierName" + index, item.SupplierName);
                sqlParameter.AddParameterString("supplierItemNumber" + index, item.SupplierItemNumber);
                sqlParameter.AddParameterString("attachedDocumentControlNumber" + index, item.AttachedDocumentControlNumber);
                sqlParameter.AddParameterString("attachedDocumentLocator" + index, item.AttachedDocumentLocator);
                sqlParameter.AddParameterString("standardWorkInstruction" + index, item.StandardWorkInstruction);
                sqlParameter.AddParameterString("additionalWorkInstruction" + index, item.AdditionalWorkInstruction);
                sqlParameter.AddParameterString("packingMaterial1" + index, item.PackingMaterial1);
                sqlParameter.AddParameterString("packingMaterial2" + index, item.PackingMaterial2);
                sqlParameter.AddParameterString("distributionCategory" + index, item.DistributionCategory);
                sqlParameter.AddParameter("orderToOrder" + index, item.OrderToOrder);
                sqlParameter.AddParameterString("janNumber" + index, item.JanNumber);
                sqlParameter.AddParameterString("pdoructName" + index, item.ProductName);
                sqlParameter.AddParameterString("pdoructCategory" + index, item.ProductCategory);
                sqlParameter.AddParameterString("regulatoryApprovalNumber" + index, item.RegulatoryApprovalNumber);
                sqlParameter.AddParameterString("jmdnNumber" + index, item.JmdnNumber);
                sqlParameter.AddParameterString("classCategory" + index, item.ClassCategory);
                sqlParameter.AddParameterString("reuseCategory" + index, item.ReuseCategory);
                sqlParameter.AddParameterString("controlCategory" + index, item.ControlCategory);
                sqlParameter.AddParameterString("sterilizationCategory" + index, item.SterilizationCategory);
                sqlParameter.AddParameterString("manufacturerName" + index, item.ManufacturerName);
                sqlParameter.AddParameterString("legacyItemNumber" + index, item.LegacyItemNumber);
                sqlParameter.AddParameter("legacyItemNumberDisplayNecessary" + index, item.LegacyItemNumberDisplayNecessary);
                sqlParameter.AddParameterInteger("labelType" + index, item.LabelType);
                sqlParameter.AddParameterString("registrationUserCode" + index, item.RegistrationUserCode);
                sqlParameter.AddParameterDateTime("registrationDateTime" + index, item.RegistrationDateTime);
                sqlParameter.AddParameterString("warehouseCode" + index, item.WarehouseCode);
            }

            //execute SQL

            var outVo = new ResultVo();
            outVo.AffectedCount = sqlCommandAdapter.ExecuteNonQuery(sqlParameter);

            return outVo;

        }

    }
}
