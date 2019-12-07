using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace photo_printing_dispatcher
{
    public class DatabaseManager
    {
        private readonly string _connectionString;
        
        private const string SelectNotPrintedPhotosQuery = "select * from Photos where datePrinted is null ";
        private const string UpdateDatePrinted = "update Photos set datePrinted = @datePrinted where id = @id";

        public DatabaseManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IEnumerable<Photo> GetAllPhotos()
        {
            List<Photo> notPrintedPhotos;
            
            using(var connection = new MySqlConnection(_connectionString))
            {
                notPrintedPhotos = connection.Query<Photo>(SelectNotPrintedPhotosQuery).ToList();
            }
            
            Console.WriteLine($"[{DateTime.Now}] Found {notPrintedPhotos.Count} new photos to print");
            return notPrintedPhotos;
        }

        public void UpdatePhoto(Photo photo)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Execute(UpdateDatePrinted, new
                {
                    datePrinted = photo.DatePrinted,
                    id = photo.Id
                });
            }
            
            Console.WriteLine($"[{DateTime.Now}] Updated photo [id:{photo.Id}] with datePrinted [{photo.DatePrinted}]");
        }
    }
}