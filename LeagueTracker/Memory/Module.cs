﻿using System;
using System.Diagnostics;

namespace LeagueTracker.Memory
{
    public class Module : IDisposable
    {
        #region // storage

        /// <inheritdoc cref="Process"/>
        public Process Process { get; private set; }

        /// <inheritdoc cref="ProcessModule"/>
        public ProcessModule ProcessModule { get; private set; }

        #endregion

        #region // ctor

        /// <summary />
        public Module(Process process, ProcessModule processModule)
        {
            Process = process;
            ProcessModule = processModule;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Process = default;

            ProcessModule.Dispose();
            ProcessModule = default;
        }

        #endregion
    }

}