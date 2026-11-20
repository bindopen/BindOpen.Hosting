using BindOpen.Data.Meta;

namespace BindOpen.Hosting.Settings
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSettings : IBdoMetaWrapper
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoHostKernelSettings Kernel { get; set; }
    }
}