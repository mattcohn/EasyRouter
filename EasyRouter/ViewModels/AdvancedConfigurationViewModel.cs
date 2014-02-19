using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.ViewModels
{
    class AdvancedConfigurationViewModel : ViewModelBase
    {
        private string _networkName;
        private string _networkPassword;
        public AdvancedConfigurationViewModel(
            Func<string> getNetworkName,
            Func<string> getNetworkPassword)
        {
            _networkName = getNetworkName();
            _networkPassword = getNetworkPassword();
        }

        public string NetworkName
        {
            get
            {
                return _networkName;
            }
        }


        public string NetworkPassword
        {
            get
            {
                return _networkPassword;
            }
        }
    }
}
