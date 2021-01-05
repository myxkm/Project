using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LY.EF.Model;
using LY.Bussiness.Interface;

namespace API.Areas.Admin.Controllers
{
    public class CaregoriesController : BaseController
    {
        public CaregoriesController(ICategoryService categoryService):base(categoryService)
        {

        }
      
        private LYContext db = new LYContext();

        // GET: Admin/Caregories
        public async Task<ActionResult> Index()
        {
            return View(await db.Caregory.ToListAsync());
        }

        // GET: Admin/Caregories/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caregory caregory = await db.Caregory.FindAsync(id);
            if (caregory == null)
            {
                return HttpNotFound();
            }
            return View(caregory);
        }

        // GET: Admin/Caregories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Caregories/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Address,CaregoryCode,CaregoryName,Phone,Email,Remark,CreateById,CreateDate")] Caregory caregory)
        {
            if (ModelState.IsValid)
            {
                db.Caregory.Add(caregory);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(caregory);
        }

        // GET: Admin/Caregories/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caregory caregory = await db.Caregory.FindAsync(id);
            if (caregory == null)
            {
                return HttpNotFound();
            }
            return View(caregory);
        }

        // POST: Admin/Caregories/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Address,CaregoryCode,CaregoryName,Phone,Email,Remark,CreateById,CreateDate")] Caregory caregory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caregory).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(caregory);
        }

        // GET: Admin/Caregories/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caregory caregory = await db.Caregory.FindAsync(id);
            if (caregory == null)
            {
                return HttpNotFound();
            }
            return View(caregory);
        }

        // POST: Admin/Caregories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Caregory caregory = await db.Caregory.FindAsync(id);
            db.Caregory.Remove(caregory);
            await db.SaveChangesAsync();
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
