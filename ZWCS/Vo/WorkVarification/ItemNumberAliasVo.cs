using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ItemNumberAliasVo : ValueObject
    {

        public string SupplierItemNumber { get; set; }

        public string LegacyItemNumber { get; set; }

    }
}
