﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Com.ZimVie.Wcs.ZWCS.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings_Tokyo : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings_Tokyo defaultInstance = ((Settings_Tokyo)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings_Tokyo())));
        
        public static Settings_Tokyo Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public string AUTHENTIFICATE_FLAG {
            get {
                return ((string)(this["AUTHENTIFICATE_FLAG"]));
            }
            set {
                this["AUTHENTIFICATE_FLAG"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("東京倉庫")]
        public string APPLICATION_ENVIRONMENT_HEADER {
            get {
                return ((string)(this["APPLICATION_ENVIRONMENT_HEADER"]));
            }
            set {
                this["APPLICATION_ENVIRONMENT_HEADER"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Server=10.204.161.9;Port=5432;UserId=postgres;Password=postgres;Database=ZWCS_2.0" +
            ";CommandTimeout=300;")]
        public string CONNECTION_STRING {
            get {
                return ((string)(this["CONNECTION_STRING"]));
            }
            set {
                this["CONNECTION_STRING"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Tokyo Standard Time")]
        public string SERVER_TIME_ZONE {
            get {
                return ((string)(this["SERVER_TIME_ZONE"]));
            }
            set {
                this["SERVER_TIME_ZONE"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Com.ZimVie.Wcs.ZWCS.ZwcsMainForm")]
        public string ApplicationTypeName {
            get {
                return ((string)(this["ApplicationTypeName"]));
            }
            set {
                this["ApplicationTypeName"] = value;
            }
        }
    }
}
