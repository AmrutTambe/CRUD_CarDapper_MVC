using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CRUD_CarDapper_MVC.Models;
using Dapper;

namespace Crud_MVC_CarDapper.Models
{
    public class CarsRepository
    {
        private string connectionstring;

        public CarsRepository()
        {
            connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString;
        }

        public List<Car> GetAll(RequestModel request)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                    .Query<Car>("GetAllCars",
                    request,
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }
        }
        public Car Get(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                        .Query<Car>("Cars_Get",
                            new { Id },
                            commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
            }
        }
        public int Create(Car car)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                int lastInsertedId =
                    db.Query<int>("AddNewCarDetails",
                    car,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
                return lastInsertedId;
            }
        }
        public int Update(Car car)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                int recordAffected =
                 db.Query<int>
                    ("UpdateCarDetails",
                       car,
                       commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
                return recordAffected;
            }
        }
        public int Delete(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db.Execute(
                        "DeleteCarById",
                        new { Id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        internal void SaveStudents(Car s)
        {
            throw new NotImplementedException();
        }

        public Car Page(int Id)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                return db
                        .Query<Car>("Car_Paging",
                            new { Id },
                            commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
            }
        }
        //public int Page(int Id)
        //{
        //    using (IDbConnection db = new SqlConnection(connectionstring))
        //    {
        //        return db.Execute(
        //                "Car_Paging",
        //                new { Id },
        //                commandType: CommandType.StoredProcedure);
        //    }
        //}
        internal object Update(RequestModel request)
        {
            throw new NotImplementedException();
        }


        internal object GetAll(object request)
        {
            throw new NotImplementedException();
        }
        
    }
}
