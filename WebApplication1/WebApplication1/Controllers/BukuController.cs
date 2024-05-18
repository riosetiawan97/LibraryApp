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
    public class BukuController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public BukuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select IdBuku, NamaBuku, Rak, Stok from
                            dbo.Buku
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
        public JsonResult Post(Buku buku)
        {
            string query = @"
                           insert into dbo.Buku
                           values (@NamaBuku, @Rak, @Stok)
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@NamaBuku", buku.NamaBuku);
                    myCommand.Parameters.AddWithValue("@Rak", buku.Rak);
                    myCommand.Parameters.AddWithValue("@Stok", buku.Stok);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }


        [HttpPut]
        public JsonResult Put(Buku buku)
        {
            string query = @"
                           update dbo.Buku
                           set NamaBuku= @NamaBuku,
                           Rak= @Rak,
                           Stok= @Stok
                            where IdBuku=@IdBuku
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdBuku", buku.IdBuku);
                    myCommand.Parameters.AddWithValue("@NamaBuku", buku.NamaBuku);
                    myCommand.Parameters.AddWithValue("@Rak", buku.Rak);
                    myCommand.Parameters.AddWithValue("@Stok", buku.Stok);
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
                           delete from dbo.Buku
                            where IdBuku=@IdBuku
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LibraryAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@IdBuku", id);

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
