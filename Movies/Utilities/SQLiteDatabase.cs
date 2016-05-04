using SQLite;
using System.Threading.Tasks;

namespace Movies.Utilities
{
    public static class SQLiteDatabase
    {
        public static string createDatabase(string path)
        {
            try
            {
                var connection = new SQLiteAsyncConnection(path);
                {
                    connection.CreateTableAsync<Movie>();
                    return "Database created";
                }
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public static async Task<string> insertUpdateData(Movie data, string path)
        {
            try
            {
                //don't insert existing records
                var db = new SQLiteAsyncConnection(path);
                var repeats = await db.Table<Movie>().Where(v => v.movieID == data.movieID).CountAsync();
                    
                if (repeats == 0 && await db.InsertAsync(data) != 0)
                {
                    await db.UpdateAsync(data);
                    return "Single data file inserted or updated";
                }
                else 
                {
                    return "existing data found. no updates";
                }
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public static async Task<string> deleteData(Movie data, string path)
        {
            try
            {
                //don't insert existing records
                var db = new SQLiteAsyncConnection(path);
                var repeats = await db.Table<Movie>().Where(v => v.movieID == data.movieID).ToListAsync();

                if (repeats.Count != 0)
                {
                    await db.DeleteAsync(repeats[0]);
                    await db.UpdateAsync(data);
                    return "Single data file deleted";
                }
                else
                {
                    return "no deletion happened";
                }
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public static async Task<System.Collections.Generic.List<Movie>> getData(string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);

                var repeats = await db.Table<Movie>().ToListAsync();
                return repeats;     
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public static async Task<System.Collections.Generic.List<Movie>> getDataById(string id, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);

                var repeats = await db.Table<Movie>().Where(v => v.movieID == id).ToListAsync();
                return repeats;
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

    }
}