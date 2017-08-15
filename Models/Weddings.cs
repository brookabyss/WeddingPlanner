using System;
using System.Collections.Generic;
namespace wedding.Models
{
    public class Weddings
    {
        public int WeddingsId {get;set;}
        public DateTime Date {get;set;}
        public string Address {get;set;}
        public string WedderOne {get;set;}
        public string WedderTwo {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
        
        public List<Invitations>  Invitations {get;set;}


        public Weddings()
        {
            Invitations = new List<Invitations> ();
        }
       
    }
}