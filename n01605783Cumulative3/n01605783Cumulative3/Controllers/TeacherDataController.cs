using MySql.Data.MySqlClient;
using n01605783Cumulative3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Diagnostics;

namespace n01605783Cumulative3.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext SchoolDb = new SchoolDbContext();

        //This Controller Will access the teachers table of our blog database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <return>
        /// A list of teacher objects with teacher name containing the search key
        /// </return>
        /// <example>GET api/TeacherData/ListTeacherData</example>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        /// <param name="TeacherSearchKey">The teachers to search for</param>
        [HttpGet]
        [Route("api/TeacherData/ListTeacherData/{TeacherSearchKey?}")]
        //[EnableCors(origins: "*", methods: "*", headers: "*")]
        public IEnumerable<Teacher> ListTeacherData(string TeacherSearchKey = null)
        {

            Debug.WriteLine("trying to do an api search for " +  TeacherSearchKey);
            //Create a connection
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            //Open the connection
            Conn.Open();

            //create a command 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from teachers where teacherfname like @key or teacherlname like @key";

            //sanitizing the teacher serach key input
            cmd.Parameters.AddWithValue("@key", "%" + TeacherSearchKey + "%");

            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of teacher names 
            List<Teacher> Teachers = new List<Teacher>();

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            { 
                // What is the Teacher name we want to get?
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                DateTime TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                string TeacherSalary = ResultSet["salary"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();

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
                NewTeacher.TeacherEmployeeNumber = TeacherEmployeeNumber;

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

            while (ResultSet.Read())
            {
                SelectedTeacher.TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                SelectedTeacher.TeacherFirstName = ResultSet["teacherfname"].ToString();
                SelectedTeacher.TeacherLastName = ResultSet["teacherlname"].ToString();
                SelectedTeacher.TeacherSalary = ResultSet["salary"].ToString();
                SelectedTeacher.TeacherHireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                SelectedTeacher.TeacherEmployeeNumber = ResultSet["employeenumber"].ToString();
            }

            Conn.Close();
            return SelectedTeacher;
        }

        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFirstName);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, hiredate, salary, employeenumber) values (@TeacherFirstName,@TeacherLastName,@TeacherHireDate, @TeacherSalary, @TeacherEmployeeNumber)";
            cmd.Parameters.AddWithValue("@TeacherFirstName", NewTeacher.TeacherFirstName);
            cmd.Parameters.AddWithValue("@TeacherLastName", NewTeacher.TeacherLastName);
            cmd.Parameters.AddWithValue("@TeacherHireDate", NewTeacher.TeacherHireDate);
            cmd.Parameters.AddWithValue("@TeacherSalary", NewTeacher.TeacherSalary);
            cmd.Parameters.AddWithValue("TeacherEmployeeNumber", NewTeacher.TeacherEmployeeNumber);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        /// <summary>
        /// Deletes an Author from the connected MySQL Database if the ID of that author exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        /// <example>POST /api/AuthorData/DeleteAuthor/3</example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void DeleteTeacher(int id)
        {
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();
        }
        /// <summary>
        /// Updates an Author on the MySQL Database. Non-Deterministic.
        /// </summary>
        /// <param name="AuthorInfo">An object with fields that map to the columns of the author's table.</param>
        /// <example>
        /// POST api/AuthorData/UpdateAuthor/208 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Christine",
        ///	"AuthorLname":"Bittle",
        ///	"AuthorBio":"Likes Coding!",
        ///	"AuthorEmail":"christine@test.ca"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Exit method if model fields are not included.
            if (!TeacherInfo.IsValid()) throw new ApplicationException("Posted Data was not valid.");
            MySqlConnection Conn = SchoolDb.AccessDatabase();

            try
            {
                //Open the connection between the web server and database
                Conn.Open();

                //Establish a new command (query) for our database
                MySqlCommand cmd = Conn.CreateCommand();

                //SQL QUERY
                cmd.CommandText = "UPDATE teachers SET teacherfname=@TeacherFirstName, teacherlname=@TeacherLastName, employeenumber=@TeacherEmployeeNumber, salary=@TeacherSalary, hiredate=@TeacherHireDate WHERE teacherid=@TeacherId";
                cmd.Parameters.AddWithValue("@TeacherFirstName", TeacherInfo.TeacherFirstName);
                cmd.Parameters.AddWithValue("@TeacherLastName", TeacherInfo.TeacherLastName);
                cmd.Parameters.AddWithValue("@TeacherEmployeeNumber", TeacherInfo.TeacherEmployeeNumber);
                cmd.Parameters.AddWithValue("@TeacherHireDate", TeacherInfo.TeacherHireDate);
                cmd.Parameters.AddWithValue("@TeacherId", id);
                cmd.Parameters.AddWithValue("@TeacherSalary", TeacherInfo.TeacherSalary);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                //Catches issues with MySQL.
                Debug.WriteLine(ex);
                throw new ApplicationException("Issue was a database issue.", ex);
            }
            catch (Exception ex)
            {
                //Catches generic issues
                Debug.Write(ex);
                throw new ApplicationException("There was a server issue.", ex);
            }
            finally
            {
                //Close the connection between the MySQL Database and the WebServer
                Conn.Close();

            }
        }
    }
}
