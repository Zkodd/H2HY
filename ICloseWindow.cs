using System;

namespace H2HY
{
    internal interface ICloseWindow
    { 
        /// <summary>
        /// The view hooks up into this event. Calling Close?.Invoke() will close the corresponding view.
        /// </summary>
        Action? Close { get; set; }

        /// <summary>
        /// Determinates if a window is closable. Default is true.
        /// </summary>
        public bool CanCloseWindow { get; set; }
    }
}