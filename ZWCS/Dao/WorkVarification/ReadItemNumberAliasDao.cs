using System;
using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadItemNumberAliasDao : AbstractDataAccessObject
    {
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            ItemNumberVo inVo = arg as ItemNumberVo;

            StringBuilder sqlQuery = new StringBuilder();

            //create SQL
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" legacy_item_number, ");
            sqlQuery.Append(" supplier_item_number ");
            sqlQuery.Append("FROM m_item ");
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND item_number = :itemNumber ");
            //sqlQuery.Append(" AND item_status_local = :itemStatusocal ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();

            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameterString("itemNumber", inVo.ItemNumber);
            //sqlParameter.AddParameter("itemStatusocal", DBNull.Value);

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            ItemNumberAliasVo outVo = new ItemNumberAliasVo();

            while (dataReader.Read())
            {
                outVo.LegacyItemNumber = ConvertDBNull<string>(dataReader, "legacy_item_number");
                outVo.SupplierItemNumber = ConvertDBNull<string>(dataReader, "supplier_item_number");
            }
            dataReader.Close();

            return outVo;
        }
    }
}
