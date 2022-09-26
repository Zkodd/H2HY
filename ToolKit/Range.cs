using System;

namespace H2HY.Toolkit
{
    /// <summary>
    /// Range class.
    /// </summary>
    /// <typeparam name="T">Generic parameter. Must implement IComparable</typeparam>
    public class Range<T> : IRange<T> where T : IComparable<T>
    {
        /// <summary>
        /// minimum/start of range.
        /// </summary>
        public T Low { get; set; }

        /// <summary>
        /// maximum/end of range.
        /// </summary>
        public T High { get; set; }

        /// <summary>
        /// Determines if the provided value is inside or equal the range.
        /// /// </summary>
        /// <param name="otherValue">value to check</param>
        /// <returns>true if value is inside or equal the boundarys</returns>
        public bool Includes(T otherValue)
        {
            return (Low.CompareTo(otherValue) <= 0) && (otherValue.CompareTo(High) <= 0);
        }

        /// <summary>
        /// Determines if another range is inside or equal the bounds of this range.
        /// </summary>
        /// <param name="otherRange"></param>
        /// <returns>true if range is inside or equal the boundarys</returns>
        public bool Includes(IRange<T> otherRange)
        {
            return Includes(otherRange.Low) && Includes(otherRange.High);
        }

        /// <summary>
        /// string representation of the range.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0} - {1}]", Low, High);
        }
    }
}
