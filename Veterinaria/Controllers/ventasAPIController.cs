using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class ventasAPIController : ApiController
    {
        private BodegaEntities db = new BodegaEntities();

        // GET: api/ventasAPI
        public IQueryable<venta> Getventa()
        {
            return db.venta.Include(v => v.cliente).Include(v => v.producto); 
        }

        // GET: api/ventasAPI/5
        [ResponseType(typeof(venta))]
        public IHttpActionResult Getventa(int id)
        {
            venta venta = db.venta.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            return Ok(venta);
        }

        // PUT: api/ventasAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putventa(int id, venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != venta.id_venta)
            {
                return BadRequest();
            }

            db.Entry(venta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ventaExists(id))
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

        // POST: api/ventasAPI
        [ResponseType(typeof(venta))]
        public IHttpActionResult Postventa(venta venta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.venta.Add(venta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = venta.id_venta }, venta);
        }

        // DELETE: api/ventasAPI/5
        [ResponseType(typeof(venta))]
        public IHttpActionResult Deleteventa(int id)
        {
            venta venta = db.venta.Find(id);
            if (venta == null)
            {
                return NotFound();
            }

            db.venta.Remove(venta);
            db.SaveChanges();

            return Ok(venta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ventaExists(int id)
        {
            return db.venta.Count(e => e.id_venta == id) > 0;
        }
    }
}