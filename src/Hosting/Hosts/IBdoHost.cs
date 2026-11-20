using BindOpen.Hosting.Settings;
using BindOpen.Logging;
using BindOpen.Logging.Loggers;
using BindOpen.Scoping;
using System;

namespace BindOpen.Hosting
{
    /// <summary>
    /// The interface defines the base bot.
    /// </summary>
    public interface IBdoHost : IBdoScope, IBdoLoggerTracked
    {
        ProcessExecutionState State { get; }

        IBdoHostOptions Options { get; set; }

        void Start();

        void Stop();

        // Trigger actions --------------------------------------

        event EventHandler OnInitSucceeds;

        event EventHandler OnInitFails;

        event EventHandler OnExecutionSucceeds;

        event EventHandler OnExecutionFails;
    }
}