using FileuploadMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileuploadMVC.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        [HttpGet]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(Employee employee)
        {
            using (controlEntities entity = new controlEntities())
            {
                var candidate = new Candidate()
                {
                    ContactNo = employee.ContactNo,
                    EmailID = employee.EmailID,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Position = employee.Position,
                    Resume = SaveToPhysicalLocation(employee.Resume),
                    Skills = employee.Skills,
                    CreatedOn = DateTime.Now
                };
                entity.Candidates.Add(candidate);
                entity.SaveChanges();
                
            }
            return View(employee);
        }

        

        /// <summary>
        ///  Save Posted File in Physical path and return saved path to store in a database
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string SaveToPhysicalLocation(HttpPostedFileBase file)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data"), fileName);
                file.SaveAs(path);
                return path;
            }
            return string.Empty;
        }
    }
}

