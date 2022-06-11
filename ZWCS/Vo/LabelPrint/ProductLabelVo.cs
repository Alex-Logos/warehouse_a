using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ProductLabelVo : ValueObject
    {

        // Label Header

        public string WorkOrderNumber { get; set; }

        public int SerialWithinWorkOrder { get; set; }

        public int SerialCount { get; set; }

        public int LabelQunaity { get; set; }


        // Label Body

        public string ProductCategory { get; set; }

        public string ProductName { get; set; }

        public string RegulatoryApprovalNumber { get; set; }

        public string ItemNumber { get; set; }

        public string LotNumber { get; set; }

        public string JmdnNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string ClassCategory { get; set; }

        public string ReuseCategory { get; set; }

        public string ControlCategory { get; set; }

        public string SterilizationCategory { get; set; }

        public string ManufacturerName { get; set; }

        public string JanNumber { get; set; }

        public string ItemNumberWithLegacyItemNumber { get; set; }
    }
}
