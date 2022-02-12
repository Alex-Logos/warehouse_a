using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class VarificationGridViewItemLotVo : ValueObject
    {
        public int ScanSerial { get; set; }

        public string ItemNumber { get; set; }

        public string LotNumber { get; set; }

    }
}
