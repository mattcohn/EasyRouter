using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyRouter.ViewModels
{
    class RouterConfigHomeViewModel : ViewModelBase
    {
        private Action
            _quickSetupAction,
            _advancedConfigAciton,
            _getHelpAction;

        public RouterConfigHomeViewModel(Action quickSetupAction, Action advancedConfigAction, Action getHelpAction)
        {
            _quickSetupAction = quickSetupAction;
            _advancedConfigAciton = advancedConfigAction;
            _getHelpAction = getHelpAction;
        }

        public ICommand QuickSetupCommand
        {
            get
            {
                return new DelegateCommand(new Action<object>((sender) => _quickSetupAction()));
            }
        }

        public ICommand AdvancedConfigCommand
        {
            get
            {
                return new DelegateCommand(new Action<object>((sender) => _advancedConfigAciton()));
            }
        }

        public ICommand GetHelpCommand
        {
            get
            {
                return new DelegateCommand(new Action<object>((sender) => _getHelpAction()));
            }
        }
    }
}
