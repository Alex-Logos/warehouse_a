using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;
using System;

namespace Com.ZimVie.Wcs.ZWCS.Vo
{
    public class ItemMasterVo : ValueObject
    {
        public string ItemNumber { get; set; }

        public string ItemDescriptionJapanes { get; set; }

        public string ItemOrganization { get; set; }

        public string Locator { get; set; }

        public string ItemStatusOracle { get; set; }

        public string ItemStatusLocal { get; set; }

        public string RemarkLocal { get; set; }

        public string SourceType { get; set; }

        public string SupplierNumber { get; set; }

        public string SupplierName { get; set; }

        public string SupplierItemNumber { get; set; }

        public string AttachedDocumentControlNumber { get; set; }

        public string AttachedDocumentLocator { get; set; }

        public string StandardWorkInstruction { get; set; }

        public string AdditionalWorkInstruction { get; set; }

        public string PackingMaterial1 { get; set; }

        public string  PackingMaterial2 { get; set; }

        public string DistributionCategory { get; set; }

        public bool OrderToOrder { get; set; }

        public string JanNumber { get; set; }

        public string ProductCategory { get; set; }

        public string ProductName { get; set; }

        public string RegulatoryApprovalNumber { get; set; }

        public string JmdnNumber { get; set; }

        public string ClassCategory { get; set; }

        public string ReuseCategory { get; set; }

        public string ControlCategory { get; set; }

        public string SterilizationCategory { get; set; }

        public string ManufacturerName { get; set; }

        public string LegacyItemNumber { get; set; }

        public bool LegacyItemNumberDisplayNecessary { get; set; }

        public int LabelType { get; set; }

        public string RegistrationUserCode { get; set; }

        public DateTime RegistrationDateTime { get; set; }

        public string WarehouseCode { get; set; }


    }
}
