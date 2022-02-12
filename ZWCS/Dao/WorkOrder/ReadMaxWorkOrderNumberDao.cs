using System;
using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadMaxWorkOrderNumberDao : AbstractDataAccessObject
    {
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            var inVo = (ShippingNoticeTrackingNumberVo)arg;

            //create SQL
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" MAX(work_order_number) ");            
            sqlQuery.Append("FROM t_work_order ");
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");
         
            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
                       
            //execute SQL
            string maxOrder = sqlCommandAdapter.ExecuteScalar(sqlParameter) as string;


            MaxWorkOrderNumberVo outVo = new MaxWorkOrderNumberVo
            {
                 OrderNumber = maxOrder
            };

            return outVo;
        }
    }
}
