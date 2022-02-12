using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadItemMasterLabelFieldsDao : AbstractDataAccessObject
    {

        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ReadItemMasterLabelFieldsDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            ItemMasterLabelFieldsQueryVo inVo = arg as ItemMasterLabelFieldsQueryVo;

            List<string> items = inVo?.WorkOrderLineItems;

            if (inVo == null || items.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(items));
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
            sqlQuery.Append(" AND item_number = ANY(:itemList) ");
            sqlQuery.Append("ORDER BY item_number ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameter("itemList", items);

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            Dictionary<string, ItemMasterLabelFieldsVo> itemLavelFieldsDictionary = new Dictionary<string, ItemMasterLabelFieldsVo>();

            while (dataReader.Read())
            {
                ItemMasterLabelFieldsVo vo = new ItemMasterLabelFieldsVo();
                vo.ItemNumber = ConvertDBNull<string>(dataReader, "item_number");
                vo.ProductCategory = ConvertDBNull<string>(dataReader, "pdoruct_category");
                vo.ProductName = ConvertDBNull<string>(dataReader, "pdoruct_name");
                vo.RegulatoryApprovalNumber = ConvertDBNull<string>(dataReader, "regulatory_approval_number");
                vo.JmdnNumber = ConvertDBNull<string>(dataReader, "jmdn_number");
                vo.ClassCategory = ConvertDBNull<string>(dataReader, "class_category");
                vo.ReuseCategory = ConvertDBNull<string>(dataReader, "reuse_category");
                vo.ControlCategory = ConvertDBNull<string>(dataReader, "control_category");
                vo.SterilizationCategory = ConvertDBNull<string>(dataReader, "sterilization_category");
                vo.ManufacturerName = ConvertDBNull<string>(dataReader, "manufacturer_name");
                vo.JanNumber = ConvertDBNull<string>(dataReader, "jan_number");
                vo.LegacyItemNumber = ConvertDBNull<string>(dataReader, "legacy_item_number");
                vo.LegacyItemNumberDisplayNecessary = ConvertDBNull<bool>(dataReader, "legacy_item_number_display_necessary");
                vo.LabelType = ConvertDBNull<int>(dataReader, "label_type");
                itemLavelFieldsDictionary.Add(vo.ItemNumber, vo);
            }
            dataReader.Close();

            ItemMasterLabelFieldsQueryResultVo outVo = new ItemMasterLabelFieldsQueryResultVo();
            outVo.ItemLavelFieldsDictionary = itemLavelFieldsDictionary;

            return outVo;

        }
    }
}
