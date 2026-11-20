using BindOpen.Data;
using System.Linq;

namespace BindOpen.Logging;

public partial class LoggingDbContext : DataDbContext
{
    public ProcessExecutionDb GetProcessExecution(string identifier)
    {
        return ProcessExecutions
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public ProcessExecutionDb Upsert(IBdoProcessExecution poco)
    {
        if (poco == null) return default;

        var dbItem = GetProcessExecution(poco.Identifier);

        if (dbItem == null)
        {
            dbItem = poco.ToDb(this);
            Add(dbItem);
        }
        else
        {
            dbItem.UpdateFromPoco(poco, this);
        }

        return dbItem;
    }
}
