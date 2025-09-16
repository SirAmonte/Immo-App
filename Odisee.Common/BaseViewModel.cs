using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odisee.Common
{
    public abstract class BaseViewModel: ObservableObject
    {
        private string title;
        private bool isEnabled;

        public string Title { get => title; set => SetProperty(ref title, value); }

        public bool IsEnabled { get => isEnabled; set => SetProperty(ref isEnabled, value); }
    }
}
