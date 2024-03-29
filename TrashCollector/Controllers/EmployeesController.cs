﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrashCollector.Models;

namespace TrashCollector.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {                     
            string currentUserId = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(e => e.ApplicationId == currentUserId).Single();
            DateTime todaysDate = new DateTime();
            todaysDate = DateTime.Today;
            int today = (int)System.DateTime.Now.DayOfWeek;           
            var customerPickups = db.Customers.Where(p => p.Zipcode == employee.Zipcode && ((int)p.Pickup.RegularPickupDay == today || p.Pickup.ExtraPickupDay == todaysDate)).Include(p => p.Pickup).ToList();

            for (int i = 0; i < customerPickups.Count; i++)
            {
                if (customerPickups[i].Pickup.TemporarySuspensionStart != null && customerPickups[i].Pickup.TemporarySuspensionEnd != null)
                {
                    if (todaysDate.Ticks > ((DateTime)customerPickups[i].Pickup.TemporarySuspensionStart).Ticks && todaysDate.Ticks < ((DateTime)customerPickups[i].Pickup.TemporarySuspensionEnd).Ticks)
                    {
                        customerPickups.RemoveAt(i);
                    }
                }
            }
            return View(customerPickups);
        }

        //Get: Pickups by day
        public ActionResult PickupsByDay(int dayOfWeek)
        {
            string currentUserId = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(e => e.ApplicationId == currentUserId).Single();
            DateTime todaysDate = new DateTime();

            var customerPickups = db.Customers.Where(p => p.Zipcode == employee.Zipcode && ((int)p.Pickup.RegularPickupDay == dayOfWeek)).Include(p => p.Pickup).ToList();

            for (int i = 0; i < customerPickups.Count; i++)
            {
                if (customerPickups[i].Pickup.TemporarySuspensionStart != null && customerPickups[i].Pickup.TemporarySuspensionEnd != null)
                {
                    if (todaysDate.Ticks > ((DateTime)customerPickups[i].Pickup.TemporarySuspensionStart).Ticks && todaysDate.Ticks < ((DateTime)customerPickups[i].Pickup.TemporarySuspensionEnd).Ticks)
                    {
                        customerPickups.RemoveAt(i);
                    }
                }
            }
            return View(customerPickups);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //GET: Pickup details!!
        public ActionResult ShowCustomerAddress(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,zipcode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ApplicationId = User.Identity.GetUserId();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index", "Employees", new { zipcode = employee.Zipcode });
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Zipcode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        //GET: Confirm pickup!!
        public ActionResult ConfirmPickup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Customer customer = db.Customers.Find(id);
            Customer customer = db.Customers.Where(c => c.Id == id).Include(c => c.Pickup).FirstOrDefault();

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer.Pickup);
        }

        //POST: Confirm pickup!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmPickup([Bind(Include = "Id,RegularPickupDay,PickupConfirmed,ExtraPickupDay,ExtraPickupConfirmed,TemporarySuspensionStart,TemporarySuspensionEnd")] Pickup pickup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pickup).State = EntityState.Modified;
                if (pickup.PickupConfirmed == true)
                {
                    pickup.Bill += 20;
                }
                if (pickup.ExtraPickupConfirmed == true)
                {
                    pickup.Bill += 20;
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(pickup);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
