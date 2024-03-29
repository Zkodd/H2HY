﻿using System;

namespace H2HY.Tools
{

    /// <summary>
    /// Exception handler call back.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Method to handle a thrown exception
        /// </summary>
        /// <param name="ex"></param>
        void HandelException(Exception ex);
    }
}
