using System;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;
using System.Linq;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// 
    /// </summary>
    public class VarifyLotNumberBetweenWorkOrderAndActualGoodsCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(VarifyLotNumberBetweenWorkOrderAndActualGoodsCbm));

        /// <summary>
        /// Instantiate CBM to generate excel file and reflect values for single work order
        /// </summary>
        private readonly CbmController varifyItemNumberBetweenWorkOrderAndActualGoodsCbm = new VarifyItemNumberBetweenWorkOrderAndActualGoodsCbm();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            //VarificationItemQueryVo inVo = vo as VarificationItemQueryVo;

            //string itemNumberOnOrder = inVo?.ItemNumberOnOrder;

            //string itemNumberOnGoods = inVo?.ItemNumberOnGoods;

            //if (string.IsNullOrEmpty(itemNumberOnOrder) || string.IsNullOrWhiteSpace(itemNumberOnGoods))
            //{
            //    var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(itemNumberOnOrder) + " or " + nameof(itemNumberOnGoods));
            //    logger.Error(messageData);

            //    throw new Framework.ApplicationException(messageData);
            //}


            //if (itemNumberOnGoods.Contains(itemNumberOnOrder))
            //{
            //    return new BooleanValueObject { BooleanValue = true };
            //}


            //ItemNumberVo order = new ItemNumberVo();
            //order.ItemNumber = itemNumberOnOrder;

            //SupplierItemNumberVo master = readSupplierItemNumberByItemNumberDao.Execute(trxContext, order) as SupplierItemNumberVo;

            //string supplierItemNumberOnMaster = master?.SupplierItemNumber;

            //if (string.IsNullOrWhiteSpace(supplierItemNumberOnMaster))
            //{
            //    var messageData = new MessageData("zwce00044", Properties.Resources.zwce00044, itemNumberOnOrder, "なし", itemNumberOnGoods);
            //    logger.Error(messageData);

            //    throw new Framework.ApplicationException(messageData);
            //}

            //if (!itemNumberOnGoods.Contains(supplierItemNumberOnMaster))
            //{
            //    var messageData = new MessageData("zwce00044", Properties.Resources.zwce00044, itemNumberOnOrder, supplierItemNumberOnMaster, itemNumberOnOrder);
            //    logger.Error(messageData);

            //    throw new Framework.ApplicationException(messageData);
            //}


            //return new BooleanValueObject { BooleanValue = true };

            return null ;
        }
    }
}
