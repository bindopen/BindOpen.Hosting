using BindOpen.Data.Meta;
using BindOpen.Hosting.Settings;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// This class represents an data source extensions.
    /// </summary>
    public static class BdoDatasourceDepotExtensions
    {
        /// <summary>
        /// Adds sources from connection strings.
        /// </summary>
        /// <param key="depot">The datasource depot to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="keyName">The key name to consider.</param>
        public static IBdoDatasourceDepot AddFromConnectionStrings<T>(
            this T depot,
            IConfiguration config,
            string keyName = "connectionStrings")
            where T : IBdoDatasourceDepot
        {
            if (depot != null && config != null)
            {
                var sections = config.GetSection(keyName).GetChildren();
                foreach (var section in sections)
                {
                    var datasource = BdoData.NewMetaWrapper<BdoDatasource>(
                        depot.Scope,
                        BdoData.NewSet(
                            (IBdoDatasource.__ConnectionString_DatasourceKind, DatasourceKind.Database),
                            (IBdoDatasource.__ConnectionString_Token, section.Value)))
                        .WithName(section.Key);
                    depot.Add(datasource);
                }
            };

            return depot;
        }

        /// <summary>
        /// Adds sources from connection strings.
        /// </summary>
        /// <param key="depot">The datasource depot to consider.</param>
        /// <param key="config">The config to consider.</param>
        /// <param key="keyName">The key name to consider.</param>
        public static IBdoDatasourceDepot AddFromSettings<T>(
            this T depot,
            IBdoSettings settings,
            params object[] tokens)
            where T : IBdoDatasourceDepot
        {
            if (tokens?.Any() != true)
            {
                tokens = new object[] { "^$datasources" };
            }

            if (depot != null && settings != null)
            {
                var meta = settings.Detail?.Descendant<IBdoConfiguration>(tokens);

                if (meta is IBdoConfiguration subConfig)
                {
                    foreach (var childMeta in subConfig)
                    {
                        var set = BdoData.NewSet().With(childMeta?.ToArray());

                        var source = depot.Scope.NewMetaWrapper<BdoDatasource>(set);
                        source.WithName(childMeta.Name);
                        source.WithDataType(childMeta.DataType);
                        depot.Add(source);
                    }
                }
            };

            return depot;
        }
    }
}