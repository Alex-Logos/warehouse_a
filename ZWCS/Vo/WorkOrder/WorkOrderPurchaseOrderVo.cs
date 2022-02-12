using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class WorkOrderPurchaseOrderVo : ValueObject
    {
        public string AttachedDocumentControlNumber { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public int WorkOrderId { get; set; }

    }
}
