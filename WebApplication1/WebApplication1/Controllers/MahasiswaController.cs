using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahasiswaController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public MahasiswaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select IdMahasiswa, NIM, NamaMahasiswa, IsActive from
                            dbo.Mahasiswa
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myCommand=new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Mahasiswa Mahasiswa)
        {
            string query = @"
                           insert into dbo.Mahasiswa
                           values (@NIM, @NamaMahasiswa, @IsActive)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NIM", Mahasiswa.NIM);
                    myCommand.Parameters.AddWithValue("@NamaMahasiswa", Mahasiswa.NamaMahasiswa);
                    myCommand.Parameters.AddWithValue("@IsActive", Mahasiswa.IsActive);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Mahasiswa Mahasiswa)
        {
            string query = @"
                           update dbo.Mahasiswa
                           set NIM= @NIM,
                           NamaMahasiswa= @NamaMahasiswa,
                           IsActive= @IsActive
                            where IdMahasiswa=@IdMahasiswa
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdMahasiswa", Mahasiswa.IdMahasiswa);
                    myCommand.Parameters.AddWithValue("@NIM", Mahasiswa.NIM);
                    myCommand.Parameters.AddWithValue("@NamaMahasiswa", Mahasiswa.NamaMahasiswa);
                    myCommand.Parameters.AddWithValue("@IsActive", Mahasiswa.IsActive);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Mahasiswa
                            where IdMahasiswa=@IdMahasiswa
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdMahasiswa", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }


    }
}
