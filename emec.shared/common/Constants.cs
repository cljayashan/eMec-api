using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.shared.common
{
    public class Constants
    {
        public static class ApiActions
        {
            public const string List = "list";
            public const string Add = "add";
            public const string Edit = "edit";
            public const string View = "view";
            public const string Delete = "delete";
        }

        public static class ConfigurationService
        {
            public static string EmecConnectionString { get; set; }
        }

        public static class AppConfigurationKeys
        {
            public static string InventoryConnectionString = "InventoryDbConnection";
        }
    }
}
