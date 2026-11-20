using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;

namespace BindOpen.Logging.Events;

/// <summary>
/// This class represents a Db converter of assembly references.
/// </summary>
public static class LogEventDbConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a database entity one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The database entity object.</returns>
    public static LogEventDb ToDb(
        this IBdoLogEvent poco,
        LoggingDbContext context)
    {
        if (poco == null) return null;

        LogEventDb dbItem = new();
        dbItem.UpdateFromPoco(poco, context);

        return dbItem;
    }

    public static LogEventDb UpdateFromPoco<T>(
        this T dbItem,
        IBdoLogEvent poco,
        LoggingDbContext context)
        where T : LogEventDb
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        poco.Identifier ??= StringHelper.NewGuid();

        dbItem.CreationDate = poco.CreationDate;
        dbItem.Criticality = poco.Criticality;
        dbItem.Date = poco.Date;
        dbItem.Description = poco.Description;
        dbItem.Kind = poco.Kind;
        dbItem.Identifier = poco.Identifier;
        dbItem.LastModificationDate = poco.LastModificationDate;
        dbItem.LongDescription = poco.LongDescription;
        dbItem.Name = poco.Name;
        dbItem.Source = poco.Source;
        dbItem.ResultCode = poco.ResultCode;
        dbItem.Title = poco.Title;

        if (context != null)
        {
            dbItem.Detail = context.Upsert(poco.Detail);
            dbItem.Log = context.Upsert(poco.Log);
        }

        return dbItem;
    }

    /// <summary>
    /// Converts an assembly reference database entity into a poco one.
    /// </summary>
    /// <param key="dbItem">The database entity to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoLogEvent ToPoco(
        this LogEventDb dbItem)
    {
        if (dbItem == null) return null;

        var poco = new BdoLogEvent();

        poco
            .WithCreationDate(dbItem.CreationDate)
            .WithCriticality(dbItem.Criticality)
            .WithDescription(dbItem.Description)
            .WithDate(dbItem.Date)
            .WithDetail(dbItem.Detail.ToPoco())
            .WithKind(dbItem.Kind)
            .WithLastModificationDate(dbItem.LastModificationDate)
            .WithLog(dbItem.Log.ToPoco())
            .WithLongDescription(dbItem.LongDescription)
            .WithId(dbItem.Identifier)
            .WithName(dbItem.Name)
            .WithResultCode(dbItem.ResultCode)
            .WithSource(dbItem.Source)
            .WithTitle(dbItem.Title);

        return poco;
    }
}
