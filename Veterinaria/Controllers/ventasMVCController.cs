using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Veterinaria.Models;

namespace Veterinaria.Controllers
{
    public class ventasMVCController : Controller
    {
        private BodegaEntities db = new BodegaEntities();

        // GET: ventasMVC
        public ActionResult Index()
        {
            //var venta = db.venta.Include(v => v.cliente).Include(v => v.producto);
            //return View(venta.ToList());
            IEnumerable<venta> alumno = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:60434/api/");
                //GET ALUMNOS
                //obtiene asincronamente y espera hasta obetener la data
                var responseTask = client.GetAsync("ventasapi");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var leer = result.Content.ReadAsAsync<IList<venta>>();
                    leer.Wait();
                    alumno = leer.Result;
                }
                else
                {
                    alumno = Enumerable.Empty<venta>();
                    ModelState.AddModelError(string.Empty, "Error .... Try Again");
                }
            }
            return View(alumno.ToList());
        }

        // GET: ventasMVC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venta venta = db.venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // GET: ventasMVC/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "nombre");
            ViewBag.id_producto = new SelectList(db.producto, "id_producto", "nombre");
            return View();
        }

        // POST: ventasMVC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_venta,id_producto,id_cliente,precio")] venta venta)
        {
            if (ModelState.IsValid)
            {
                db.venta.Add(venta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "nombre", venta.id_cliente);
            ViewBag.id_producto = new SelectList(db.producto, "id_producto", "nombre", venta.id_producto);
            return View(venta);
        }

        // GET: ventasMVC/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venta venta = db.venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "nombre", venta.id_cliente);
            ViewBag.id_producto = new SelectList(db.producto, "id_producto", "nombre", venta.id_producto);
            return View(venta);
        }

        // POST: ventasMVC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_venta,id_producto,id_cliente,precio")] venta venta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.cliente, "id_cliente", "nombre", venta.id_cliente);
            ViewBag.id_producto = new SelectList(db.producto, "id_producto", "nombre", venta.id_producto);
            return View(venta);
        }

        // GET: ventasMVC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venta venta = db.venta.Find(id);
            if (venta == null)
            {
                return HttpNotFound();
            }
            return View(venta);
        }

        // POST: ventasMVC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            venta venta = db.venta.Find(id);
            db.venta.Remove(venta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
