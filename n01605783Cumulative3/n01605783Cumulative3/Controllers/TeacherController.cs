using n01605783Cumulative3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics; 

namespace n01605783Cumulative3.Controllers
{
    public class TeacherController : Controller
    {
        private TeacherDataController controller = new TeacherDataController();
        // GET: Teacher
        // GET: Teacher/List?TeacherSearchKey={value}
        // Go to /Views/Teacher/List.cshtml
        // Browser opens a list teachers page
        public ActionResult List(string TeacherSearchKey)
        {

            //even without searching, we can grab that search key
            Debug.WriteLine("I want to search for teachers with the key " + TeacherSearchKey);

            // we need to pass teacher information to the view

            // create an instance of the teacher data controller

            //IEnumerable<Teacher> enumerable = controller.ListTeacherData("");
            
            List<Teacher> Teachers = (List<Teacher>)controller.ListTeacherData(TeacherSearchKey);

            // pass the teachers information to the /Views/Teacher/List.cshtml
            return View(Teachers);
        }
        // GET : /Teacher/Show/{Id}
        //Route to /Views/Teacher/Show.cshtml
        public ActionResult Show(int id)
        {

            Teacher SelectedTeacher = controller.FindTeacher(id);
            // we want to show a particular teacher given the id
            return View(SelectedTeacher);
        }

        public ActionResult New()
        {
            return View();
        }
        public ActionResult Create(string TeacherFname, string TeacherLname, DateTime TeacherHireDate, string TeacherSalary, string TeacherEmployeeNumber)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherHireDate);
            Debug.WriteLine(TeacherSalary);
            Debug.WriteLine(TeacherEmployeeNumber);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFirstName = TeacherFname;
            NewTeacher.TeacherLastName = TeacherLname;
            NewTeacher.TeacherHireDate = TeacherHireDate;
            NewTeacher.TeacherSalary = TeacherSalary;
            NewTeacher.TeacherEmployeeNumber = TeacherEmployeeNumber;

            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
        //GET : /Author/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }
        /// <summary>
        /// Routes to a dynamically generated "Teacher Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the author and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Author/Update/5</example>
        public ActionResult Update(int id)
        {
            try
            {
                Teacher SelectedTeacher = controller.FindTeacher(id);
                return View(SelectedTeacher);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Teacher");
            }

        }


        /// <summary>
        /// Receives a POST request containing information about an existing author in the system, with new values. Conveys this information to the API, and redirects to the "Author Show" page of our updated author.
        /// </summary>
        /// <param name="id">Id of the Author to update</param>
        /// <param name="TeacherFirstName">The updated first name of the author</param>
        /// <param name="TeacherLastName">The updated last name of the author</param>
        /// <param name="TeacherEmployeeNumber">The updated bio of the author.</param>
        /// <param name="TeacherId">The updated email of the author.</param>
        /// <returns>A dynamic webpage which provides the current information of the author.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"TeacherFirstName":"Abhishek",
        ///	"TeacherLastName":"Gaur",
        ///	"TeacherEmployeeNumber":"T785",
        ///	"TeacherId":"52"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherEmployeeNumber, string TeacherFirstName, string TeacherLastName, string TeacherHireDate, string TeacherSalary)
        {
            try
            {
                Teacher TeacherInfo = new Teacher();
                if (ModelState.IsValid)
                {
                    TeacherInfo.TeacherFirstName = TeacherFirstName;
                    TeacherInfo.TeacherLastName = TeacherLastName;
                    TeacherInfo.TeacherEmployeeNumber = TeacherEmployeeNumber;
                    TeacherInfo.TeacherSalary = TeacherSalary;
                    TeacherInfo.TeacherHireDate = Convert.ToDateTime(TeacherHireDate);
                    controller.UpdateTeacher(id, TeacherInfo);

                    return RedirectToAction("Show/" + id);
                }
                else
                {
                    return View(TeacherInfo);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Error", "Teacher");
            }
            
        }
        //GET : /Teacher/Error
        /// <summary>
        /// This window is for showing Teacher Specific Errors!
        /// </summary>
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult New_js()
        {
            return View();
        }
    }
}
