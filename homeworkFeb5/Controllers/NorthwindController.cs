using homeworkFeb5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace homeworkFeb5.Controllers
{
    public class NorthwindController : Controller
    {
        public ActionResult Orders()
        {
            NorthwindManger nm = new NorthwindManger(Properties.Settings.Default.ConStr);
            NorthwindViewModel viewModel = new NorthwindViewModel();
            viewModel.orders = nm.GetOrders();
            return View(viewModel);
        }

        public ActionResult OrderDetails()
        {
            NorthwindManger nm = new NorthwindManger(Properties.Settings.Default.ConStr);
            NorthwindViewModel viewModel = new NorthwindViewModel();
            viewModel.orders = nm.GetOrders(1997);
            return View(viewModel);
        }
    }
}