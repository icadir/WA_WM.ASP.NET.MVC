using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using Admin.BLL.Helpers;
using Admin.BLL.Repository;
using Admin.BLL.Services;
using Admin.Models.Entities;
using Admin.Models.Models;
using Admin.Models.ViewModels;
using AutoMapper;

namespace Admin.Web.UI.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            ViewBag.ProductList = GetProductSelectList();
            ViewBag.CategoryList = GetCategorySelectList();
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Add(ProductViewModel model)
        {
            //Mapper kullanımı
            //asagıda veri tabanına bi sorgu atılıyor veir tabanından  product list dönüyor ve biz bu dönen X nesnesini prodcutview modelle cast ettiriyoruz. Eşiliyoruz. Bu çagarırken.
         //-->var data = new ProductRepo().GetAll().Select(x=>Mapper.Map<ProductViewModel>(x)).ToList();

            //Bu Örnekte viewmodel olarak gelen nesneyi istedigimiz entitye cast etme.Bu sayfaya gelen modeli örnek verebilriz.
            //new ProductRepo().Insert(Mapper.Map<ProductViewModel, Product>(model));

            if (!ModelState.IsValid)
            {
                ViewBag.ProductList = GetProductSelectList();
                ViewBag.CategoryList = GetCategorySelectList();
                return View(model);
            }

            try
            {
                if (model.SupProductId.ToString().Replace("0", "").Replace("-", "").Length == 0)
                    model.SupProductId = null;

                model.LastPriceUpdateDate = DateTime.Now;
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
                    model.ProductPath = "/Product/" + fileName + extName;
                }

                var model2 = new Product
                {
                    ProductPath = model.ProductPath,
                    Barcode = model.Barcode,
                    BuyPrice = model.BuyPrice,
                    CategoryId = model.CategoryId,
                    CreatedDate = DateTime.Now,
                    Description = model.Description,
                    ProductType = model.ProductType,
                    SalesPrice = model.SalesPrice,
                    UnitsInStock = model.UnitsInStock,
                    Quantity = model.Quantity,
                    ProductName = model.ProductName,
                    LastPriceUpdateDate = DateTime.Now,
                    SupProductId = model.SupProductId,
                   

                };
                await new ProductRepo().InsertAsync(model2);
                TempData["Message"] = $"{model.ProductName} isimli ürün başarıyla eklenmiştir";
                return RedirectToAction("Add");
            }
            catch (DbEntityValidationException ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"Bir hata oluştu: {EntityHelpers.ValidationMessage(ex)}",
                    ActionName = "Add",
                    ControllerName = "Product",
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
                    ControllerName = "Product",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public JsonResult CheckBarcode(string barcode)
        {
            try
            {
                if (new ProductRepo().Queryable().Any(x => x.Barcode == barcode))
                {
                    return Json(new ResponseData()
                    {
                        message = $"{barcode} sistemde kayıtlı",
                        success = true
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new ResponseData()
                {
                    message = $"{barcode} bilgisi servisten getirildi",
                    success = true,
                    data = new BarcodeService().Get(barcode)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseData()
                {
                    message = $"Bir hata oluştu: {ex.Message}",
                    success = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}