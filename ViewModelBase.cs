
namespace H2HY;

/// <summary>
/// Base class for all view models.
/// </summary>
public abstract class ViewModelBase : NotifyPropertyChanged
{
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
}