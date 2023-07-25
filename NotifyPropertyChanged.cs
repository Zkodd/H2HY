using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace H2HY;

/// <summary>
/// Realises INotifyPropertyChanged.
/// </summary>
public abstract class NotifyPropertyChanged : INotifyPropertyChanged
{
    /// <summary>
    /// Property Changed event.
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Calls property Changed for the given property name. Default is CallerMemberName.
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
        if (EqualityComparer<T>.Default.Equals(fieldName, newValue))
        {
            return false;
        }

        fieldName = newValue;
        RaisePropertyChanged(propertyName);
        return true;
    }
}
