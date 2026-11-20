using BindOpen.Data;
using System.Linq;

namespace BindOpen.Logging;

public partial class LoggingDbContext : DataDbContext
{
    public LogDb GetLog(string identifier)
    {
        return Logs
            .FirstOrDefault(q => q.Identifier == identifier);
    }

    public LogDb Upsert(IBdoCompleteLog poco)
    {
        if (poco == null) return default;

        var dbItem = GetLog(poco.Identifier);

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
