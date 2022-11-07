using System;

namespace H2HY
{
    public interface ICloseWindow
    { 

        Action? Close { get; set; }

        public bool CanCloseWindow { get; set; }
    }
}