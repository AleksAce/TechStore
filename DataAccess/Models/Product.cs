﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
   [Serializable]
   public class Product
    {
        //Basic Info
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DateTime? DateAdded { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Description { get; set; }
        public string PhotoURL { get; set; }
        public float Price { get; set; }
        //Stock info
        public bool IsInStock { get; set; }
        public int LeftInStock { get; set; }

        public DateTime? AvailableUntill { get; set; }
        public int  AvailableInStockTime { get; set; }
        //Category Info


        public virtual List<Category> Categories { get; set; }

        public Product()
        {
            DateAdded = DateTime.Now;
            DateUpdated = DateTime.Now;
        }
        public bool isForMainPage { get; set; }
        //Used for sorting on the main page
        public DateTime? addedToMainPageDate { get; set; }

      
    }
}
