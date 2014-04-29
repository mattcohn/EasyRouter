using EasyRouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyRouter.ViewModels
{
    class AdvancedConfigurationViewModel : ViewModelBase
    {
        public AdvancedConfigurationViewModel(Func<string> getNetworkName, Func<string> getNetworkPassword)
        {
            NetworkName = getNetworkName();
            NetworkPassword = getNetworkPassword();
        }

        private string _networkName;
        private string _networkPassword;

        public string NetworkName
        {
            get
            {
                return _networkName;
            }

            set
            {
                if (_networkName != value)
                {
                    _networkName = value;
                    OnPropertyChanged("NetworkName");
                }
            }
        }

        public string NetworkPassword
        {
            get
            {
                return _networkPassword;
            }

            set
            {
                if (_networkPassword != value)
                {
                    _networkPassword = value;
                    OnPropertyChanged("NetworkPassword");
                }
            }
        }
    }
}
