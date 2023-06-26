namespace H2HY.Models
{
    /// <summary>
    /// An ID for every Model, which likes to be stored in a database, for example.
    /// </summary>
    public interface IIDInterface
    {
        /// <summary>
        /// Unique Identifier.
        /// </summary>
        int Id { get; set; }
    }
}

