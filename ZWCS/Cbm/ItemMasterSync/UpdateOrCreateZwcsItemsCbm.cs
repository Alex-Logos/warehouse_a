using System.Linq;
using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBN to update or create ZWCS item master records
    /// </summary>
    class UpdateOrCreateZwcsItemsCbm : CbmController
    {
        /// <summary>
        /// Initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(UpdateOrCreateZwcsItemsCbm));

        /// <summary>
        /// Instantiate DAO to delete update target items
        /// </summary>
        private readonly DataAccessObject deleteZwcsItemsDao = new DeleteZwcsItemsDao();

        /// <summary>
        /// Instantiate DAO to create both update and creation target items
        /// </summary>
        private readonly DataAccessObject createZwcsItemsDao = new CreateZwcsItemsDao();

        /// <summary>
        /// 1. Delete update target items
        /// 2. Create both update and creation target items
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {
            ItemMasterUpdateOrCreateVo inVo = vo as ItemMasterUpdateOrCreateVo;

            List<ItemMasterVo> updateItems = inVo?.UpdateItems;

            List<ItemMasterVo> createItems = inVo?.CreateItems;

            if ((updateItems == null || updateItems.Count <= 0) && (createItems == null || createItems.Count <= 0))
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(inVo.CreateItems));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 1. Delete update target items if the target exists

            if (updateItems != null && updateItems.Count > 0)
            {
                List<string> deleteTargets = updateItems.Select(i => i.ItemNumber).ToList();

                ItemNumbersVo deleteVo = new ItemNumbersVo { ItemNumbers = deleteTargets };

                ResultVo deleteResult = deleteZwcsItemsDao.Execute(trxContext, deleteVo) as ResultVo;

                if (deleteResult == null || deleteResult.AffectedCount <= 0)
                {
                    var messageData = new MessageData("zwce00053", Properties.Resources.zwce00053);
                    logger.Info(messageData);
                    throw new Framework.ApplicationException(messageData);
                }
            }


            // 2. Create both update and creation target items

            List<ItemMasterVo> updateAndCreateItems = new List<ItemMasterVo>();

            if (updateItems != null && updateItems.Count > 0)
            {
                updateAndCreateItems.AddRange(updateItems);
            }

            updateAndCreateItems.AddRange(createItems);

            ValueObjectList<ItemMasterVo> createVo = new ValueObjectList<ItemMasterVo>();
            createVo.SetNewList(updateAndCreateItems);

            ResultVo createResult = createZwcsItemsDao.Execute(trxContext, createVo) as ResultVo;

            if (createResult == null || createResult.AffectedCount <= 0 || createResult.AffectedCount != updateAndCreateItems.Count)
            {
                var messageData = new MessageData("zwce00054", Properties.Resources.zwce00054);
                logger.Info(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            return createResult;

        }
    }
}
