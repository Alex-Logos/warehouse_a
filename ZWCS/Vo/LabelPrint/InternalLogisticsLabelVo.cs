using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class InternalLogisticsLabelVo : ValueObject
    {
        // Label Header

        public string AttachedDocumentControlNumber { get; set; }

        public string WorkOrderNumber { get; set; }

        public int SerialWithinWorkOrder { get; set; }

        public int ProductQunaity { get; set; }

        public int LabelQunaity { get; set; }


        // Label Body

        public string ItemNumber { get; set; }

        public string LotNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string ProductName { get; set; }

        public string ItemNumberWithLegacyItemNumber { get; set; }
    }
}
