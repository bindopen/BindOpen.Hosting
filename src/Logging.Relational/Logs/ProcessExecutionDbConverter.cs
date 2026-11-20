using BindOpen.Data;
using BindOpen.Data.Helpers;

namespace BindOpen.Logging;

/// <summary>
/// This class represents a Db converter of assembly references.
/// </summary>
public static class ProcessExecutionDbConverter
{
    /// <summary>
    /// Converts an assembly reference poco into a database entity one.
    /// </summary>
    /// <param key="poco">The poco to consider.</param>
    /// <returns>The database entity object.</returns>
    public static ProcessExecutionDb ToDb(
        this IBdoProcessExecution poco,
        LoggingDbContext context)
    {
        if (poco == null) return null;

        ProcessExecutionDb dbItem = new();
        dbItem.UpdateFromPoco(poco, context);

        return dbItem;
    }

    public static ProcessExecutionDb UpdateFromPoco<T>(
        this T dbItem,
        IBdoProcessExecution poco,
        LoggingDbContext context)
        where T : ProcessExecutionDb
    {
        if (dbItem == null) return null;

        if (poco == null) return dbItem;

        poco.Identifier ??= StringHelper.NewGuid();

        dbItem.CustomStatus = poco.CustomStatus;
        dbItem.EndDate = poco.EndDate;
        dbItem.Identifier = poco.Identifier;
        dbItem.Location = poco.Location;
        dbItem.ProgressIndex = poco.ProgressIndex;
        dbItem.ProgressMax = poco.ProgressMax;
        dbItem.RestartDate = poco.RestartDate;
        dbItem.ResultLevel = poco.ResultLevel;
        dbItem.StartDate = poco.StartDate;
        dbItem.State = poco.State;
        dbItem.Status = poco.Status;

        return dbItem;
    }

    /// <summary>
    /// Converts an assembly reference database entity into a poco one.
    /// </summary>
    /// <param key="dbItem">The database entity to consider.</param>
    /// <returns>The poco object.</returns>
    public static IBdoProcessExecution ToPoco(
        this ProcessExecutionDb dbItem)
    {
        if (dbItem == null) return null;

        var poco = new BdoProcessExecution();

        poco
            .WithCustomStatus(dbItem.CustomStatus)
            .WithEndDate(dbItem.EndDate)
            .WithId(dbItem.Identifier)
            .WithLocation(dbItem.Location)
            .WithProgressIndex(dbItem.ProgressIndex)
            .WithProgressMax(dbItem.ProgressMax)
            .WithRestartDate(dbItem.RestartDate)
            .WithResultLevel(dbItem.ResultLevel)
            .WithStartDate(dbItem.StartDate)
            .WithState(dbItem.State)
            .WithStatus(dbItem.Status);

        return poco;
    }
}
