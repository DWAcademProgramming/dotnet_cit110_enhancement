using System;
using System.Data;
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=false;User Id=sa;Password=SQLConnect1;";
            
            IDbConnection dbConnection = new SqlConnection(connectionString); 

            string sqlCommand ="SELECT GETDATE()"; 

            DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand); 
            Console.WriteLine(rightNow); 

            //Computer myComputer = new Computer()
            //{
            //    Motherboard = "z690",
            //    HasWifi = true,
            //    HasLTE = false, 
            //    ReleaseDate = DateTime.Now, 
            //    Price = 921.34m,  
            //    VideoCard = "Vx 2060"
            //};
            //Console.WriteLine(myComputer.Motherboard);
            //Console.WriteLine(myComputer.HasWifi); 
           // Console.WriteLine(myComputer.ReleaseDate); 
        }
    }
}
