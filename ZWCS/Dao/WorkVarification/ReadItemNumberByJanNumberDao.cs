using System;
using System.Data;
using System.Text;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;

namespace Com.ZimVie.Wcs.ZWCS.Dao
{
    class ReadItemNumberByJanNumberDao : AbstractDataAccessObject
    {
        public override ValueObject Execute(TransactionContext trxContext, ValueObject arg)
        {
            var inVo = arg as JanNumberVo;

            StringBuilder sqlQuery = new StringBuilder();

            //create SQL
            sqlQuery.Append("SELECT ");
            sqlQuery.Append(" item_number ");
            sqlQuery.Append("FROM m_item ");
            sqlQuery.Append("WHERE warehouse_cd = :warehouseCode ");
            sqlQuery.Append(" AND jan_number = :janNumber ");
            //sqlQuery.Append(" AND item_status_local = :itemStatusocal ");

            //create command
            DbCommandAdaptor sqlCommandAdapter = base.GetDbCommandAdaptor(trxContext, sqlQuery.ToString());

            //create parameter
            DbParameterList sqlParameter = sqlCommandAdapter.CreateParameterList();

            sqlParameter.AddParameterString("warehouseCode", trxContext.UserData.FactoryCode);
            sqlParameter.AddParameterString("janNumber", inVo.JanNumber);
            //sqlParameter.AddParameter("itemStatusocal", DBNull.Value);

            //execute SQL
            IDataReader dataReader = sqlCommandAdapter.ExecuteReader(trxContext, sqlParameter);

            var outVo = new ValueObjectList<ItemNumberVo>();

            while (dataReader.Read())
            {
                var vo = new ItemNumberVo();
                vo.ItemNumber = ConvertDBNull<string>(dataReader, "item_number");
                outVo.add(vo);
            }
            dataReader.Close();

            return outVo;
        }
    }
}
