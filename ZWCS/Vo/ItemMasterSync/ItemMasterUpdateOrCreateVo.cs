using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ItemMasterUpdateOrCreateVo : ValueObject
    {
        public List<ItemMasterVo> UpdateItems { get; set; }

        public List<ItemMasterVo> CreateItems { get; set; }

    }
}
