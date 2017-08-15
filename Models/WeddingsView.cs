
using System.ComponentModel.DataAnnotations;
using System;
namespace wedding.Models
{
    public class WeddingsView 
    {

        [Required(ErrorMessage="Date Field Required")]
        public DateTime Date {get;set;}

        [Required(ErrorMessage="Address Field Required")]
        public string Address {get;set;}

        [Required(ErrorMessage="Wedder One Field Required")]
        public String WedderOne {get;set;}

        [Required(ErrorMessage="Wedder TwoField Required")]
        public String WedderTwo {get;set;}
        

    }
    
}