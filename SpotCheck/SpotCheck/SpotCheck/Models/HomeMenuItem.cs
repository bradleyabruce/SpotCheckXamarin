using System;
using System.Collections.Generic;
using System.Text;

namespace SpotCheck.Models
{
   public enum MenuItemType
   {
      Browse,
      About,
      Map
   }
   public class HomeMenuItem
   {
      public MenuItemType Id { get; set; }

      public string Title { get; set; }
   }
}
