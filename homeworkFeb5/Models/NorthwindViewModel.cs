using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace homeworkFeb5.Models
{
    public class NorthwindViewModel
    {
        public List<Order> orders { get; set; } = new List<Order>();
        public DateTime date = DateTime.Today;

    }
}