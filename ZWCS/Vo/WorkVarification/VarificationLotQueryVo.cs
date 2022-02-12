using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class VarificationLotQueryVo : ValueObject
    {

        public string ItemNumberOnOrder { get; set; }

        public string ItemNumberOnGoods { get; set; }

        public string LotNumberOnOrder { get; set; }

        public string LotNumberOnGoods { get; set; }

    }
}
