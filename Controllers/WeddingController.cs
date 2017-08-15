using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using wedding.Models;
using System.Linq;
using MySQL.Data.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

namespace wedding.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext _context;
       
       
        public WeddingController(WeddingContext context){
            _context= context;
            
        }

        [HttpGet]
        [Route("weddings/show")]
        public IActionResult Show()
        {
            ViewBag.Errors=new List<string>();
            int? Id=HttpContext.Session.GetInt32("UserId"); 
            if(Id == null){
                return RedirectToAction("Index","User");
            }
            List<Weddings> weddings= _context.Weddings
                                    .Include(a=>a.Invitations)
                                    .ThenInclude(b=>b.Guests).ToList();
            ViewBag.weddings=weddings;
            return View("Dashboard");
        }

        // To New Wedding Form
        [HttpGet]
        [Route("weddings/new")]
        public IActionResult ToNew()
        {
            ViewBag.Errors=new List<string>();
            return View("New");
        }

        // Create new Wedding plan 
        [HttpPost]
        [Route("weddings/create")]
        public IActionResult Create(WeddingsView WedView)
        {

            if(ModelState.IsValid){
                System.Console.WriteLine($"Line 50 Weddings-Valid");
                Weddings newWed= new Weddings {
                    WedderOne= WedView.WedderOne,
                    WedderTwo= WedView.WedderTwo,
                    Date= WedView.Date,
                    Address= WedView.Address,
                    CreatedAt= DateTime.Now,
                    UpdatedAt= DateTime.Now,
                };
                _context.Weddings.Add(newWed);
                _context.SaveChanges();
                // Get new Wedding back;
                Weddings Wed= new Weddings();
                Wed = _context.Weddings
                        .Include(w=>w.Invitations)
                        .ThenInclude(b=>b.Guests)
                .SingleOrDefault(c=> c.WedderOne==WedView.WedderOne && c.WedderTwo==WedView.WedderTwo);
                System.Console.WriteLine($"Wedding Wedding {Wed}");
                ViewBag.Wedding= Wed;
                return View("Dashboard");
            }
            else{
                ViewBag.Errors= ModelState.Values;
                return RedirectToAction("ToNew");
            }
            
        }



        [HttpGet]
        [Route("weddings/{id}/{choice}")]
        public IActionResult UpdateInvite(int id, string choice)
        {
            int? Id=HttpContext.Session.GetInt32("UserId"); 
            System.Console.WriteLine($"Wedding to update choice %%^^^^^^^^^^^^^%^%%%%%&^*^*{id}{choice}");
            Invitations newInvite= new Invitations();
            if(choice=="RSVP")
            {   
                newInvite.GuestsId=(int)Id;
                newInvite.WeddingsId=id;
                
            }
            else if (choice=="Un-RSVP")
            {
                
            }
            else if (choice=="Delete")
            {

            }


            ViewBag.Errors= new List<string>();
            return RedirectToAction("Show");
        }
        // public Weddings GetRecentWedding(){
        //      Weddings Wed= new Weddings();
        //         Wed = _context.Weddings
        //                 .Include(w=>w.Invitations)
        //                 .ThenInclude(b=>b.Guests)
        //         .SingleOrDefault(c=> c.WedderOne==WedView.WedderOne && c.WedderTwo==WedView.WedderTwo);
        //         return Wed;

        // }

        
        
        
        
       
    }
}
