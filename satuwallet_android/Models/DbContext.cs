using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace satuwallet_android.Models
{
    public static class DbContext
    {
        static SQLiteConnection database;

        public static void InitialiseDatabase()
        {
            var database = GetConnection();
            //using (TransactionScope tran = new TransactionScope())
            //{
            database.BeginTransaction();
            try
            {
                database.CreateTable<Token>();
                database.CreateTable<User>();
                database.CreateTable<PlatformRegistration>();
                database.CreateTable<UserRecord>();
                database.Commit();
            }
            catch (Exception ex)
            {
                database.Rollback();
                throw ex;
            }
            //    tran.Complete();
            //}
        }

        public static void ClearTable()
        {

            var database = DbContext.GetConnection();
            database.BeginTransaction();
            try
            {
                database.Execute("DELETE FROM Token");
                database.Execute("DELETE FROM User");
                database.Execute("DELETE FROM PlatformRegistration");
                database.Execute("DELETE FROM UserRecord");
                database.Commit();
            }
            catch (Exception ex)
            {
                database.Rollback();
                throw ex;
            }
        }

        public static SQLiteConnection GetConnection()
        {
            if (database == null)
            {
                var sqlLiteName = "satuwallet_localdb";
                string docPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                var path = Path.Combine(docPath, sqlLiteName);
                database = new SQLiteConnection(path);
                return database;
            }
            else
            {
                return database;
            }
        }

        //public SQLiteConnection GetDatabase()
        //{
        //    return database;
        //}
    }
}