using System;
using System.Collections.Generic;
using System.Text;

namespace SpotCheck.Models
{
    class User
    {
        private int UserID { get; set; }
        private String LastName { get; set; }
        private String FirstName { get; set; }
        private String Username { get; set; }
        private String Password { get; set; }
        private String EmailAddress { get; set; }
        private Double Latitude { get; set; }
        private Double Longitude { get; set; }
    }
}
