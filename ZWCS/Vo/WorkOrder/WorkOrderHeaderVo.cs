using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class WorkOrderHeaderVo : ValueObject
    {
        public int WorkOrderId { get; set; }

        public string WorkOrderNumber { get; set; }

        public int ShippingNoticeId { get; set; }

        public string AttachedDocumentControlNumber { get; set; }

        public string AttachedDocumentLocator { get; set; }

        public int WorkOrderOperationStage { get; set; }

        public string ShippingNoticeTrackingNumber { get; set; }

        public List<string> PurchaseOrderNumbers { get; set; }


    }
}
