using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ShippingNoticeHeaderVo : ValueObject
    {
        public int ShippingNoticeId { get; set; }

        public string ShippingNoticeTrackingNumber { get; set; }

        public DateTime ShippingNoticeIssueDate { get; set; }

        public bool SourceTypeInventory { get; set; }

        public string SupplierNumber { get; set; }

    }
}
