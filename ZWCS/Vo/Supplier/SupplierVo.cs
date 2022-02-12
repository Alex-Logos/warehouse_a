using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class SupplierVo : ValueObject
    {
        public int SupplierId { get; set; }

        public string SupplierNumber { get; set; }

        public string SupplierNumberAndName { get; set; }

        public bool SourceTypeInventory { get; set; }

        public string SupplierName { get; set; }

    }
}
