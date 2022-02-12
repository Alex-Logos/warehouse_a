using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class WorkOrderVo : ValueObject
    {
        public WorkOrderHeaderVo Header { get; set; }

        public List<WorkOrderLineVo> Lines { get; set; }

    }
}
