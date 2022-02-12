using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Com.ZimVie.Wcs.Framework.Login;
using System.Reflection;

namespace Com.ZimVie.Wcs.ZWCS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = new Mutex(false, Assembly.GetExecutingAssembly().ManifestModule.Name);

            try
            {
                if (mutex.WaitOne(0, false))
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    ApplicationContext appCntxt =
                                            new Framework.DefaultApplicationContext(
                                                Assembly.GetExecutingAssembly().ManifestModule.Name,
                                                ZwcsConfigurationDataTypeEnum.APPLICATION_TYPE_NAME.GetValue(),
                                                Assembly.GetExecutingAssembly().GetName().Name, true);
                    Application.Run(appCntxt);

                }
            }
            finally
            {
                mutex.Close();
            }
        }
    }
}
