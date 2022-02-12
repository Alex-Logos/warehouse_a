using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ShippingNoticeLineVo : ValueObject
    {
        public int ShippingNoticeLineId { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public string ItemNumber { get; set; }

        public string SupplierItemNumber { get; set; }

        public string LotNumber { get; set; }

        public int LotQuantity { get; set; }

        public DateTime LotExpirationDate { get; set; }


    }
}
