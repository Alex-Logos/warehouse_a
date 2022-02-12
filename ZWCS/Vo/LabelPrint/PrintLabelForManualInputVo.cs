using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class PrintLabelForManualInputVo : ValueObject
    {

        public string ItemNumber { get; set; }

        public string LotNumber { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int LabelQuantity { get; set; }

        public ItemMasterLabelFieldsVo LabelFieldsVo { get; set; }

    }
}
