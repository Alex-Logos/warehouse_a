using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ItemMasterLabelFieldsVo : ValueObject
    {
        public string ItemNumber { get; set; }

        public string ProductCategory { get; set; }

        public string ProductName { get; set; }

        public string RegulatoryApprovalNumber { get; set; }
 
        public string JmdnNumber { get; set; }

        public string ClassCategory { get; set; }

        public string ReuseCategory { get; set; }

        public string ControlCategory { get; set; }

        public string SterilizationCategory { get; set; }

        public string ManufacturerName { get; set; }

        public string JanNumber { get; set; }

        public string LegacyItemNumber { get; set; }

        public bool LegacyItemNumberDisplayNecessary { get; set; }

        public int LabelType { get; set; }

    }
}
