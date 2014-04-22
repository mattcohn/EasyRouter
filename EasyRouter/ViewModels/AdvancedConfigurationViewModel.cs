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
        private Action
            _resetAction;

        public AdvancedConfigurationViewModel(Action resetAction)
        {
            _resetAction = resetAction;
        }

        public ICommand ResetCommand
        {
            get
            {
                return new DelegateCommand(new Action<object>((sender) => _resetAction()));
            }
        }
    }
}
