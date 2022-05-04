using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadItemMasterItemNumbersDao : AbstractDataAccessObject
    {

        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ReadItemMasterItemNumbersDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            ItemNumbersVo inVo = arg as ItemNumbersVo;

            List<string> items = inVo?.ItemNumbers;

            if (inVo == null || items.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(items));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" item_number ");;
            sqlQuery.Append("FROM m_item ");          
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND item_number = ANY(:itemList) ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameter("itemList", items);

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            List<string> masterItems = new List<string>();

            while (dataReader.Read())
            {
                masterItems.Add(ConvertDBNull<string>(dataReader, "item_number"));
            }
            dataReader.Close();

            ItemNumbersVo outVo = new ItemNumbersVo();
            outVo.ItemNumbers = masterItems;

            return outVo;

        }
    }
}
