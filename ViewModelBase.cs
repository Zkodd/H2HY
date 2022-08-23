using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace H2HY
{
    /// <summary>
    /// Base class for ALL Viewmodels. Realises INotifyPropertyChanged.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Property Changed event.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// abstract forces every derivation to implement a dispose function to avoid memory leaks.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// The attached view has been closed without a result. (modal or nonmodal)
        /// </summary>
        public virtual void ViewClosed()
        {
        }

        /// <summary>
        /// The attached modal view has been closed with the given result.
        /// </summary>
        /// <param name="dialogResult"></param>
        public virtual void ViewClosed(bool dialogResult)
        {
        }

        internal void DisposeAll()
        {
            // this is not possible "out of a package"
            //foreach (PropertyInfo propertyInfo in GetType().GetProperties().Where(p => p.PropertyType.IsSubclassOf(typeof(ViewModelBase))))
            //{
            //    MethodInfo m = propertyInfo.PropertyType.GetMethod(nameof(Dispose), new Type[0] { });
            //    _ = m.Invoke(propertyInfo.GetValue(this), new object[] { });
            //}

            Dispose();
        }

        /// <summary>
        /// Calls property Changed for the given Propertyname. Default is CallerMembername.
        /// </summary>
        /// <param name="propertyName">[CallerMemberName]</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises property changed for all properties.
        /// </summary>
        protected void RaisePropertyChangedAll()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }

        /// <summary>
        /// Sets a field and raises PropertyChanged in case of an change.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldName">ref to field</param>
        /// <param name="newValue">New value</param>
        /// <param name="propertyName">Default [CallerMemberName]</param>
        protected bool SetProperty<T>(ref T fieldName, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (Equals(fieldName, newValue))
            {
                return false;
            }

            fieldName = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
