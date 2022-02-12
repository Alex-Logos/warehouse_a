using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ShippingNoticeTrackingNumberVo : ValueObject
    {
        public string ShippingNoticeTrackingNumber { get; set; }

        public DateTime ShippingNoticeIssueDate { get; set; }

        public string SupplierNumber { get; set; }

        public string SupplierName { get; set; }

        public string RegistrationUserCode { get; set; }

        public DateTime RegistrationDateTime { get; set; }

    }
}
