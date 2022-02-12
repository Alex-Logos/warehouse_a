using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ShippingNoticeHeaderVo : ValueObject
    {
        public int ShippingNoticeId { get; set; }

        public string ShippingNoticeTrackingNumber { get; set; }

        public DateTime ShippingNoticeIssueDate { get; set; }

        public bool SourceTypeInventory { get; set; }

        public string SupplierNumber { get; set; }

        public int ShippingNoticeOperationStage { get; set; }

        public string RegistrationUserCode { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public string WarehouseCode { get; set; }


    }
}
