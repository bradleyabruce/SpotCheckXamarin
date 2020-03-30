using System;
using System.Collections.Generic;
using System.Text;

namespace SpotCheck.Models
{
    public class OldParkingLot
    {
        public Int64 lotId { get; set; }
        public String lotName { get; set; }
        public String address { get; set; }
        public int zipCode { get; set; }
        public String city { get; set; }
        public String state { get; set; }
        public Double lat { get; set; }
        public Double lon { get; set; }
        public int ContactId { get; set; }
        public int OpenSpots { get; set; }

    }
}
