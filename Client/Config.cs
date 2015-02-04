using System;
using System.Configuration;

namespace Dwolla
{
    /// <summary>
    ///     Configuration class, wraps around ConfigurationManager
    /// </summary>
    public class Config
    {
        /// <summary>
        ///     The Dwolla API Application ID
        /// </summary>
        public string client_id
        {
            get { return ConfigurationManager.AppSettings["client_id"]; }
            set { ConfigurationManager.AppSettings["client_id"] = value; }
        }

        /// <summary>
        ///     The Dwolla API Application Secret
        /// </summary>
        public string client_secret
        {
            get { return ConfigurationManager.AppSettings["client_secret"]; }
            set { ConfigurationManager.AppSettings["client_secret"] = value; }
        }

        /// <summary>
        ///     Your/OAuth User's Dwolla PIN
        /// </summary>
        public int pin
        {
            get { return Convert.ToInt16(ConfigurationManager.AppSettings["pin"]); }
            set { ConfigurationManager.AppSettings["pin"] = value.ToString(); }
        }

        /// <summary>
        ///     Your/OAuth User's Access Token
        /// </summary>
        public string access_token
        {
            get { return ConfigurationManager.AppSettings["access_token"]; }
            set { ConfigurationManager.AppSettings["access_token"] = value; }
        }

        /// <summary>
        ///     OAuth permissions scope
        /// </summary>
        public string oauth_scope
        {
            get { return ConfigurationManager.AppSettings["oauth_scope"]; }
            set { ConfigurationManager.AppSettings["oauth_scope"] = value; }
        }

        /// <summary>
        ///     Dwolla production host
        /// </summary>
        public string production_host
        {
            get { return ConfigurationManager.AppSettings["production_host"]; }
            set { ConfigurationManager.AppSettings["production_host"] = value; }
        }

        /// <summary>
        ///     Dwolla sandbox host
        /// </summary>
        public string sandbox_host
        {
            get { return ConfigurationManager.AppSettings["sandbox_host"]; }
            set { ConfigurationManager.AppSettings["sandbox_host"] = value; }
        }

        /// <summary>
        ///     Default REST postfix
        /// </summary>
        public string default_postfix
        {
            get { return ConfigurationManager.AppSettings["default_postfix"]; }
            set { ConfigurationManager.AppSettings["default_postfix"] = value; }
        }

        /// <summary>
        ///     Debug flag
        /// </summary>
        public bool debug
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["debug"]); }
            set { ConfigurationManager.AppSettings["debug"] = value.ToString(); }
        }

        /// <summary>
        ///     Toggle use of Sandbox host (for testing)
        /// </summary>
        public bool sandbox
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["sandbox"]); }
            set { ConfigurationManager.AppSettings["sandbox"] = value.ToString(); }
        }
    }
}