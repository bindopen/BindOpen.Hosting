using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Objects;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;

namespace Microsoft.Extensions.Configuration
{
    /// <summary>
    /// This static class extends .Net core config.
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="config"></param>
        /// <param key="key"></param>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        public static T GetBdoValue<T>(
            this IConfiguration config,
            string key, IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : class
        {
            return config?.GetBdoValue<T>(key, default, scope, varSet, log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param key="config"></param>
        /// <param key="key"></param>
        /// <param key="defaultValue"></param>
        /// <param key="scope"></param>
        /// <param key="scriptVariableSet">The script variable set to consider.</param>
        /// <param key="log"></param>
        /// <returns></returns>
        public static T GetBdoValue<T>(
            this IConfiguration config,
            string key,
            string defaultValue,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : class
        {
            return config?.GetBdoValue(typeof(T), key, defaultValue, scope, varSet, log) as T;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="config"></param>
        /// <param key="type"></param>
        /// <param key="key"></param>
        /// <returns></returns>
        public static object GetBdoValue(
            this IConfiguration config,
            Type type,
            string key)
        {
            if (config == default) return default;
            return config.GetValue(type, key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="config"></param>
        /// <param key="type"></param>
        /// <param key="key"></param>
        /// <param key="defaultValue"></param>
        /// <param key="scope"></param>
        /// <param key="varSet">The script variable set to consider.</param>
        /// <param key="log"></param>
        /// <returns></returns>
        public static object GetBdoValue(
            this IConfiguration config,
            Type type,
            string key,
            string defaultValue,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (config == default) return default;
            var value = config.GetValue<string>(key, defaultValue);

            if (scope != null)
            {
                return Convert.ChangeType(scope.Interpreter?.Evaluate(
                    BdoData.NewExp(value, BdoExpressionKind.Script), varSet, log), type);
            }
            else if (type == typeof(string))
            {
                return Convert.ChangeType(value, type);
            }
            return default;
        }
    }
}