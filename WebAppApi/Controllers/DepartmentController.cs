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
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT DepartmentId, DepartmentName FROM dbo.Department";

            DataTable table = new DataTable();

            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader myReader;

            using(SqlConnection myConn = new SqlConnection(sqlDataSource))
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
        public JsonResult Post(Department dep)
        {
            string query = @"insert into dbo.Department values ('"+dep.DepartmentName+@"')";

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
        public JsonResult Put(Department dep)
        {
            string query = @" update dbo.Department 
                              set DepartmentName = '" + dep.DepartmentName + @"'
                              where DepartmentId = " + dep.DepartmentId + @"
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
            string query = @" delete from dbo.Department where DepartmentId = " + id + @" ";

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
