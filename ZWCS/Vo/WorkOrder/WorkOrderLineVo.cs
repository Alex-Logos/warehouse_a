using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class WorkOrderLineVo : ValueObject
    {
        public string AttachedDocumentControlNumber { get; set; }

        public int WorkOrderLineId { get; set; }

        public int WorkOrderId { get; set; }

        public int SerialWithinWorkOrder { get; set; }

        public int PageWithinWorkOrder { get; set; }

        public string ItemNumber { get; set; }

        public string SupplierItemNumber { get; set; }

        public string ItemDescriptionJapanese { get; set; }

        public string JanNumber { get; set; }

        public string ProductName { get; set; }

        public string ProductCategory { get; set; }

        public string LotNumber { get; set; }

        public DateTime LotExpirationDate { get; set; }

        public int LotQuantity { get; set; }

        public string PackingMaterial1 { get; set; }

        public string PackingMaterial2 { get; set; }

        public string StandardWorkInstruction { get; set; }

        public string AdditionalWorkInstruction { get; set; }

        public int LabelType { get; set; }

    }
}
