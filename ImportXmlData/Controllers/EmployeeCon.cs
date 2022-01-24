using ImportXmlData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using System.Web;
using System.Data;

namespace ImportXmlData.Controllers
{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
  
    public class EmployeeCon : Controller
    {
        private readonly IEmployee _employee;
        private readonly AppDataContext _db;
        public EmployeeCon(IEmployee employee, AppDataContext db)
        {
            _employee = employee;
            _db = db;
        }


        public IActionResult Details(int id)
        {

            return View(_employee.GetById(id));
        }
      
        [HttpGet]
        public IActionResult listOfEmployee()
        {
          

            return View(_employee.All());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeModel emp)
        {
            if (ModelState.IsValid)
            {
                _employee.Create(emp);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var model = _employee.GetById(id);
            EmployeeModel updateId = new EmployeeModel();
            updateId.Id = model.Id;
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(EmployeeModel emp)
        {
            if (ModelState.IsValid)
            {
                EmployeeModel ob = _employee.GetById(emp.Id);
                if (ob != null)
                {
                    ob.EmployeeId = emp.EmployeeId;
                    ob.Firstname = emp.Firstname;
                    ob.Lastname = emp.Lastname;
                    ob.Division = emp.Division;
                    ob.Room = emp.Room;
                    ob.Title = emp.Title;
                    ob.Building = emp.Building;

                    _employee.Update(ob);
                }

                return RedirectToAction("listOfEmployee");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _employee.GetById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfimDelete(int id)
        {
            _employee.Delete(id);

            return RedirectToAction("listOfEmployee");
        }
        public IActionResult importXml()
        {

            return View();
        }

        [HttpPost]
    
        public IActionResult importXml(IFormFile xmlFile)
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(),
                "xmls", xmlFile.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                xmlFile.CopyTo(stream);
            }
            var data = ImportProcess("xmls/" + xmlFile.FileName);

            ViewBag.result = "File Upload complete";
            return RedirectToAction("listOfEmployee");
        }

        public List<EmployeeModel> ImportProcess(string path)
        {


            string xmlData = path;
            DataSet ds = new DataSet();
            ds.ReadXml(xmlData);
            var employees = new List<EmployeeModel>();
            employees = (from rows in ds.Tables[0].AsEnumerable()
                         select new EmployeeModel
                         {
                              //Convert row to int  
                             Firstname = rows[0].ToString(),
                             Lastname = rows[1].ToString(),
                             Title = rows[2].ToString(),
                             Division = rows[3].ToString(),
                             Building = rows[4].ToString(),
                             Room = rows[5].ToString(),
                             EmployeeId = rows[6].ToString(),
                         }).ToList();
            foreach (var item in employees)
            {


                _employee.Create(item);
            }
           
            return employees;

        }


    }
}
