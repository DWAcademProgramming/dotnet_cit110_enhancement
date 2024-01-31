using System;
using System.Data;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper(); 

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

           string sql = @"INSERT INTO TutorialAppSchema.Computer(
                Motherboard,
                HasWifi, 
                HasLTE, 
                ReleaseDate, 
                Price, 
                VideoCard
           ) VALUES('" + myComputer.Motherboard 
                    + "', + '" + myComputer.HasWifi
                    + "', + '" + myComputer.HasLTE
                    + "', + '" + myComputer.ReleaseDate
                    + "', + '" + myComputer.Price
                    + "', + '" + myComputer.VideoCard 
            +"')"; 
            Console.WriteLine(sql);
            //int result = dapper.ExecuteSqlWithRowCount(sql); 
            bool result = dapper.ExecuteSql(sql); 
            Console.WriteLine(result); 

            string sqlSelect = @"
            SELECT
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect); 

            foreach(Computer singleComputer in computers)
            {
                Console.WriteLine("'" +  myComputer.Motherboard 
                    + "', + '" + myComputer.HasWifi
                    + "', + '" + myComputer.HasLTE
                    + "', + '" + myComputer.ReleaseDate
                    + "', + '" + myComputer.Price
                    + "', + '" + myComputer.VideoCard 
                + "'"); 
            }
        }
    }
}
