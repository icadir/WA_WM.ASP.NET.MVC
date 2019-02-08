using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using Admin.BLL.Repository;
using System.Linq;
using System.Web.Helpers;
using System.Web.Mvc;
using Admin.BLL.Helpers;
using Admin.Models.Entities;
using Admin.Models.ViewModels;

namespace Admin.Web.UI.Controllers
{

    public class CategoryController : BaseController
    {
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            ViewBag.CategoryList = GetCategorySelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(CategoryViewModel model)
        {
            try
            {
                if (model.SupCategoryId == 0) model.SupCategoryId = null;
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("CategoryName", "100 karakteri geçme kardeş");
                    model.SupCategoryId = model.SupCategoryId ?? 0;
                    ViewBag.CategoryList = GetCategorySelectList();
                    return View(model);
                }

                if (model.SupCategoryId > 0)
                {
                    model.TaxRate = new CategoryRepo().GetById(model.SupCategoryId).TaxRate;
                }
                if (model.PostedFile != null &&
                    model.PostedFile.ContentLength > 0)
                {
                    var file = model.PostedFile;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extName = Path.GetExtension(file.FileName);
                    fileName = StringHelpers.UrlFormatConverter(fileName);
                    fileName += StringHelpers.GetCode();
                    var klasoryolu = Server.MapPath("~/Product/");
                    var dosyayolu = Server.MapPath("~/Product/") + fileName + extName;

                    if (!Directory.Exists(klasoryolu))
                        Directory.CreateDirectory(klasoryolu);
                    file.SaveAs(dosyayolu);

                    WebImage img = new WebImage(dosyayolu);
                    img.Resize(250, 250, false);
                    img.AddTextWatermark("Wissen");
                    img.Save(dosyayolu);
                    model.CategoryPath = "/Product/" + fileName + extName;
                }

                var model2 = new Category
                {
                    CategoryName = model.CategoryName,
                    CategoryPath = model.CategoryPath,
                    SupCategoryId = model.SupCategoryId,
                    TaxRate = model.TaxRate,
                };
                new CategoryRepo().Insert(model2);
                TempData["Message"] = $"{model.CategoryName} isimli kategori başarıyla eklenmiştir";
                return RedirectToAction("Add");
            }
            catch (DbEntityValidationException ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu: {EntityHelpers.ValidationMessage(ex)}",
                    ActionName = "Add",
                    ControllerName = "Category",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu: {ex.Message}",
                    ActionName = "Add",
                    ControllerName = "Category",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public ActionResult Update(int id = 0)
        {
            ViewBag.CategoryList = GetCategorySelectList();
            var data = new CategoryRepo().GetById(id);
            if (data == null)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Kategori Bulunamadı",
                    ActionName = "Add",
                    ControllerName = "Category",
                    ErrorCode = 404
                };
                return RedirectToAction("Error", "Home");
            }
            var data2 = new CategoryViewModel
            {
                CategoryPath = data.CategoryPath,
                CategoryName = data.CategoryName,
                SupCategoryId = data.SupCategoryId,
                Id = data.Id,
            };
           

            return View(data2);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(CategoryViewModel model)
        {
            try
            {
                if (model.SupCategoryId == 0) model.SupCategoryId = null;
                if (!ModelState.IsValid)
                {
                    model.SupCategoryId = model.SupCategoryId ?? 0;
                    ViewBag.CategoryList = GetCategorySelectList();
                    return View(model);
                }

                if (model.SupCategoryId > 0)
                {
                    model.TaxRate = new CategoryRepo().GetById(model.SupCategoryId).TaxRate;
                }

                var data = new CategoryRepo().GetById(model.Id);
                data.CategoryName = model.CategoryName;
                data.TaxRate = model.TaxRate;
                data.SupCategoryId = model.SupCategoryId;
                if (model.PostedFile != null &&
                    model.PostedFile.ContentLength > 0)
                {
                    var file = model.PostedFile;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extName = Path.GetExtension(file.FileName);
                    fileName = StringHelpers.UrlFormatConverter(fileName);
                    fileName += StringHelpers.GetCode();
                    var klasoryolu = Server.MapPath("~/Category/");
                    var dosyayolu = Server.MapPath("~/Category/") + fileName + extName;

                    if (!Directory.Exists(klasoryolu))
                        Directory.CreateDirectory(klasoryolu);
                    file.SaveAs(dosyayolu);

                    WebImage img = new WebImage(dosyayolu);
                    img.Resize(250, 250, false);
                    img.AddTextWatermark("Wissen");
                    img.Save(dosyayolu);
                    model.CategoryPath = "/Category/" + fileName + extName;
                }

                data.CategoryPath = model.CategoryPath;

                new CategoryRepo().Update(data);
                foreach (var dataCategory in data.Categories)
                {
                    dataCategory.TaxRate = data.TaxRate;
                    new CategoryRepo().Update(dataCategory);
                    if (dataCategory.Categories.Any())
                        UpdateSubTaxRate(dataCategory.Categories);
                }

                void UpdateSubTaxRate(ICollection<Category> dataC)
                {
                    foreach (var dataCategory in dataC)
                    {
                        dataCategory.TaxRate = data.TaxRate;
                        new CategoryRepo().Update(dataCategory);
                        if (dataCategory.Categories.Any())
                            UpdateSubTaxRate(dataCategory.Categories);
                    }
                }
                TempData["Message"] = $"{model.CategoryName} isimli kategori başarıyla güncellenmiştir";
                ViewBag.CategoryList = GetCategorySelectList();
                return View(model);
            }
            catch (DbEntityValidationException ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu: {EntityHelpers.ValidationMessage(ex)}",
                    ActionName = "Add",
                    ControllerName = "Category",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu: {ex.Message}",
                    ActionName = "Add",
                    ControllerName = "Category",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }

    }
}