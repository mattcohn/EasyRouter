using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyRouter.ViewModels
{
    class QuickSetupDoneViewModel : ViewModelBase
    {
        private Action _quickSetupNext;
        public QuickSetupDoneViewModel(
            Action quickSetupNext)
        {
            _quickSetupNext = quickSetupNext;
        }

        public ICommand NextCommand
        {
            get
            {
                return new DelegateCommand(new Action<object>((sender) => _quickSetupNext()));
            }
        }
    }
}
