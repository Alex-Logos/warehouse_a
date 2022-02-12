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
    public class ReadItemNumberAndLotNumberFromProductLabelBarcodeCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(OutputMultipleWorkOrderFilesCbm));

        /// <summary>
        /// Instantiate CBM to generate excel file and reflect values for single work order
        /// </summary>
        private readonly AbstractDataAccessObject readItemNumberByJanNumberDao = new ReadItemNumberByJanNumberDao();


        /// <summary>
        /// 
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            ProductLabelBarcodeVo inVo = vo as ProductLabelBarcodeVo;

            string productLabelBarcode = inVo?.ProductLabelBarcode;

            if (string.IsNullOrWhiteSpace(productLabelBarcode))
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(inVo.ProductLabelBarcode));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            if (!productLabelBarcode.StartsWith("01"))
            {
                var messageData = new MessageData("zwce00039", Properties.Resources.zwce00039, productLabelBarcode);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            if (productLabelBarcode.Length < 19)
            {
                var messageData = new MessageData("zwce00040", Properties.Resources.zwce00040, productLabelBarcode);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            string janNumber = productLabelBarcode.Substring(2, 14);

            // Read item number by jan number from item master

            JanNumberVo janVo = new JanNumberVo();
            janVo.JanNumber = janNumber;

            ValueObjectList<ItemNumberVo> itemVo = readItemNumberByJanNumberDao.Execute(trxContext, janVo) as ValueObjectList<ItemNumberVo>;

            List<ItemNumberVo> items = itemVo?.GetList();

            string itemNumber = items?.FirstOrDefault()?.ItemNumber;

            if (items == null || items.Count <= 0 || string.IsNullOrWhiteSpace(itemNumber))
            {
                var messageData = new MessageData("zwce00036", Properties.Resources.zwce00013, nameof(readItemNumberByJanNumberDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            if (items.Count > 1)
            {
                string itemNumbers = string.Join(",", items.Select(i => i.ItemNumber));

                var messageData = new MessageData("zwce00037", Properties.Resources.zwce00013, janVo.JanNumber, itemNumbers);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // Separate lot numbre from remaining text

            string expirationAndLot = productLabelBarcode.Replace("01" + janNumber, string.Empty);

            string lotNumber = null;

            if (expirationAndLot.StartsWith("17"))
            {
                lotNumber = expirationAndLot.Substring(10, expirationAndLot.Length - 10);
            }
            else if (expirationAndLot.StartsWith("10"))
            {
                lotNumber = expirationAndLot.Substring(2, expirationAndLot.Length - 2);
            }
            else
            {
                var messageData = new MessageData("zwce00041", Properties.Resources.zwce00041, productLabelBarcode);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // Return item number and lot number

            VarificationItemLotVo outVo = new VarificationItemLotVo();
            outVo.ItemNumber = itemNumber;
            outVo.LotNumber = lotNumber;

            return outVo;

        }
    }
}
