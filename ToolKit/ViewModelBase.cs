using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace H2HY
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// abstract forces every derivation to implement a dispose function to avoid memory leaks.
        /// </summary>
        public abstract void Dispose();

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChangedAll()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }

        protected void SetProperty<T>(ref T fieldName, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(fieldName, newValue))
            {
                return;
            }

            fieldName = newValue;
            RaisePropertyChanged(propertyName);
        }

        public virtual void ViewClosed()
        {
        }

        public virtual void ViewClosed(bool dialogResult)
        {
        }
    }
}
