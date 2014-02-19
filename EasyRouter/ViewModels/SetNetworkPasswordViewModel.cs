using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyRouter.ViewModels
{
    class SetNetworkPasswordViewModel : ViewModelBase
    {
        private string _networkPassword;
        private Action<string> _setNetworkPassword;
        private Action _setNetworkPasswordNext;
        public SetNetworkPasswordViewModel(
            Func<string> getNetworkPassword,
            Action<string> setNetworkPassword,
            Action setNetworkPasswordNext)
        {
            NetworkPassword = getNetworkPassword();
            _setNetworkPassword = setNetworkPassword;
            _setNetworkPasswordNext = setNetworkPasswordNext;
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

        public ICommand NextCommand
        {
            get
            {
                return new DelegateCommand(new Action<object>((sender) =>
                {
                    _setNetworkPassword(_networkPassword);
                    _setNetworkPasswordNext();
                }));
            }
        }


    }
}
