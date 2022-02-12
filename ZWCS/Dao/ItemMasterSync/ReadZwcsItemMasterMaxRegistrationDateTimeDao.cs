using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadZwcsItemMasterMaxRegistrationDateTimeDao : AbstractDataAccessObject
    {

        /// <summary>
        /// Instantiate CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ReadZwcsItemMasterMaxRegistrationDateTimeDao));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            if (arg != null)
            {
                var messageData = new MessageData("zwce00050", Properties.Resources.zwce00050, this.ToString());
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            //create SQL
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT MAX(registration_date_time) AS registration_date_time ");
            sqlQuery.Append("FROM m_item ");          
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);

            //execute SQL
            DateTime? maxDateTime = sqlCommandAdapter.ExecuteScalar(sqlParameter) as DateTime?;

            return new ItemMasterMaxDateTimeVo { RegistrationDateTime = maxDateTime ?? DateTime.MinValue };

        }
    }
}
