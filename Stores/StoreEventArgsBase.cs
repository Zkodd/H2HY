using System;

namespace H2HY.Stores
{
    public abstract class StoreEventArgsBase<T, TAction> : EventArgs
    {
        /// <summary>
        /// Affected Value
        /// </summary>
        public T? Value { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value">affected value</param>
        /// <param name="action">performed action</param>
        public StoreEventArgsBase(T? value, TAction action)
        {
            Value = value;
            Action = action;
        }

        /// <summary>
        /// performed action
        /// </summary>
        public TAction Action { get; }
    }
}