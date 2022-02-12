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
    public class VarifyItemNumberBetweenWorkOrderAndActualGoodsCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(OutputMultipleWorkOrderFilesCbm));

        /// <summary>
        /// Instantiate DAO to read supplier item
        /// </summary>
        private readonly AbstractDataAccessObject readItemNumberAliasDao = new ReadItemNumberAliasDao();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            VarificationItemQueryVo inVo = vo as VarificationItemQueryVo;

            string itemNumberOnOrder = inVo?.ItemNumberOnOrder;

            string itemNumberOnGoods = inVo?.ItemNumberOnGoods;

            if (string.IsNullOrEmpty(itemNumberOnOrder) || string.IsNullOrWhiteSpace(itemNumberOnGoods))
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(itemNumberOnOrder) + " or " + nameof(itemNumberOnGoods));
                logger.Error(messageData);

                throw new Framework.ApplicationException(messageData);
            }


            // Match item number on order with item number on goods.  Some Oracle master item number includes unnecessary slash, comma, or both.

            string itemNumberOnOrderExcludingSlash = itemNumberOnOrder.Replace("/", string.Empty);
            string itemNumberOnOrderExcludingComma = itemNumberOnOrder.Replace(".", string.Empty);
            string itemNumberOnOrderExcludingHyphen = itemNumberOnOrder.Replace("-", string.Empty);
            string itemNumberOnOrderExcludingSlashAndComma = itemNumberOnOrder.Replace("/", string.Empty).Replace(".", string.Empty);
            string itemNumberOnOrderExcludingCommaAndHyphen = itemNumberOnOrder.Replace(".", string.Empty).Replace("-", string.Empty);
            string itemNumberOnOrderExcludingHyphenAndSlash = itemNumberOnOrder.Replace("-", string.Empty).Replace("/", string.Empty);

            if (itemNumberOnGoods.Contains(itemNumberOnOrder) ||
                itemNumberOnGoods.Contains(itemNumberOnOrderExcludingSlash) ||
                itemNumberOnGoods.Contains(itemNumberOnOrderExcludingComma) ||
                itemNumberOnGoods.Contains(itemNumberOnOrderExcludingHyphen) ||
                itemNumberOnGoods.Contains(itemNumberOnOrderExcludingSlashAndComma) ||
                itemNumberOnGoods.Contains(itemNumberOnOrderExcludingCommaAndHyphen) ||
                itemNumberOnGoods.Contains(itemNumberOnOrderExcludingHyphenAndSlash))
            {
                return new BooleanValueObject { BooleanValue = true };
            }


            // Read legacy item number and supplier item number on item master

            ItemNumberVo order = new ItemNumberVo();
            order.ItemNumber = itemNumberOnOrder;

            ItemNumberAliasVo alias = readItemNumberAliasDao.Execute(trxContext, order) as ItemNumberAliasVo;

            string legacyItemNumberOnMaster = alias?.LegacyItemNumber;

            string supplierItemNumberOnMaster = alias?.SupplierItemNumber;

            if (string.IsNullOrWhiteSpace(legacyItemNumberOnMaster) && string.IsNullOrWhiteSpace(supplierItemNumberOnMaster))
            {
                var messageData = new MessageData("zwce00048", Properties.Resources.zwce00048, itemNumberOnOrder, itemNumberOnGoods);
                logger.Error(messageData);

                throw new Framework.ApplicationException(messageData);
            }


            // Match legacy item number with item number on goods

            if (!string.IsNullOrWhiteSpace(legacyItemNumberOnMaster))
            {
                string legacyItemWithoutLeadingZeros = legacyItemNumberOnMaster.TrimStart('0');

                if (itemNumberOnGoods.Contains(legacyItemWithoutLeadingZeros))
                {

                    return new BooleanValueObject { BooleanValue = true };
                }
            }


            // Match supplier item number with item number on goods

            if (!string.IsNullOrWhiteSpace(supplierItemNumberOnMaster) &&
                    itemNumberOnGoods.Contains(supplierItemNumberOnMaster))
            {
                return new BooleanValueObject { BooleanValue = true };
            }


            // Throw exception when all the above 3 matching patterns fail.

            var errorMessage = new MessageData("zwce00044", Properties.Resources.zwce00044,
                itemNumberOnOrder, legacyItemNumberOnMaster, supplierItemNumberOnMaster, itemNumberOnGoods);
            logger.Error(errorMessage);

            throw new Framework.ApplicationException(errorMessage);

        }
    }
}
