using System;
using System.Collections.Generic;
using Com.ZimVie.Wcs.Framework;

namespace Com.ZimVie.Wcs.ZWCS
{
    public class ZwcsConfigurationDataTypeEnum
    {
        private string keyName;

        private static ConfigurationReader configurationReader = new DefaultStaticCachedConfigurationReader();

        private ZwcsConfigurationDataTypeEnum(string keyName)
        {
            this.keyName = keyName;

        }

        /// <summary>
        /// get the value give in the settings
        /// throw exception is the keyName is not available in the settings
        /// </summary>
        /// <returns>value in the settings</returns>
        public String GetValue()
        {
            return configurationReader.GetValue(this.keyName);
        }

        public IList<string> GetValueList()
        {
            return configurationReader.GetValueList(this.keyName);
        }


        public static readonly ZwcsConfigurationDataTypeEnum APPLICATION_TYPE_NAME = new ZwcsConfigurationDataTypeEnum("ApplicationTypeName");

    }
}
