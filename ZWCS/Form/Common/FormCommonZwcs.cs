using System;
using Com.ZimVie.Wcs.Framework;
using System.Collections.Generic;

namespace Com.ZimVie.Wcs.ZWCS
{
    public partial class FormCommonZwcs
    {

        /// <summary>
        /// initialize popupmessagecontroller
        /// </summary>
        private readonly PopUpMessageController popUpMessage = new PopUpMessageController();

        /// <summary>
        /// initialize CommonLogger
        /// </summary>
        private static readonly CommonLogger Logger = CommonLogger.GetInstance(typeof(FormCommonZwcs));



        /// <summary>
        /// constructor
        /// </summary>
        public FormCommonZwcs()
        {
            InitializeComponent();
        }


    }
}
