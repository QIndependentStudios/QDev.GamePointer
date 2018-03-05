using SQLitePCL;

namespace QDev.GamePointer.Persist
{
    public class AppData
    {
        public async static void Initialize()
        {
            raw.SetProvider(new SQLite3Provider_e_sqlite3());
            using (var db = new WatchedExecutionContext())
                await db.Database.EnsureCreatedAsync();
        }
    }
}
