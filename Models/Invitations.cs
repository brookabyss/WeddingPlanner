using System;
using System.Collections.Generic;


namespace wedding.Models
{
    public class Invitations 
    {
        public int  InvitationsId {get;set;}

        public int GuestsId {get;set;}
        public Users Guests {get;set;}

        public int WeddingsId {get;set;}
        public Weddings Weddings {get;set;}
        
    }
}