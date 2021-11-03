using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Test3.Models;

namespace Test3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                        SELECT * FROM `todos`";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TodoAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Todo todo)
        {
            string query = @"
                       INSERT INTO `todos`(Nombre, Completado) values (@Nombre, @Estado);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TodoAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Nombre", todo.Nombre);
                    myCommand.Parameters.AddWithValue("@Estado", false);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public JsonResult Put(Todo todo)
        {
            string query = @"
                       update `todos` set
                      Nombre = @Nombre,
                        Estado = @Estado
                        where Id_todo =@Id_todo;";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TodoAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Nombre", todo.Nombre);
                    myCommand.Parameters.AddWithValue("@NombreDeUsuario", todo.Completado);
                    myCommand.Parameters.AddWithValue("@Id_todo", todo.Id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");

        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                       delete from `todos`
                        where Id_todo =@Id_todo;";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("TodoAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@Id_todo", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");

        }

        /*[HttpGet("{id}")]
        public JsonResult GetId(int id)
        {
            string query = @"
                        SELECT * FROM `usuarios` WHERE Id_Usuarios = " + id;

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UserAppCon");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }*/
    }
}
