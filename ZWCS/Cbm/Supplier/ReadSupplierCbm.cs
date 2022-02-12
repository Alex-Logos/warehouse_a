

using System;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    public class ReadSupplierCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ReadSupplierCbm));

        /// <summary>
        /// 
        /// </summary>
        private readonly DataAccessObject readSupplierDao = new ReadSupplierDao();

        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            var inVo = vo as SupplierVo;
            
            var outVo = readSupplierDao.Execute(trxContext, vo) as ValueObjectList<SupplierVo>;

            return outVo;
        }
    }
}
