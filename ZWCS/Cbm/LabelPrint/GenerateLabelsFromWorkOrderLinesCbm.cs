using System;
using System.Linq;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to generate label value objects
    /// </summary>
    public class GenerateLabelsFromWorkOrderLinesCbm : CbmController
    {
        /// <summary>
        /// Instantiate logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(GenerateLabelsFromWorkOrderLinesCbm));

        /// <summary>
        /// Instantiate DAO to read item master's label related fields
        /// </summary>
        private readonly DataAccessObject readItemMasterLabelFieldsDao = new ReadItemMasterLabelFieldsDao();

        /// <summary>
        /// 1. Read item master label related fileds        
        /// 2. Generate label value objects
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {

            ValueObjectList<WorkOrderVo> inVo = vo as ValueObjectList<WorkOrderVo>;

            List<WorkOrderVo> workOrders = inVo?.GetList();

            if (workOrders == null || workOrders.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(workOrders));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            List<WorkOrderLineVo> workOrderLines = workOrders.SelectMany(w => w.Lines).Where(l => l.LabelType != 0).ToList();

            List<string> workOrderLineItems = workOrderLines.Select(l => l.ItemNumber).Distinct().ToList();

            if (workOrderLines.Count <= 0 || workOrderLineItems.Count <= 0)
            {
                var messageData = new MessageData("zwce00008", Properties.Resources.zwce00008, nameof(workOrderLines));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 1. Read item master label related fileds

            ItemMasterLabelFieldsQueryVo masterInVo = new ItemMasterLabelFieldsQueryVo();
            masterInVo.WorkOrderLineItems = workOrderLineItems;

            ItemMasterLabelFieldsQueryResultVo masterOutVo = readItemMasterLabelFieldsDao.Execute(trxContext, masterInVo) as ItemMasterLabelFieldsQueryResultVo;

            Dictionary<string, ItemMasterLabelFieldsVo> itemLavelFieldsDictionary = masterOutVo?.ItemLavelFieldsDictionary;

            if (itemLavelFieldsDictionary == null || itemLavelFieldsDictionary.Count <= 0 || itemLavelFieldsDictionary.Count != workOrderLineItems.Count)
            {
                var messageData = new MessageData("zwce00026", Properties.Resources.zwce00026, nameof(readItemMasterLabelFieldsDao));
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 2. Generate label value objects

            Dictionary<int, string> orderIdNumberDictionary = workOrders.ToDictionary(w => w.Header.WorkOrderId, w => w.Header.WorkOrderNumber);

            ValueObjectList<ValueObject> labelsOutVo = new ValueObjectList<ValueObject>();

            foreach (WorkOrderLineVo line in workOrderLines)
            {
                ValueObject label = null;
               
                switch (line.LabelType)
                {
                    case 0:
                        continue;
                    case 1:
                        label = GenerateProductLabel(line, itemLavelFieldsDictionary, orderIdNumberDictionary);
                        break;
                    case 2:
                        label = GenerateInternalLogisticsLabel(line, itemLavelFieldsDictionary, orderIdNumberDictionary);
                        break;
                    default:
                        var messageData = new MessageData("zwce00026", Properties.Resources.zwce00026, nameof(line.LabelType), line.LabelType.ToString());
                        logger.Error(messageData);
                        throw new Framework.ApplicationException(messageData);
                }

                labelsOutVo.add(label);
            }

            return labelsOutVo;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ProductLabelVo GenerateProductLabel(WorkOrderLineVo line, 
            Dictionary<string, ItemMasterLabelFieldsVo> itemLavelFieldsDictionary, Dictionary<int, string> orderIdNumberDictionary)
        {
            ItemMasterLabelFieldsVo master = itemLavelFieldsDictionary[line.ItemNumber];

            ProductLabelVo label = new ProductLabelVo();
            
            label.WorkOrderNumber = orderIdNumberDictionary[line.WorkOrderId];
            label.SerialWithinWorkOrder = line.SerialWithinWorkOrderSubNumber;
            label.LotNumber = line.LotNumber;
            label.ExpirationDate = line.LotExpirationDate;
            label.LabelQunaity = line.LotQuantity + 1;

            label.ProductCategory = master.ProductCategory;
            label.ProductName = master.ProductName;
            label.RegulatoryApprovalNumber = master.RegulatoryApprovalNumber;
            label.JmdnNumber = master.JmdnNumber;
            label.ClassCategory = master.ClassCategory;
            label.ReuseCategory = master.ReuseCategory;
            label.ControlCategory = master.ControlCategory;
            label.SterilizationCategory = master.SterilizationCategory;
            label.ManufacturerName = master.ManufacturerName;
            label.JanNumber = master.JanNumber;

            label.ItemNumber = line.ItemNumber;

            bool legacyItemNecessary = master.LegacyItemNumber != null && master.LegacyItemNumberDisplayNecessary;
            label.ItemNumberWithLegacyItemNumber = legacyItemNecessary ? line.ItemNumber + "(" + master.LegacyItemNumber + ")" : line.ItemNumber;

            return label;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InternalLogisticsLabelVo GenerateInternalLogisticsLabel(WorkOrderLineVo line, 
            Dictionary<string, ItemMasterLabelFieldsVo> itemLavelFieldsDictionary, Dictionary<int, string> orderIdNumberDictionary)
        {
            ItemMasterLabelFieldsVo master = itemLavelFieldsDictionary[line.ItemNumber];

            InternalLogisticsLabelVo label = new InternalLogisticsLabelVo();

            label.WorkOrderNumber = orderIdNumberDictionary[line.WorkOrderId];
            label.SerialWithinWorkOrder = line.SerialWithinWorkOrder;

            label.LotNumber = line.LotNumber;
            label.ExpirationDate = line.LotExpirationDate;
            label.LabelQunaity = line.LotQuantity + 1;
            label.ProductName = master.ProductName;

            label.ItemNumber = line.ItemNumber;

            bool legacyItemNecessary = master.LegacyItemNumber != null && master.LegacyItemNumberDisplayNecessary;
            label.ItemNumberWithLegacyItemNumber = legacyItemNecessary ? line.ItemNumber + "(" + master.LegacyItemNumber + ")" : line.ItemNumber;

            return label;

        }
    }
}
