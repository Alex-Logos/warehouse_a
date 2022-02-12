using System;
using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadShippingNoticeTrackingNumberDao : AbstractDataAccessObject
    {
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            var inVo = (ShippingNoticeTrackingNumberVo)arg;

            //create SQL
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" sn.shipping_notice_tracking_number, ");
            sqlQuery.Append(" sn.shipping_notice_issue_date, ");            
            sqlQuery.Append(" sn.supplier_number, ");
            sqlQuery.Append(" sp.supplier_name, ");
            sqlQuery.Append(" sn.registration_user_cd, ");
            sqlQuery.Append(" sn.registration_date_time ");            
            sqlQuery.Append("FROM t_shipping_notice sn ");
            sqlQuery.Append(" LEFT JOIN m_supplier sp USING(supplier_number) ");
            sqlQuery.Append("WHERE sn.warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND sn.shipping_notice_tracking_number = :trackingNumber ");
         
            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();
            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameterString("trackingNumber", inVo.ShippingNoticeTrackingNumber);

            //execute SQL

            var outVo = new ResultVo();
            outVo.AffectedCount = sqlCommandAdapter.ExecuteNonQuery(sqlParameter);

            return outVo;
        }
    }
}
