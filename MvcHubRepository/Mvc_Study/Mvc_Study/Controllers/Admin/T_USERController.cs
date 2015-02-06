using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Library;
using IdentitySample.Models;
using System.Web.Security;

namespace Mvc_Study.Controllers.Admin
{
    [Authorize]
    public class T_USERController : Controller
    {
        private Entities db = new Entities();

        // GET: T_USER
        public ActionResult Index()
        {
            var t_USER = db.T_USER.Include(t => t.AspNetUser).Include(t => t.T_ARTIST);
            return View(t_USER.ToList());
        }

        // GET: T_USER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_USER t_USER = db.T_USER.Find(id);
            if (t_USER == null)
            {
                return HttpNotFound();
            }
            return View(t_USER);
        }

        // GET: T_USER/Create
        public ActionResult Create()
        {
            ViewBag.US_IDASPNETUSERS = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.Id = new SelectList(db.T_ARTIST, "ART_IDUSER", "AKA");
            return View();
        }


        //private ApplicationRoleManager _roleManager;
        //public ApplicationRoleManager RoleManager
        //{
        //    get
        //    {
        //        return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
        //    }
        //    private set
        //    {
        //        _roleManager = value;
        //    }
        //}

        // POST: T_USER/Create
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NAME,SURNAME,URIMG,US_IDASPNETUSERS")] T_USER t_USER)
        {
            if (ModelState.IsValid)
            {
                //AddUserToRole aggiunge il ruolo VISITOR come default per ogni utente creato
                Roles.AddUserToRole(t_USER.NAME, "Visitor");

                db.T_USER.Add(t_USER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.US_IDASPNETUSERS = new SelectList(db.AspNetUsers, "Id", "Email", t_USER.US_IDASPNETUSERS);
            ViewBag.Id = new SelectList(db.T_ARTIST, "ART_IDUSER", "AKA", t_USER.Id);
            return View(t_USER);
        }

        // GET: T_USER/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_USER t_USER = db.T_USER.Find(id);
            if (t_USER == null)
            {
                return HttpNotFound();
            }
            ViewBag.US_IDASPNETUSERS = new SelectList(db.AspNetUsers, "Id", "Email", t_USER.US_IDASPNETUSERS);
            ViewBag.Id = new SelectList(db.T_ARTIST, "ART_IDUSER", "AKA", t_USER.Id);
            return View(t_USER);
        }

        // POST: T_USER/Edit/5
        // Per proteggere da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per ulteriori dettagli, vedere http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NAME,SURNAME,URIMG,US_IDASPNETUSERS")] T_USER t_USER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_USER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.US_IDASPNETUSERS = new SelectList(db.AspNetUsers, "Id", "Email", t_USER.US_IDASPNETUSERS);
            ViewBag.Id = new SelectList(db.T_ARTIST, "ART_IDUSER", "AKA", t_USER.Id);
            return View(t_USER);
        }

        // GET: T_USER/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_USER t_USER = db.T_USER.Find(id);
            if (t_USER == null)
            {
                return HttpNotFound();
            }
            return View(t_USER);
        }

        // POST: T_USER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            T_USER t_USER = db.T_USER.Find(id);
            db.T_USER.Remove(t_USER);
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
