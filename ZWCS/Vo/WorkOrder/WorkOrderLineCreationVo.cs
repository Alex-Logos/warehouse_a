using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class WorkOrderLineCreationVo : ValueObject
    {
        public int ShippingNoticeId;

        public List<WorkOrderHeaderVo> WorkOrderHeaders;
    }
}
