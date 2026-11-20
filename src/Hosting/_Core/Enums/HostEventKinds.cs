using System;

namespace BindOpen.Hosting
{
    /// <summary>
    /// The interface defines a hosted item.
    /// </summary>
    [Flags]
    public enum HostBdoEventKinds
    {
        None = 0,

        OnInitSuccess = 0x1,

        OnInitFailure = 0x1 << 1,

        OnExecutionSucess = 0x1 << 2,

        OnExecutionFailure = 0x1 << 3,

        Any = OnInitSuccess | OnInitFailure | OnExecutionSucess | OnExecutionFailure
    }
}