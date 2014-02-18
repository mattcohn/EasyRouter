using EasyRouter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyRouter.ViewModels
{
    class ConnectingViewModel
    {
        private Router _router;

        public ConnectingViewModel(Router router)
        {
            _router = router;
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
    }
}
