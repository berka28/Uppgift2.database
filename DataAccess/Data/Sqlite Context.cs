using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataAccess.Data
{
    

    public static class SqliteContext
    {
        #region Configuration and Properties

        private static string _dbpath { get; set; }

        public static async void UseSQLite(string databaseName = "sqlite.db")
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(databaseName, CreationCollisionOption.OpenIfExists);
            _dbpath = $"Filename={Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseName)}";

            using (var db = new SqliteConnection(_dbpath))
            {
                db.Open();
                var query = "CREATE TABLE IF NOT EXISTS Customers (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name TEXT NOT NULL, Created DATETIME NOT NULL); CREATE TABLE IF NOT EXISTS Issues (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, CustomerId INTEGER NOT NULL, Title TEXT NOT NULL, Description TEXT NOT NULL, Status TEXT NOT NULL, Created DATETIME NOT NULL, FOREIGN KEY (CustomerId) REFERENCES Customers(Id)); CREATE TABLE IF NOT EXISTS Comments (Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, IssueId INTEGER NOT NULL, Description TEXT NOT NULL, Created DATETIME NOT NULL, FOREIGN KEY (IssueId) REFERENCES Issues(Id));";
                var cmd = new SqliteCommand(query, db);

                await cmd.ExecuteNonQueryAsync();
                db.Close();
            }
        }
    }
        #endregion


}
