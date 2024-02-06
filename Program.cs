using System;
using System.Data;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build(); 

            DataContextDapper dapper = new DataContextDapper(config); 
            DataContextEF entityFramework = new DataContextEF(config); 

            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()"); 
            Console.WriteLine(rightNow); 

            Computer myComputer = new Computer()
            {
                Motherboard = "z690",
                HasWifi = true,
                HasLTE = false, 
                ReleaseDate = DateTime.Now, 
                Price = 921.34m,  
                VideoCard = "Vx 2060"
            };
            //Console.WriteLine(myComputer.Motherboard);
            //Console.WriteLine(myComputer.HasWifi); 
           // Console.WriteLine(myComputer.ReleaseDate); 

           entityFramework.Add(myComputer); 
           entityFramework.SaveChanges(); 

         string sql = @"INSERT INTO TutorialAppSchema.Computer (
            Motherboard,
            HasWifi,
            HasLTE,
            ReleaseDate,
            Price,
            Videocard
            ) VALUES ('" + myComputer.Motherboard
                     + "','" + myComputer.HasWifi
                     + "','" + myComputer.HasLTE
                     + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
                     + "','" + myComputer.Price
                     + "','" + myComputer.VideoCard 
                +"')";
            Console.WriteLine(sql);
            //int result = dapper.ExecuteSqlWithRowCount(sql); 
            bool result = dapper.ExecuteSql(sql); 
            Console.WriteLine(result); 

            string sqlSelect = @"
            SELECT
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect); 

            IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>(); 

            if(computersEf != null)
            {
                foreach(Computer singleComputer in computers)
                {
                    Console.WriteLine("'" +  myComputer.ComputerId
                        + "', + '" + singleComputer.Motherboard
                        + "', + '" + singleComputer.HasWifi
                        + "', + '" + singleComputer.HasLTE
                        + "', + '" + singleComputer.ReleaseDate
                        + "', + '" + singleComputer.Price
                        + "', + '" + singleComputer.VideoCard 
                    + "'"); 
                }
            }
        }
    }
}
