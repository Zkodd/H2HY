﻿using System;

namespace H2HY.Services
{
    /// <summary>
    /// Opens viewmodels in windows or dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Opens given viewmodel as Dialog. Result on close will be true or false.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="callback"></param>
        void ShowDialog(ViewModelBase viewmodel, Action<bool> callback);

        /// <summary>
        /// Opens given viewmodel as window. No dialog result available.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="callback">Closed callback</param>
        void ShowWindow(ViewModelBase viewmodel, Action callback);
    }
}
