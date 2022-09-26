using System;

namespace H2HY.Toolkit
{
    /// <summary>
    /// Range class.
    /// </summary>
    /// <typeparam name="T">Generic parameter. Must implement IComparable</typeparam>
    public interface IRange<T> where T : IComparable<T>
    {
        /// <summary>
        /// minimum/start of range.
        /// </summary>
        T Low { get; }
        /// <summary>
        /// maximum/end of range.
        /// </summary>
        T High { get; }
        /// <summary>
        /// Determines if the provided value is inside or equal the range.
        /// /// </summary>
        /// <param name="otherValue">value to check</param>
        /// <returns>true if value is inside or equal the boundarys</returns>
        bool Includes(T otherValue);
        /// <summary>
        /// Determines if another range is inside or equal the bounds of this range.
        /// </summary>
        /// <param name="otherRange"></param>
        /// <returns>true if range is inside or equal the boundarys</returns>
        bool Includes(IRange<T> otherRange);
    }
}
