using MySql.Data.MySqlClient;
using n01605783cumulative1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace n01605783cumulative1.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDdContext SchoolDb = new SchoolDdContext();

        //This Controller Will access the authors table of our blog database.
        /// <summary>
        /// Returns a list of Authors in the system
        /// </summary>
        /// <example>GET api/AuthorData/ListAuthors</example>
        /// <returns>
        /// A list of authors (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeacherData")]
        public IEnumerable<Teacher> ListTeacherData()
        {
            //Create a connection
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            //Open the connection
            Conn.Open();

            //create a command 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of teacher names 
            List<Teacher> Teachers = new List<Teacher>{};

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                string TeacherSalary = ResultSet["salary"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                
                Teacher NewTeacher = new Teacher();
                /*string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                string TeacherSalary = ResultSet["salary"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);*/
                NewTeacher.TeacherFirstName = TeacherFname;
                NewTeacher.TeacherLastName = TeacherLname;
                NewTeacher.TeacherHireDate = TeacherHireDate;
                NewTeacher.TeacherSalary = TeacherSalary;
                NewTeacher.TeacherId = TeacherId;

                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Teachers;
        }

        // GET api/TeacherData/FindTeacher/{TeacherID} -> {"TeacherID"}
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherID}")]
        public Teacher FindTeacher(int TeacherID)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            //Open the connection
            Conn.Open();

            //create a command 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherid=" + TeacherID;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            Teacher SelectedTeacher = new Teacher();

            while (ResultSet.Read()) {
                /*string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                string TeacherSalary = ResultSet["salary"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);*/
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFirstName = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLastName = ResultSet["teacherlname"].ToString();
                SelectedTeacher.TeacherSalary = ResultSet["salary"].ToString();
                SelectedTeacher.TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
            }

            Conn.Close();
            return SelectedTeacher;
        }
    }
}
