using System;

namespace SpotCheck.Models
{
   public class ParkingLot
   {
      public string Id { get; set; }
      public string Text { get; set; }
      public string Description { get; set; }

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