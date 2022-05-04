using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class PrintLabelsResultVo : ValueObject
    {

        public int ProductLabelSetCount { get; set; }

        public int ProductLabelQuantityTotal { get; set; }

        public int InternalLogisticsLabelSetCount { get; set; }

        public int InternalLogisticsLabelQuantityTotal { get; set; }

        public List<string> ProductLabelWorkOrders { get; set; }

        public List<string> InternalLogisticsLabelWorkOrders { get; set; }

    }
}
