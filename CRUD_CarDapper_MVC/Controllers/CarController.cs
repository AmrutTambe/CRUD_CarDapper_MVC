using CRUD_CarDapper_MVC.Models;
using Crud_MVC_CarDapper.Models;
using NPOI.HSSF.UserModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;


namespace CRUD_CarDapper_MVC.Controllers
{
    public class CarController : Controller
    {
        //IPagedList<Car> stu = null;
        //List<Car> objCarList = new List<Car>();
        ////CarsRepository sr = new CarsRepository();
        Car s = new Car();
        //int pageIndex = 1;
        //int pageSize = 5;
        //int a;
        // GET: Car
        private CarsRepository repository;

        //public dynamic SelectedText { get; private set; }

        public CarController()
        {
            repository = new CarsRepository();
        }

        // GET: Car
        public ActionResult Index(RequestModel request, int? i)
        {

            //ViewBag.pg = pageSize;
            //ViewBag.ValueToSet = SelectedText;
            //ViewBag.Value = SelectedText;
            //pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            
                if (request.OrderBy == null)
                {
                    request = new RequestModel
                    {
                        
                       
                        Search = request.Search,
                        OrderBy = "name",
                        IsDescending = false
                    };
                }
                  ViewBag.Request = request;
            var list = new List<int>() { 5, 10, 15, 20 };
            ViewBag.list = list;
            int Page = 1;
            int PageSize = 3;
                return View(repository.GetAll(request).ToList().ToPagedList(i ?? Page, PageSize));

            }
        
        // GET: Car/Details/5
        public ActionResult Details(int id)
        {
            return View(repository.Get(id));
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            var list = new List<string>() {"BMW", "Lamborgini", "Mercedese", "Ferarri" };
            var list1 = new List<string>() { "20000000", "30000000", "400000000", "50000000" };
            var list2 = new List<string>() { "X1", "X4", "X5", "X7" };
            ViewBag.list = list;
            ViewBag.list1 = list1;
            ViewBag.list2 = list2;
           
            return View();
        }

        // POST: Car/Create
        [HttpPost]
        public ActionResult Create(Car car, bool editAfterSaving = false)
        {
            
            if (ModelState.IsValid)
            {
                
                //repository.SaveStudents(s);
                var lastInsertedId = repository.Create(car);
                if (lastInsertedId > 0)
                {
                    TempData["Message"] = "Record added successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to add record";
                }
                if (editAfterSaving)
                {
                    return RedirectToAction("Edit", new { Id = lastInsertedId });
                }
                else
                {
                    //int TotalPages = (int)Math.Ceiling(a / (double)pageSize);
                    return RedirectToAction("Index");
                    //return RedirectToAction("Index");
                }
            }
            var list = new List<string>() { "BMW", "Lamborgini", "Mercedese", "Ferarri" };
            var list1 = new List<string>() { "20000000", "30000000", "400000000", "50000000" };
            var list2 = new List<string>() { "X1", "X4", "X5", "X7" };
            ViewBag.list = list;
            ViewBag.list1 = list1;
            ViewBag.list2 = list2;
            return View();
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int id)
        {
            var list = new List<string>() { "BMW", "Lamborgini", "Mercedese", "Ferarri" };
            var list1 = new List<string>() { "20000000", "30000000", "400000000", "50000000" };
            var list2 = new List<string>() { "X1", "X4", "X5", "X7" };
            ViewBag.list = list;
            ViewBag.list1 = list1;
            ViewBag.list2 = list2;
            return View(repository.Get(id));
        }

        // POST: Car/Edit/5
        [HttpPost]
        public ActionResult Edit(Car car, bool editAfterSaving = false)
        {
            if (ModelState.IsValid)
            {
                var recordAffected = repository.Update(car);
                if (recordAffected > 0)
                {
                    TempData["Message"] = "Record updated successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to update record";
                }
                if (editAfterSaving)
                {
                    return RedirectToAction("Edit", new { Id = recordAffected });
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            var list = new List<string>() { "BMW", "Lamborgini", "Mercedese", "Ferarri" };
            var list1 = new List<string>() { "20000000", "30000000", "400000000", "50000000" };
            var list2 = new List<string>() { "X1", "X4", "X5", "X7" };
            ViewBag.list = list;
            ViewBag.list1 = list1;
            ViewBag.list2 = list2;
            return View();
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int id)
        {
            return View(repository.Get(id));
        }

        // POST: Car/Delete/5
        [HttpPost]
        public ActionResult Delete(Car car)
        {
            var recordAffected = repository.Delete(car.Id);
            if (recordAffected > 0)
            {
                TempData["Message"] = "Record deleted successfully";
            }
            else
            {
                TempData["Error"] = "Failed to delete record";
            }
            return RedirectToAction("Index");
        }
      
        public ActionResult ExportToExcel(RequestModel request)
        {

            CarsRepository repository = new CarsRepository();
            var model = repository.GetAll(request);

            HSSFWorkbook templateWorkbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)templateWorkbook.CreateSheet("Index");
            List<Car> _car = model.ToList();
            HSSFRow dataRow = (HSSFRow)sheet.CreateRow(0);
            HSSFCellStyle style = (HSSFCellStyle)templateWorkbook.CreateCellStyle();

            HSSFFont font = (HSSFFont)templateWorkbook.CreateFont();
            font.Color = NPOI.HSSF.Util.HSSFColor.White.Index;
            HSSFCell cell;
            style.SetFont(font);

            //cell = (HSSFCell)dataRow.CreateCell(0);
            //cell.CellStyle = style;
            //cell.SetCellValue("Id.");

            cell = (HSSFCell)dataRow.CreateCell(0);
            cell.CellStyle = style;
            cell.SetCellValue("CarName");
            cell = (HSSFCell)dataRow.CreateCell(1);
            cell.CellStyle = style;
            cell.SetCellValue("CarModel");
            cell = (HSSFCell)dataRow.CreateCell(2);
            cell.CellStyle = style;
            cell.SetCellValue("Company");
            cell = (HSSFCell)dataRow.CreateCell(3);
            cell.CellStyle = style;
            cell.SetCellValue("Price");

            for (int i = 0; i < _car.Count; i++)
            {
                dataRow = (HSSFRow)sheet.CreateRow(i + 1);
                dataRow.CreateCell(0).SetCellValue(i + 1);
                dataRow.CreateCell(1).SetCellValue(_car[i].CarName);
                dataRow.CreateCell(2).SetCellValue(_car[i].CarModel);
                dataRow.CreateCell(3).SetCellValue(_car[i].Company);
                dataRow.CreateCell(3).SetCellValue(_car[i].Price);
            }
            MemoryStream ms = new MemoryStream();
            templateWorkbook.Write(ms);
            return File(ms.ToArray(), "application/vnd.ms-excel", "Cars.xls");


            
       
    }

    }
}
