using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadSupplierDao : AbstractDataAccessObject
    {
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            var inVo = arg as SupplierVo;

            StringBuilder sqlQuery = new StringBuilder();

            //create SQL
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" supplier_number, ");
            sqlQuery.Append(" supplier_name, ");
            sqlQuery.Append(" source_type_inventory ");
            sqlQuery.Append("FROM m_supplier ");          
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");
            sqlQuery.Append("ORDER BY supplier_number ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();

            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
                       
            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            var outVo = new ValueObjectList<SupplierVo>();

            while (dataReader.Read())
            {
                var vo = new SupplierVo();
                vo.SupplierNumber = ConvertDBNull<string>(dataReader, "supplier_number");
                vo.SupplierName = ConvertDBNull<string>(dataReader, "supplier_name");
                vo.SupplierNumberAndName = vo.SupplierNumber + " : " + vo.SupplierName;
                vo.SourceTypeInventory = ConvertDBNull<bool>(dataReader, "source_type_inventory");
                outVo.add(vo);
            }
            dataReader.Close();

            return outVo;

        }
    }
}
