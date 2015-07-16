using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Framework
{
    class Pages : Page
    {
        #region Constructor
        #endregion Constructor

        #region Public Properties
        [ElementDefinition("userName")]
        public IWebElement UserNameTextBox { get; private set; }

        [ElementDefinition("password")]
        public IWebElement PasswordTextBox { get; private set; }

        [ElementDefinition("remember_me_checkbox")]
        public IWebElement KeepMeSignedInCheckBox { get; private set; }

        [ElementDefinition("btnSubmit")]
        public IWebElement SignInButton { get; private set; }

        [ElementDefinition("password-show-link")]
        public IWebElement ForgotyourpasswordLink { get; private set; }

        [ElementDefinition("register_link")]
        public IWebElement SigningUpLink { get; private set; }

        //social provider
        [ElementDefinition(ElementMode.Soft, "login_title")]
        public IWebElement PageTitle { get; private set; }

        [ElementDefinition("ldap-label")]
        public IWebElement LDAPSection { get; private set; }

        [ElementDefinition(ElementMode.Lazy, "adsk-custom-logo")]
        public IWebElement CustomLogoImage
        {
            get { return lazyElements["CustomLogoImage"].Value; }
            set { }
        }

        #endregion Public Properties

        #region Public Methods
        public override void WaitForPageReady()
        {
            TestContextExtension.TC.Driver.WaitForElement(new []{"userName"});
        }
        #endregion Public Methods

        public static Pages Create()
        {
            return new Pages();
        }
    }
}
