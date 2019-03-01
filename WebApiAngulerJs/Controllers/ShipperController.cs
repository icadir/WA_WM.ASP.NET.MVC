using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiAngulerJs.Models;

namespace WebApiAngulerJs.Controllers
{
    public class ShipperController : ApiController
    {
        NorthwindEntities db = new NorthwindEntities();

        public IHttpActionResult GetAll()
        {
            try
            {
               
                return Ok(new
                {
                    success = true,
                    data = db.Shippers.Select(x => new ShipperViewModel()
                    {
                        CompanyName = x.CompanyName,
                        ShipperID = x.ShipperID,
                        Phone = x.Phone
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştu {ex.Message}");
            }
        }

        [HttpGet]
        public IHttpActionResult Get(int id = 0)
        {
            try
            {
                var ship = db.Shippers.Find(id);
                if (ship == null)
                {
                    return NotFound();
                }

                var data = new ShipperViewModel()
                {
                     CompanyName = ship.CompanyName,
                     Phone = ship.Phone,
                     ShipperID = ship.ShipperID
                };
                return Ok(new
                {
                    success = true,
                    data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştu {ex.Message}");
            }
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody]ShipperViewModel model)
        {
            try
            {
                db.Shippers.Add(new Shipper()
                {
                    CompanyName = model.CompanyName,
                    Phone = model.Phone,
                });
                db.SaveChanges();
                return Ok(new
                {
                    success = true,
                    message = "Kargo Şirketi işlemi başarılı"
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştu {ex.Message}");
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id = 0)
        {
            try
            {
                db.Shippers.Remove(db.Shippers.Find(id));
                db.SaveChanges();
                return Ok(new
                {
                    success = true,
                    message = "Kargo silme işlemi başarılı"
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştu {ex.Message}");
            }
        }

        [HttpPut]
        public IHttpActionResult PutShipper(int id, Shipper model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.ShipperID)
            {
                return BadRequest();
            }

            db.Entry(model).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok(new
                {
                    success = true,
                    message = "Kategori Güncelleme işlemi başarılı"
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!db.Shippers.Any(x => x.ShipperID == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
    public class ShipperViewModel
    {
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
    }
}
