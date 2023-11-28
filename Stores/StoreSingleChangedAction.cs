namespace H2HY.Stores;

/// <summary>
/// Indicates a performed action on an single item.
/// </summary>
public enum StoreSingleChangedAction
{
    /// <summary>
    /// item needs initialisation
    /// </summary>
    Initialise = 0,
    /// <summary>
    /// item has been set
    /// </summary>
    Set = 1,
    /// <summary>
    /// item has changed
    /// </summary>
    Changed = 2,
    /// <summary>
    /// item has been rested to default.
    /// </summary>
    Reset = 3,
}