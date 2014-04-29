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
            _advancedConfigAction,
            _getHelpAction,
            _resetAction;

        public RouterConfigHomeViewModel(Action quickSetupAction, Action advancedConfigAction, Action getHelpAction, Action resetAction)
        {
            _quickSetupAction = quickSetupAction;
            _advancedConfigAction = advancedConfigAction;
            _getHelpAction = getHelpAction;
            _resetAction = resetAction;
        }

        
        public ICommand ResetCommand
        {
            get
            {
                return new DelegateCommand(new Action<object>((sender) => _resetAction()));
            }
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
                return new DelegateCommand(new Action<object>((sender) => _advancedConfigAction()));
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
