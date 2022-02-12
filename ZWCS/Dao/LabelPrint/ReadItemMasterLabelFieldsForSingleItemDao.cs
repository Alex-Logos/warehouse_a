using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadItemMasterLabelFieldsForSingleItemDao : AbstractDataAccessObject
    {

        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ReadItemMasterLabelFieldsForSingleItemDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            ItemMasterLabelFieldsQueryForSingleItemVo inVo = arg as ItemMasterLabelFieldsQueryForSingleItemVo;

            if (inVo == null || inVo.ItemNumber == null)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(inVo.ItemNumber));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" item_number,");
            sqlQuery.Append(" pdoruct_category,");
            sqlQuery.Append(" pdoruct_name,");
            sqlQuery.Append(" regulatory_approval_number,");
            sqlQuery.Append(" jmdn_number,");
            sqlQuery.Append(" class_category,");
            sqlQuery.Append(" reuse_category,");
            sqlQuery.Append(" control_category,");
            sqlQuery.Append(" sterilization_category,");
            sqlQuery.Append(" manufacturer_name,");
            sqlQuery.Append(" jan_number,");
            sqlQuery.Append(" legacy_item_number,");
            sqlQuery.Append(" legacy_item_number_display_necessary, ");
            sqlQuery.Append(" label_type  ");
            sqlQuery.Append("FROM m_item ");          
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND item_number = :itemNumber ");
            sqlQuery.Append("ORDER BY item_number ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameterString("itemNumber", inVo.ItemNumber);

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            ItemMasterLabelFieldsVo outVo = null;

            while (dataReader.Read())
            {
                outVo = new ItemMasterLabelFieldsVo();
                outVo.ItemNumber = ConvertDBNull<string>(dataReader, "item_number");
                outVo.ProductCategory = ConvertDBNull<string>(dataReader, "pdoruct_category");
                outVo.ProductName = ConvertDBNull<string>(dataReader, "pdoruct_name");
                outVo.RegulatoryApprovalNumber = ConvertDBNull<string>(dataReader, "regulatory_approval_number");
                outVo.JmdnNumber = ConvertDBNull<string>(dataReader, "jmdn_number");
                outVo.ClassCategory = ConvertDBNull<string>(dataReader, "class_category");
                outVo.ReuseCategory = ConvertDBNull<string>(dataReader, "reuse_category");
                outVo.ControlCategory = ConvertDBNull<string>(dataReader, "control_category");
                outVo.SterilizationCategory = ConvertDBNull<string>(dataReader, "sterilization_category");
                outVo.ManufacturerName = ConvertDBNull<string>(dataReader, "manufacturer_name");
                outVo.JanNumber = ConvertDBNull<string>(dataReader, "jan_number");
                outVo.LegacyItemNumber = ConvertDBNull<string>(dataReader, "legacy_item_number");
                outVo.LegacyItemNumberDisplayNecessary = ConvertDBNull<bool>(dataReader, "legacy_item_number_display_necessary");
                outVo.LabelType = ConvertDBNull<int>(dataReader, "label_type");
            }
            dataReader.Close();

            return outVo;

        }
    }
}
