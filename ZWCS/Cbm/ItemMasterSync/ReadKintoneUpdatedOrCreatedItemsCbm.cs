using System;
using System.Linq;
using Com.ZimVie.Wcs.Framework;
using Com.ZimVie.Wcs.ZWCS.Vo;
using Com.ZimVie.Wcs.ZWCS.Dao;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Text;

namespace Com.ZimVie.Wcs.ZWCS.Cbm
{
    /// <summary>
    /// CBM to read max registration date time from ZWCS item master then read from Kintone item master updaed or newly created items later than the registration date time
    /// </summary>
    public class ReadKintoneUpdatedOrCreatedItemsCbm : CbmController
    {
        /// <summary>
        /// Instantiate logger
        /// </summary>
        private static readonly CommonLogger logger = CommonLogger.GetInstance(typeof(ReadKintoneUpdatedOrCreatedItemsCbm));

        /// <summary>
        /// Instantiate DAO to read ZWCS item master's max registration date time
        /// </summary>
        private readonly DataAccessObject readZwcsItemMasterMaxRegistrationDateTimeDao = new ReadZwcsItemMasterMaxRegistrationDateTimeDao();

        /// <summary>
        /// 1. Read max registration date time from ZWCS item master       
        /// 2. Read updated or newly created items from Kintone item master
        /// </summary>
        /// <param name="trxContext"></param>
        /// <param name="vo"></param>
        /// <returns></returns>
        public ValueObject Execute(TransactionContext trxContext, ValueObject vo)
        {

            if (vo != null)
            {
                var messageData = new MessageData("zwce00050", Properties.Resources.zwce00050, this.ToString());
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 1. Read max registration date time from ZWCS item master  

            ItemMasterMaxDateTimeVo timeVo = readZwcsItemMasterMaxRegistrationDateTimeDao.Execute(trxContext, null) as ItemMasterMaxDateTimeVo;

            if (timeVo == null || timeVo.RegistrationDateTime == DateTime.MinValue)
            {
                var messageData = new MessageData("zwce00051", Properties.Resources.zwce00051);
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }


            // 2. Read updated or newly created items from Kintone item master

            List<ItemMasterVo> itemNumbers = ReadKintoneItems(timeVo.RegistrationDateTime);


            // 3. Check each Kintone item master field value such as length

            StringBuilder errors = CheckKintoneMasterValues(itemNumbers);

            if (errors != null && errors.Length > 0)
            {
                var messageData = new MessageData("zwce00055", Properties.Resources.zwce00055, errors.ToString());
                logger.Error(messageData);
                throw new Framework.ApplicationException(messageData);
            }

            ValueObjectList<ItemMasterVo> outVo = new ValueObjectList<ItemMasterVo>();
            outVo.SetNewList(itemNumbers);

            return outVo;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        private List<ItemMasterVo> ReadKintoneItems(DateTime zwcsMaxDateTime)
        {
            string dsn = "CData Kintone Sys"; // "CData Kintone Source";
            string commandText = "SELECT 品目番号, 品目名, 品目組織, 棚番号, オラクル品目状態, ローカル品目状態, 品目運用状況メモ, 仕入タイプ, 仕入先番号, "
                + " 仕入先名, 仕入先品目番号, 添付文書管理番号, 添付文書保管棚番号, 標準製造作業内容, 追加製造作業内容, 資材1, 資材2, 配送区分, 受発注品, JANコード, 製品名, "
                + " 販売名, 薬事承認番号, JMDNコード, クラス分類, 再使用区分, 管理区分, 滅菌区分, 製造業者名, 楽商品目番号, 楽商品番表示の必要性, ラベル種類, \"更新者 Code\", 更新日時 "
                + " FROM Kintone.ZWCS品目マスタ WHERE 更新日時 >= '" + zwcsMaxDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "'";

            var odbcConBuilder = new OdbcConnectionStringBuilder { Dsn = dsn };

            using (var odbcConnection = new OdbcConnection { ConnectionString = odbcConBuilder.ToString()})
            {
                odbcConnection.Open();

                using (var odbcCommand = new OdbcCommand { Connection = odbcConnection, CommandText = commandText})
                {
                    var odbcDataReader = odbcCommand.ExecuteReader();

                    List<ItemMasterVo> items = LoadValueObjects(odbcDataReader);

                    return items;
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private StringBuilder CheckKintoneMasterValues(List<ItemMasterVo> items)
        {
            StringBuilder errors = new StringBuilder();

            foreach (ItemMasterVo item in items)
            {
                string itemNumber = item.ItemNumber;

                if (ValidateLength(item.ItemNumber, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ItemNumber) + " " + item.ItemNumber.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ItemDescriptionJapanes, 128))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ItemDescriptionJapanes) + " " + item.ItemDescriptionJapanes.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ItemOrganization, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ItemOrganization) + " " + item.ItemOrganization.Length + Environment.NewLine);
                }

                if (ValidateLength(item.Locator, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.Locator) + " " + item.Locator.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ItemStatusOracle, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ItemStatusOracle) + " " + item.ItemStatusOracle.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ItemStatusLocal, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ItemStatusLocal) + " " + item.ItemStatusLocal.Length + Environment.NewLine);
                }

