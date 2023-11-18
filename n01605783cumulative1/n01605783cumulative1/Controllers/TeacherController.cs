using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using n01605783cumulative1.Models;

namespace n01605783cumulative1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/List
        // Go to /Views/Teacher/List.cshtml
        // Browser opens a list teachers page
        public ActionResult List()
        {
            // we need to pass teacher information to the view

            // create an instance of the teacher data controller

            TeacherDataController Controller = new TeacherDataController();



            IEnumerable<Teacher> enumerable = Controller.ListTeacherData();
            List<Teacher> Teachers = (List<Teacher>)enumerable;

            // pass the teachers information to the /Views/Teacher/List.cshtml
            return View(Teachers);
        }
        // GET : /Teacher/Show/{Id}
        //Route to /Views/Teacher/Show.cshtml
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();

            Teacher SelectedTeacher =  Controller.FindTeacher(id);
            // we want to show a particular teacher given the id
            return View(SelectedTeacher);
        }
    }
}