using QDev.GamePointer.Abstract;
using SQLitePCL;

namespace QDev.GamePointer.Persist
{
    public class AppData
    {
        public async static void Initialize(IDbPath dbPath)
        {
            raw.SetProvider(new SQLite3Provider_e_sqlite3());
            using (var db = new WatchedExecutionContext(dbPath))
                await db.Database.EnsureCreatedAsync();
        }
    }
}
