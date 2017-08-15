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
    public class UserController : Controller
    {
        private WeddingContext _context;
        public UserController(WeddingContext context){
            _context=context;
        }

        // private Users GetUsers(){
        //     Users currentUser= new Users();
        //     int? Id= HttpContext.Session.GetInt32("UserId");
        //     currentUser = _context.Users
        //                                 .Include(a=>a.JointAccounts)
        //                                 .ThenInclude(c=>c.Accounts).SingleOrDefault(user=>user.UsersId==(int)Id);
        //     System.Console.WriteLine($"The joint account {currentUser.JointAccounts.Count}");
        //     return currentUser;
        // }
        private List<Users> GetPopulatedUser(){
            int? Id= HttpContext.Session.GetInt32("UserId");
            List<Users> PopulatedUserAccounts=new List<Users> ();
           
            PopulatedUserAccounts = _context.Users
                                    .Include(a=>a.Invitations)
                                    .ThenInclude(c=>c.Weddings)
                                    .Where(user=>user.UsersId==(int)Id).ToList();
            
            return PopulatedUserAccounts;
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.Errors=new List<string>();

            return View();
        }

        [HttpGet]
        [Route("user/registration")]
        public IActionResult ToRegister(UsersView newUser){
            ViewBag.Errors=new List<string>();
            return View("Registration");
        }



       // Register function

        [HttpPost]
        [Route("user/register")]
        public IActionResult Register(Users newUser)
        {
            System.Console.WriteLine(ModelState.IsValid);
            
            if(ModelState.IsValid){
                    System.Console.WriteLine(newUser.FirstName);
                   List<Users> UserCheck= _context.Users.Where(user=>user.Email==newUser.Email).ToList();
                    if (UserCheck.Count>0){
                        ViewBag.Errors.Add("Why don't you try a different email, it seems that the previous email was taken");
                        return View("Registration");
                    }
                    Users createdUser= new Users{
                        FirstName=newUser.FirstName,
                        LastName=newUser.LastName,
                        Email=newUser.Email,
                        Password= newUser.Password,
                        CreatedAt= DateTime.Now,
                        UpdatedAt=DateTime.Now,
                    };
                    _context.Users.Add(createdUser);
                    _context.SaveChanges();
                    Users ReturnedUser = _context.Users.SingleOrDefault(user => user.Email == createdUser.Email);
                    System.Console.WriteLine($"Email from returned {ReturnedUser.Email}");
                    HttpContext.Session.SetInt32("UserId",(int)ReturnedUser.UsersId);  
                    return RedirectToAction("Show","Wedding");
                    
            }
            else{
                    ViewBag.Errors=ModelState.Values;
                    return View("Registration");
            }
            
        }
        [HttpPost]
        [Route("user/login")]
        public IActionResult Login(string Email, string Password)
        {
               ViewBag.errors=new List<string>();
               Users ReturnedUser= _context.Users.SingleOrDefault(user=>user.Email==Email);
               System.Console.WriteLine($"The returned user inside Login line 100 {ReturnedUser}");
               if (ReturnedUser==null){
                   ViewBag.errors.Add("User email doesn't exist please register") ;
                   return RedirectToAction("Index");
               }
               if(ReturnedUser.Password==Password){
                    HttpContext.Session.SetInt32("UserId",(int)ReturnedUser.UsersId);
                    System.Console.WriteLine("Inside Login{0}",HttpContext.Session.GetInt32("UserId"));
                    // Users populatedUser=GetUsers();
                    // System.Console.WriteLine($"The populated user data with the joint accounts{populatedUser.JointAccounts.Count}");
                    // if(populatedUser.JointAccounts.Count==0){
                    //     Accounts newAccount=new Accounts{
                    //         CurrentBalance=50,
                    //         Transaction=50,
                    //         CreatedAt=DateTime.Now,
                    //         UpdatedAt=DateTime.Now,
                    //     };
                    //     _context.Accounts.Add(newAccount);
                    //     _context.SaveChanges();


                    // }
                    return RedirectToAction("Show","Wedding");
               }
               else{
                   System.Console.WriteLine("Say Wahts&&&&&&&");
                   List<string> errors=new List<string>();
                   errors.Add("Email or password incorrect");
                   ViewBag.Errors=errors;
               }
            return View("Index");
        }









        [HttpGet]
        [Route("user/logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
