using System;
using System.Collections.Generic;

namespace wedding.Models
{
    public class Users
    {
        public int UsersId {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string Email {get;set;}
        public string Password {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}

        public List<Invitations>  Invitations {get;set;}


        public Users()
        {
            Invitations = new List<Invitations> ();
        }

       
    }
}