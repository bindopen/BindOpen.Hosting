using System;

namespace BindOpen.Hosting
{
    /// <summary>
    /// The interface defines a hosted item.
    /// </summary>
    [Flags]
    public enum ConfigurationFileExtenions
    {
        None = 0,

        Json = 0x1,

        Xml = 0x1 >> 1,

        Any = Json | Xml
    }
}