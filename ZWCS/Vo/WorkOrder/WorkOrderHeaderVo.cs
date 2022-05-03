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

        public string PurchaseOrderNumber { get; set; }

        public string CommercialInvoiceNumber { get; set; }

        public string PackingMaterial1 { get; set; }

        public string StandardWorkInstruction { get; set; }

        public string ShippingNoticeTrackingNumber { get; set; }


    }
}
