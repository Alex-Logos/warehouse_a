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
    class CreateShippingNoticeLineDao : AbstractDataAccessObject
    {
        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(CreateShippingNoticeLineDao));


        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            var inVo = arg as ValueObjectList<ShippingNoticeLineVo>;

            List<ShippingNoticeLineVo> lines = inVo?.GetList();

            if (lines == null || lines.Count <= 0)
            {
                MessageData messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(lines));
                logger.Error(messageData);
                throw new ApplicationException(messageData);
            }

            //create SQL
            var sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT INTO t_shipping_notice_line ");
            sqlQuery.Append("( ");
            sqlQuery.Append(" shipping_notice_id, ");
            sqlQuery.Append(" purchase_order_number, ");
            sqlQuery.Append(" commercial_invoice_number, ");
            sqlQuery.Append(" item_number, ");
            sqlQuery.Append(" supplier_item_number, ");
            sqlQuery.Append(" lot_number, ");
            sqlQuery.Append(" lot_quantity, ");
            sqlQuery.Append(" lot_expiration_date,");
            sqlQuery.Append(" registration_user_cd,");
            sqlQuery.Append(" registration_date_time,");
            sqlQuery.Append(" warehouse_cd ");
            sqlQuery.Append(") ");
            sqlQuery.Append("VALUES ");

            foreach (ShippingNoticeLineVo line in lines)
            {
                string index = lines.IndexOf(line).ToString();
                sqlQuery.Append("( ");
                sqlQuery.Append(" :shippingNoticeId" + index + ",");
                sqlQuery.Append(" :purchaseOrderNumber" + index + ",");
                sqlQuery.Append(" :commercialInvoiceNumber" + index + ",");
                sqlQuery.Append(" :itemNumber" + index + ",");
                sqlQuery.Append(" :supplierItemNumber" + index + ",");
                sqlQuery.Append(" :lotNumber" + index + ",");
                sqlQuery.Append(" :lotQuantity" + index + ",");
                sqlQuery.Append(" :lotExpirationDate" + index + ",");
                sqlQuery.Append(" :registrationUserCode" + index + ",");
                sqlQuery.Append(" :registrationDateTime" + index + ",");
                sqlQuery.Append(" :warehouseCode" + index);
                sqlQuery.Append(")");
                if (line == lines.Last()) break;
                sqlQuery.Append(", ");
            }

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();

            foreach (ShippingNoticeLineVo line in lines)
            {
                string index = lines.IndexOf(line).ToString();
                sqlParameter.AddParameterInteger("shippingNoticeId" + index, line.ShippingNoticeLineId);
                sqlParameter.AddParameterString("purchaseOrderNumber" + index, line.PurchaseOrderNumber);
                sqlParameter.AddParameterString("commercialInvoiceNumber" + index, line.InvoiceNumber);
                sqlParameter.AddParameterString("itemNumber" + index, line.ItemNumber);
                sqlParameter.AddParameterString("supplierItemNumber" + index, line.SupplierItemNumber);
                sqlParameter.AddParameterString("lotNumber" + index, line.LotNumber);
                sqlParameter.AddParameterInteger("lotQuantity" + index, line.LotQuantity);
                sqlParameter.AddParameterDateTime("lotExpirationDate" + index, line.LotExpirationDate);
                sqlParameter.AddParameterString("registrationUserCode" + index, UserData.GetUserData().UserCode);
                sqlParameter.AddParameterDateTime("registrationDateTime" + index, trxContext.ProcessingDBDateTime);
                sqlParameter.AddParameterString("warehouseCode" + index, UserData.GetUserData().FactoryCode);
            }

            //execute SQL

            var outVo = new ResultVo();
            outVo.AffectedCount = sqlCommandAdapter.ExecuteNonQuery(sqlParameter);

            return outVo;

        }

    }
}
