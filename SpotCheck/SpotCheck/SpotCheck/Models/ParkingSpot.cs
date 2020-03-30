using System;
using System.Collections.Generic;
using System.Text;

namespace SpotCheck.Models
{
    class ParkingSpot
    {
        public int spotId { get; set; }
        public int floorNum { get; set; }
        public int lotId { get; set; }
        public bool isOpen { get; set; }
        public int deviceId { get; set; }
        public int topLeftXCoordinate { get; set; }
        public int topLeftYCoordinate { get; set; }
        public int bottomRightXCoordinate { get; set; }
        public int bottomRightYCoordinate { get; set; }
        public DateTime updateDate { get; set; }
    }
}
