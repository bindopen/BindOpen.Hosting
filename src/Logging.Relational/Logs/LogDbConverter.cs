using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging.Events;
using System.Linq;

namespace BindOpen.Logging;

/// <summary>
/// This class represents a Db converter of assembly references.
/// </summary>
public static class LogDbConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a database entity one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The database entity object.</returns>
    public static LogDb ToDb(
        this IBdoCompleteLog poco,
        LoggingDbContext context)
    {
        if (poco == null) return null;

        LogDb dbItem = new();
        dbItem.UpdateFromPoco(poco, context);

        return dbItem;
    }

    public static LogDb UpdateFromPoco<T>(
        this T dbItem,
        IBdoCompleteLog poco,
        LoggingDbContext context)
        where T : LogDb
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        poco.Identifier ??= StringHelper.NewGuid();

        dbItem.Description = poco.Description;
        dbItem.Identifier = poco.Identifier;
        dbItem.Name = poco.Name;
        dbItem.ResultCode = poco.ResultCode;
        dbItem.Title = poco.Title;

        if (context != null)
        {
            dbItem.Events ??= [];
            dbItem.Events.RemoveAll(q => poco._Events?.Any(p => p?.Identifier == q?.Identifier) != true);

            if (poco?._Events?.Count > 0)
            {
                foreach (var subItem in poco._Events)
                {
                    var dbSubItem = context.Upsert(subItem);

                    if (dbItem.Events.Any(p => p?.Identifier == dbSubItem?.Identifier) != true)
                    {
                        dbItem.Events.Add(dbSubItem);
                    }
                }
            }

            dbItem.Detail = context.Upsert(poco.Detail);
            dbItem.ProcessExecution = context.Upsert(poco.Execution);
            dbItem.TaskConfiguration = context.Upsert(poco.TaskConfig);
        }

        return dbItem;
    }

    /// <summary>
    /// Converts an assembly reference database entity into a poco one.
    /// </summary>
    /// <param key="dbItem">The database entity to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoCompleteLog ToPoco(
        this LogDb dbItem)
    {
        if (dbItem == null) return null;

        var poco = new BdoLog();

        poco
            .WithDescription(dbItem.Description)
            .WithDetail(dbItem.Detail.ToPoco())
            .WithExecution(dbItem.ProcessExecution.ToPoco())
            .WithEvents(dbItem.Events.Select(q => q.ToPoco()).ToArray())
            .WithId(dbItem.Identifier)
            .WithName(dbItem.Name)
            .WithResultCode(dbItem.ResultCode)
            .WithTask(dbItem.TaskConfiguration.ToPoco())
            .WithTitle(dbItem.Title);

        return poco;
    }
}
