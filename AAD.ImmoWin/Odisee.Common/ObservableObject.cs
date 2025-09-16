using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Odisee.Common
{
    public class ObservableObject : INotifyPropertyChanged
    {
        #region Properties

        #region PropertyChanged propeties

        private Boolean _changed;
        public Boolean Changed
        {
            get { return _changed; }
            set
            {
                SetProperty(ref _changed, value);
            }
        }

        #endregion

        #region Event properties

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #endregion

        #region Methods

        protected virtual Boolean SetProperty<T>(ref T field, T value, [CallerMemberName] String propertyName = null)
        {
            if (!Object.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                Changed = true;
            }
            return Changed;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] String propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        #endregion
    }
}