using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class DeleteZwcsItemsDao : AbstractDataAccessObject
    {
        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ReadZwcsUpdatOrCreateTargetItemsDao));


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

            // Building SQL
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("DELETE FROM m_item ");
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND item_number = ANY(:itemList) ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameter("itemList", itemsNumbers);

            ResultVo outVo = new ResultVo();
            outVo.AffectedCount = sqlCommandAdapter.ExecuteNonQuery(sqlParameter);

            return outVo;
        }
    }
}
