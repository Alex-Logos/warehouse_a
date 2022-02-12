using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ShippingNoticeVo : ValueObject
    {
        public ShippingNoticeHeaderVo Header { get; set; }

        public List<ShippingNoticeLineVo> Line { get; set; }

    }
}
