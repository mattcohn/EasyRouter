using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EasyRouter.ViewModels
{
    class SetNetworkNameViewModel : ViewModelBase
    {
        public SetNetworkNameViewModel(Func<string> getNetworkName, Action<string> setNetworkName, Action setNetworkNameNext)
        {
            NetworkName = getNetworkName();
            _setNetworkName = setNetworkName;
            _setNetworkNameNext = setNetworkNameNext;
        }

        private string _networkName;
        private Action<string> _setNetworkName;
        private Action _setNetworkNameNext;

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

        public ICommand NextCommand
        {
            get
            {
                return new DelegateCommand(new Action<object>((sender) => 
                    {
                        _setNetworkName(_networkName);
                        _setNetworkNameNext();
                    }));
            }
        }

    }
}
