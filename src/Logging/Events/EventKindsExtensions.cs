//using Microsoft.Extensions.Logging;
//using System.Diagnostics.Tracing;

//namespace BindOpen.Logging.Events
//{
//    /// <summary>
//    /// This static class provides extensions to BdoEventKinds enumeration.
//    /// </summary>
//    public static class BdoEventKindsExtensions
//    {
//        public static EventLevel? ToNetEventLevel(BdoEventKinds bdoLevel)
//        {
//            return bdoLevel switch
//            {
//                BdoEventKinds.Always => EventLevel.LogAlways,
//                BdoEventKinds.Debug => EventLevel.Verbose,
//                BdoEventKinds.Error => EventLevel.Error,
//                BdoEventKinds.Exception => EventLevel.Critical,
//                BdoEventKinds.Message => EventLevel.Informational,
//                BdoEventKinds.None => null,
//                BdoEventKinds.Verbose => EventLevel.Verbose,
//                BdoEventKinds.Warning => EventLevel.Warning,
//                _ => null,
//            };
//        }

//        public static BdoEventKinds ToBdoEventLevel(EventLevel level)
//        {
//            return level switch
//            {
//                EventLevel.Critical => BdoEventKinds.Exception,
//                EventLevel.Error => BdoEventKinds.Error,
//                EventLevel.Informational => BdoEventKinds.Message,
//                EventLevel.LogAlways => BdoEventKinds.Always,
//                EventLevel.Verbose => BdoEventKinds.Verbose,
//                EventLevel.Warning => BdoEventKinds.Warning,
//                _ => BdoEventKinds.None,
//            };
//        }

//        public static LogLevel ToNetLogLevel(BdoEventKinds bdoLevel)
//        {
//            return bdoLevel switch
//            {
//                BdoEventKinds.Always => LogLevel.Trace,
//                BdoEventKinds.Debug => LogLevel.Debug,
//                BdoEventKinds.Error => LogLevel.Error,
//                BdoEventKinds.Exception => LogLevel.Critical,
//                BdoEventKinds.Message => LogLevel.Information,
//                BdoEventKinds.None => LogLevel.None,
//                BdoEventKinds.Verbose => LogLevel.Trace,
//                BdoEventKinds.Warning => LogLevel.Warning,
//                _ => LogLevel.None,
//            };
//        }

//        public static BdoEventKinds ToBdoLogLevel(LogLevel level)
//        {
//            return level switch
//            {
//                LogLevel.Critical => BdoEventKinds.Exception,
//                LogLevel.Debug => BdoEventKinds.Debug,
//                LogLevel.Error => BdoEventKinds.Error,
//                LogLevel.Information => BdoEventKinds.Message,
//                LogLevel.None => BdoEventKinds.None,
//                LogLevel.Trace => BdoEventKinds.Verbose,
//                LogLevel.Warning => BdoEventKinds.Warning,
//                _ => BdoEventKinds.None,
//            };
//        }
//    }
//}