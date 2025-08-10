using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emec.contracts.handlers
{
    public interface IConfigurationServiceHandler
    {
        string GetAppConfigurations(string conString, string configname, string environment);

    }
}
