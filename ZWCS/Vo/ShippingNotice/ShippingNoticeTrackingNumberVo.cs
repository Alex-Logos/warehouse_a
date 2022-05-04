using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ShippingNoticeTrackingNumberVo : ValueObject
    {
        public string ShippingNoticeTrackingNumber { get; set; }

        public string RegistrationUserCode { get; set; }

        public DateTime RegistrationDateTime { get; set; }

    }
}
