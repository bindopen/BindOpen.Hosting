using BindOpen.Data;
using BindOpen.Logging.Events;
using System.Linq;

namespace BindOpen.Logging;

public partial class LoggingDbContext : DataDbContext
{
    public LogEventDb GetLogEvent(string identifier)
    {
        return LogEvents
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public LogEventDb Upsert(IBdoLogEvent poco)
    {
        if (poco == null) return default;

        var dbItem = GetLogEvent(poco.Identifier);

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
