﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication8.Models;

namespace WebApplication8.Controllers
{
    public class ObjectsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Objects
        public ActionResult XemDanhSach(string tim, string gia, string sx)
        {
            var products = db.Products.Select(p => p);
            ViewBag.sapxeptheoten = String.IsNullOrEmpty(sx) ? "SX" : "";
            ViewBag.sapxeptheogia = sx =="Gia" ? "Gia_desc" : "";
            switch(sx)
            {
                case "SX":
                    products = products.OrderBy(p => p.ProductName);
                    break;

            }
            if (!string.IsNullOrEmpty(tim))
            {
                products = products.Where(p => p.ProductName.Contains(tim));
            }
            if (!string.IsNullOrEmpty(gia))
            {
                decimal gia1 = 0;
                if(decimal.TryParse(gia, out gia1))
                {
                    products = products.Where(p => p.Price > gia1);
                }
            }
            return View(products.ToList());
        }

        // GET: Objects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Objects/Create
        public ActionResult ThemDuLieu()
        {
            ViewBag.CatalogyID = new SelectList(db.Catalogies, "CatalogyID", "CatalogyName");
            return View();
        }

        // POST: Objects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDuLieu([Bind(Include = "ProductID,ProductName,Description,PurchasePrice,Price,Quantity,Vintage,CatalogyID,Image,Region")] Product product)
        {
            if (ModelState.IsValid)
            {
                var f = Request.Files["FileName"];
                if(f != null && f.ContentLength > 0)
                {
                    string tenfile = System.IO.Path.GetFileName(f.FileName);
                    string duongdan = Server.MapPath("~/Images/" + tenfile);
                    f.SaveAs(duongdan);
                    product.Image = tenfile;    
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("XemDanhSach");
            }

            ViewBag.CatalogyID = new SelectList(db.Catalogies, "CatalogyID", "CatalogyName", product.CatalogyID);
            return View(product);
        }

        // GET: Objects/Edit/5
        public ActionResult SuaDuLieu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatalogyID = new SelectList(db.Catalogies, "CatalogyID", "CatalogyName", product.CatalogyID);
            return View(product);
        }

        // POST: Objects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaDuLieu([Bind(Include = "ProductID,ProductName,Description,PurchasePrice,Price,Quantity,Vintage,CatalogyID,Image,Region")] Product product)
        {
            if (ModelState.IsValid)
            {
                var b = db.Products.AsNoTracking().SingleOrDefault(p => p.ProductID == product.ProductID);
                var f = Request.Files["FileName"];
                if (f != null && f.ContentLength > 0)
                {
                    string tenfile = System.IO.Path.GetFileName(f.FileName);
                    string duongdan = Server.MapPath("~/Images/" + tenfile);
                    f.SaveAs(duongdan);
                    product.Image = tenfile;
                }
                else
                {
                    product.Image = b.Image;
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("XemDanhSach");
            }
            ViewBag.CatalogyID = new SelectList(db.Catalogies, "CatalogyID", "CatalogyName", product.CatalogyID);
            return View(product);
        }

        // GET: Objects/Delete/5
        public ActionResult XoaDuLieu(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Objects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult XoaDuLieuConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("XemDanhSach");
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