                if (ValidateLength(item.RemarkLocal, 128))
                {
                    errors.Append(itemNumber + ": " + nameof(item.RemarkLocal) + " " + item.RemarkLocal.Length + Environment.NewLine);
                }

                if (ValidateLength(item.SourceType, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.SourceType) + " " + item.SourceType.Length + Environment.NewLine);
                }

                if (ValidateLength(item.SupplierNumber, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.SupplierNumber) + " " + item.SupplierNumber.Length + Environment.NewLine);
                }

                if (ValidateLength(item.SupplierName, 128))
                {
                    errors.Append(itemNumber + ": " + nameof(item.SupplierName) + " " + item.SupplierName.Length + Environment.NewLine);
                }

                if (ValidateLength(item.SupplierItemNumber, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.SupplierItemNumber) + " " + item.SupplierItemNumber.Length + Environment.NewLine);
                }

                if (ValidateLength(item.AttachedDocumentControlNumber, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.AttachedDocumentControlNumber) + " " + item.AttachedDocumentControlNumber.Length + Environment.NewLine);
                }

                if (ValidateLength(item.AttachedDocumentLocator, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.AttachedDocumentLocator) + " " + item.AttachedDocumentLocator.Length + Environment.NewLine);
                }

                if (ValidateLength(item.StandardWorkInstruction, 128))
                {
                    errors.Append(itemNumber + ": " + nameof(item.StandardWorkInstruction) + " " + item.StandardWorkInstruction.Length + Environment.NewLine);
                }

                if (ValidateLength(item.AdditionalWorkInstruction, 128))
                {
                    errors.Append(itemNumber + ": " + nameof(item.AdditionalWorkInstruction) + " " + item.AdditionalWorkInstruction.Length + Environment.NewLine);
                }

                if (ValidateLength(item.PackingMaterial1, 128))
                {
                    errors.Append(itemNumber + ": " + nameof(item.PackingMaterial1) + " " + item.PackingMaterial1.Length + Environment.NewLine);
                }

                if (ValidateLength(item.PackingMaterial2, 128))
                {
                    errors.Append(itemNumber + ": " + nameof(item.PackingMaterial2) + " " + item.PackingMaterial2.Length + Environment.NewLine);
                }

                if (ValidateLength(item.DistributionCategory, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.DistributionCategory) + " " + item.DistributionCategory.Length + Environment.NewLine);
                }

                //item.OrderToOrder = Convert.ToBoolean(reader["受発注品"]);

                if (ValidateLength(item.JanNumber, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.JanNumber) + " " + item.JanNumber.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ProductCategory, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ProductCategory) + " " + item.ProductCategory.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ProductName, 64))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ProductName) + " " + item.ProductName.Length + Environment.NewLine);
                }

                if (ValidateLength(item.RegulatoryApprovalNumber, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.RegulatoryApprovalNumber) + " " + item.RegulatoryApprovalNumber.Length + Environment.NewLine);
                }

                if (ValidateLength(item.JmdnNumber, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.JmdnNumber) + " " + item.JmdnNumber.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ClassCategory, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ClassCategory) + " " + item.ClassCategory.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ReuseCategory, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ReuseCategory) + " " + item.ReuseCategory.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ControlCategory, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ControlCategory) + " " + item.ControlCategory.Length + Environment.NewLine);
                }

                if (ValidateLength(item.SterilizationCategory, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.SterilizationCategory) + " " + item.SterilizationCategory.Length + Environment.NewLine);
                }

                if (ValidateLength(item.ManufacturerName, 64))
                {
                    errors.Append(itemNumber + ": " + nameof(item.ManufacturerName) + " " + item.ManufacturerName.Length + Environment.NewLine);
                }

                if (ValidateLength(item.LegacyItemNumber, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.LegacyItemNumber) + " " + item.LegacyItemNumber.Length + Environment.NewLine);
                }

                //item.LegacyItemNumberDisplayNecessary = Convert.ToBoolean(reader["楽商品番表示の必要性"]);


                //item.LabelType = Convert.ToInt32(reader["ラベル種類"]);

                if (ValidateLength(item.RegistrationUserCode, 32))
                {
                    errors.Append(itemNumber + ": " + nameof(item.RegistrationUserCode) + " " + item.RegistrationUserCode.Length + Environment.NewLine);
                }

                //item.RegistrationDateTime = Convert.ToDateTime(reader["更新日時"]);

            }

            return errors;
        }

        private bool ValidateLength(string value, int length)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            return value.Length > length;
        
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<ItemMasterVo> LoadValueObjects(OdbcDataReader reader)
        {
            List<ItemMasterVo> items = new List<ItemMasterVo>();

            while (reader.Read())
            {
                ItemMasterVo item = new ItemMasterVo();

                item.ItemNumber = Convert.ToString(reader["品目番号"]);

                item.ItemDescriptionJapanes = Convert.ToString(reader["品目名"]);

                item.ItemOrganization = Convert.ToString(reader["品目組織"]);

                item.Locator = Convert.ToString(reader["棚番号"]);

                item.ItemStatusOracle = Convert.ToString(reader["オラクル品目状態"]);

                item.ItemStatusLocal = Convert.ToString(reader["ローカル品目状態"]);

                item.RemarkLocal = Convert.ToString(reader["品目運用状況メモ"]);

                item.SourceType = Convert.ToString(reader["仕入タイプ"]);

                item.SupplierNumber = Convert.ToString(reader["仕入先番号"]);

                item.SupplierName = Convert.ToString(reader["仕入先名"]);

                item.SupplierItemNumber = Convert.ToString(reader["仕入先品目番号"]);

                item.AttachedDocumentControlNumber = Convert.ToString(reader["添付文書管理番号"]);

                item.AttachedDocumentLocator = Convert.ToString(reader["添付文書保管棚番号"]);

                item.StandardWorkInstruction = Convert.ToString(reader["標準製造作業内容"]);

                item.AdditionalWorkInstruction = Convert.ToString(reader["追加製造作業内容"]);

                item.PackingMaterial1 = Convert.ToString(reader["資材1"]);

                item.PackingMaterial2 = Convert.ToString(reader["資材2"]);

                item.DistributionCategory = Convert.ToString(reader["配送区分"]);

                item.OrderToOrder = Convert.ToBoolean(reader["受発注品"]);

                item.JanNumber = Convert.ToString(reader["JANコード"]);

                item.ProductName = Convert.ToString(reader["製品名"]);

                item.ProductCategory = Convert.ToString(reader["販売名"]);

                item.RegulatoryApprovalNumber = Convert.ToString(reader["薬事承認番号"]);

                item.JmdnNumber = Convert.ToString(reader["JMDNコード"]);

                item.ClassCategory = Convert.ToString(reader["クラス分類"]);

                item.ReuseCategory = Convert.ToString(reader["再使用区分"]);

                item.ControlCategory = Convert.ToString(reader["管理区分"]);

                item.SterilizationCategory = Convert.ToString(reader["滅菌区分"]);

                item.ManufacturerName = Convert.ToString(reader["製造業者名"]);

                item.LegacyItemNumber = Convert.ToString(reader["楽商品目番号"]);

                item.LegacyItemNumberDisplayNecessary = Convert.ToBoolean(reader["楽商品番表示の必要性"]);

                item.LabelType = Convert.ToInt32(reader["ラベル種類"]);

                item.RegistrationUserCode = Convert.ToString(reader["更新者 Code"]);

                item.RegistrationDateTime = Convert.ToDateTime(reader["更新日時"]);

                items.Add(item);
            }
            reader.Close();

            return items;
        }
    }
}
