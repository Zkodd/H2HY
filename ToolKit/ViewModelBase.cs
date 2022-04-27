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

        internal void DisposeAll()
        {
            // not possible "out of a package"
            //foreach (PropertyInfo propertyInfo in GetType().GetProperties().Where(p => p.PropertyType.IsSubclassOf(typeof(ViewModelBase))))
            //{
            //    MethodInfo m = propertyInfo.PropertyType.GetMethod(nameof(Dispose), new Type[0] { });
            //    _ = m.Invoke(propertyInfo.GetValue(this), new object[] { });
            //}

            Dispose();
        }


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

        /// <summary>
        /// The attached view has been closed without. (modal or nonmodal)
        /// </summary>
        public virtual void ViewClosed()
        {
            // not possible "out of a package"
            //foreach (PropertyInfo propertyInfo in GetType().GetProperties().Where(p => p.PropertyType.IsSubclassOf(typeof(ViewModelBase))))
            //{
            //    MethodInfo m = propertyInfo.PropertyType.GetMethod(nameof(ViewClosed), new Type[0] { });
            //    _ = m.Invoke(propertyInfo.GetValue(this), new object[] { });
            //}
        }

        /// <summary>
        /// The attached modal view has been closed with an result.
        /// </summary>
        /// <param name="dialogResult"></param>
        public virtual void ViewClosed(bool dialogResult)
        {
        }
    }
}
