/*
 * Copyright 2021 by Taku Fujii, All rights reserved.
 *
 *  Change Tracking
 *  2021/09/14 <Change NO.0001> Newly created by Takusuke Fujii(ZBD G.K.)
 */
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class CreateShippingNoticeHeaderDao : AbstractDataAccessObject
    {
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {

            var inVo = arg as ShippingNoticeHeaderVo;

            //create SQL
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT INTO t_shipping_notice ");
            sqlQuery.Append("( ");
            sqlQuery.Append(" shipping_notice_tracking_number, ");
            sqlQuery.Append(" shipping_notice_issue_date, ");
            sqlQuery.Append(" source_type_inventory, ");
            sqlQuery.Append(" supplier_number, ");
            sqlQuery.Append(" shipping_notice_operation_stage, ");
            sqlQuery.Append(" registration_user_cd, ");
            sqlQuery.Append(" registration_date_time,");
            sqlQuery.Append(" warehouse_cd ");
            sqlQuery.Append(") ");
            sqlQuery.Append("VALUES");
            sqlQuery.Append("( ");
            sqlQuery.Append(" :shippingNoticeTrackingNumber, ");
            sqlQuery.Append(" :shippingNoticeIssueDate, ");
            sqlQuery.Append(" :sourceTypeInventory, ");
            sqlQuery.Append(" :supplierNumber, ");
            sqlQuery.Append(" :shippingNoticeOperationStage, ");
            sqlQuery.Append(" :registrationUserCode, ");
            sqlQuery.Append(" :registrationDateTime, ");
            sqlQuery.Append(" :factoryCode ");
            sqlQuery.Append(") ");
            sqlQuery.Append("RETURNING shipping_notice_id;");

            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();

            sqlParameter.AddParameterString("shippingNoticeTrackingNumber", inVo.ShippingNoticeTrackingNumber);
            sqlParameter.AddParameterDateTime("shippingNoticeIssueDate", inVo.ShippingNoticeIssueDate);
            sqlParameter.AddParameter("sourceTypeInventory", inVo.SourceTypeInventory);
            sqlParameter.AddParameterString("supplierNumber", inVo.SupplierNumber);
            sqlParameter.AddParameterInteger("shippingNoticeOperationStage", inVo.ShippingNoticeOperationStage);
            sqlParameter.AddParameterString("registrationUserCode", UserData.GetUserData().UserCode);
            sqlParameter.AddParameterDateTime("registrationDateTime", trxContext.ProcessingDBDateTime);
            sqlParameter.AddParameterString("factoryCode", UserData.GetUserData().FactoryCode);

            //execute SQL

            var outVo = new ResultVo();
            outVo.ResultId = sqlCommandAdapter.ExecuteScalar(sqlParameter) as int? ?? 0;

            return outVo;

        }
    }
}
