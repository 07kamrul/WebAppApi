using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebAppApi.Models;

namespace WebAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT EmployeeId, EmployeeName, Department,
                            convert(varchar(10),DateOfJoining,120) as DateOfJoining, 
                            PhotoFileName FROM dbo.Employee";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCmd = new SqlCommand(query, myConn))
                {
                    myReader = myCmd.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }

            //var data = new Dictionary<string, object>();
            //data.Add("data",table);

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"insert into dbo.Employee (EmployeeName, Department, DateOfJoining, PhotoFileName)
                            values (
                                    '" + emp.EmployeeName + @"',
                                    '" + emp.Department + @"',
                                    '" + emp.DateOfJoining + @"',
                                    '" + emp.PhotoFileName + @"'
                                    )";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCmd = new SqlCommand(query, myConn))
                {
                    myReader = myCmd.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }

            var data = new Dictionary<string, object>();
            data.Add("message", "Added Successfully");

            return new JsonResult(data);
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @" update dbo.Employee set 
                              EmployeeName = '" + emp.EmployeeName + @"'
                              , Department = '" + emp.Department + @"'
                              , DateOfJoining = '" + emp.DateOfJoining + @"'
                              where EmployeeId = " + emp.EmployeeId + @"
                            ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCmd = new SqlCommand(query, myConn))
                {
                    myReader = myCmd.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }

            var data = new Dictionary<string, object>();
            data.Add("message", "Update Successfully");

            return new JsonResult(data);
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @" delete from dbo.Employee where EmployeeId = " + id + @" ";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myCmd = new SqlCommand(query, myConn))
                {
                    myReader = myCmd.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }
            }

            var data = new Dictionary<string, object>();
            data.Add("message", "Delete Successfully");

            return new JsonResult(data);
        }
    }
}
