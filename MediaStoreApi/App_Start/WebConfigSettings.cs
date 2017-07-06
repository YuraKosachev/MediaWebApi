using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace MediaStoreApi
{
    public class WebConfigSettings
    {
        public string GetStringSetting(string name)
        {
            return GetSetting(name);
        }
        public int GetIntOrDefaultSetting(string name,int defaultValue)
        {
            int result;
            return int.TryParse(GetSetting(name),out result) ? result : defaultValue;
        }
        public double GetDoubleOrDefaultSetting(string name, double defaultValue)
        {
            double result;
            return double.TryParse(GetSetting(name), out result) ? result : defaultValue;
        }

        private string GetSetting(string name)
        {
            return WebConfigurationManager.AppSettings[name];
        }
       
    }
}