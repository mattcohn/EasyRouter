using EasyRouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EasyRouter.ViewModels
{
    class RouterConfigViewModel : ViewModelBase
    {
        private Router _router;

        private ViewModelBase _contentViewModel;


        public RouterConfigViewModel(Router router)
        {
            _router = router;
            SetContentToConfigHomeViewModel();
        }

        private void SetContentToConfigHomeViewModel()
        {
            ContentViewModel = new RouterConfigHomeViewModel(
                OnQuickSetup,
                OnAdvancedConfig,
                OnGetHelp);
        }

        private void OnGetHelp()
        {
            // NOT IMPLEMENTED but don't crash the demo!
            //throw new NotImplementedException();
        }

        private void OnAdvancedConfig()
        {
            ContentViewModel = new AdvancedConfigurationViewModel(
                GetNetworkName,
                GetNetworkPassword);
        }

        private void OnQuickSetup()
        {
            ContentViewModel = new SetNetworkNameViewModel(
                GetNetworkName,
                SetNetworkName,
                OnSetNetworkNameNext);
        }

        private void OnSetNetworkNameNext()
        {
            ContentViewModel = new SetNetworkPasswordViewModel(
                GetNetworkPassword,
                SetNetworkPassword,
                OnSetNetworkPasswordNext);
        }

        private void OnSetNetworkPasswordNext()
        {
            ContentViewModel = new QuickSetupDoneViewModel(
                OnQuickSetupNext);
        }

        private void OnQuickSetupNext()
        {
            SetContentToConfigHomeViewModel();
        }

        private void SetNetworkPassword(string networkPassword)
        {
            _router.ChangeWifiPassword(networkPassword);
        }

        private string GetNetworkPassword()
        {
            return _router.GetWifiPassword();
        }

        private void SetNetworkName(string networkName)
        {
            _router.ChangeSSID(networkName);
        }

        private void ResetRouter()
        {
            _router.Reset();
        }

        private string GetNetworkName()
        {
            return _router.GetSSID();
        }

        public string RouterModel
        {
            get
            {
                return _router.Model;
            }
        }

        public string RouterImageUri
        {
            get
            {
                return "pack://application:,,,/Content/" + _router.ImageFilename;
            }
        }

        public ICommand RouterClickedCommand
        {
            get
            {
                return new DelegateCommand(new Action<object>((sender) => SetContentToConfigHomeViewModel()));
            }
        }

        public ViewModelBase ContentViewModel
        {
            get
            {
                return _contentViewModel;
            }

            set
            {
                if (_contentViewModel != value)
                {
                    _contentViewModel = value;
                    OnPropertyChanged("ContentViewModel");
                }
            }
        }
    }
}
