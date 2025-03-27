using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopping.Models;
using Shopping.Models.Repository;

namespace Shopping_Tutorial.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Dashboard")]
    [Authorize(Roles = "Publisher,Author,Admin")]
    public class DashboardController : Controller
    {
        private readonly DataContext _dataContext;

        public DashboardController(DataContext context)
        {
            _dataContext = context;
        }

        public IActionResult Index()
        {
            var count_product = _dataContext.Products.Count();
            var count_order = _dataContext.Orders.Count();
            var count_category = _dataContext.Categories.Count();
            var count_user = _dataContext.Users.Count();
            ViewBag.CountProduct = count_product;
            ViewBag.CountOrder = count_order;
            ViewBag.CountCategory = count_category;
            ViewBag.CountUser = count_user;
            return View();
        }

        [HttpPost]
        [Route("SubmitFilterDate")]
        public IActionResult SubmitFilterDate(string filterdate)
        {
            var dateselect = DateTime.Parse(filterdate).ToString("yyyy-MM-dd");
            var chartData = _dataContext.Orders
                .Where(o => o.CreateDate.ToString("yyyy-MM-dd") == dateselect)
                .Join(_dataContext.OrderDetails,
                    o => o.OrderCode,
                    od => od.OrderCode,
                    (o, od) => new StatisticalModel
                    {
                        date = o.CreateDate,
                        revenue = od.Quantity * od.Price,
                        orders = 1
                    })
                .GroupBy(s => s.date)
                .Select(group => new StatisticalModel
                {
                    date = group.Key,
                    revenue = group.Sum(s => (decimal)s.revenue),
                    orders = group.Count()
                })
                .ToList();

            return Json(chartData);
        }

        [HttpPost]
        [Route("SelectFilterDate")]
        public IActionResult SelectFilterDate(string filterdate)
        {
            var chartData = new List<StatisticalModel>();
            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);

            if (filterdate == "last_month")
            {
                var first = month.AddMonths(-1);
                var last = month.AddDays(-1);
                chartData = _dataContext.Orders
                    .Where(o => o.CreateDate >= first && o.CreateDate <= last)
                    .Join(_dataContext.OrderDetails,
                        o => o.OrderCode,
                        od => od.OrderCode,
                        (o, od) => new StatisticalModel
                        {
                            date = o.CreateDate,
                            revenue = od.Quantity * od.Price,
                            orders = 1
                        })
                    .GroupBy(s => s.date)
                    .Select(group => new StatisticalModel
                    {
                        date = group.Key,
                        revenue = group.Sum(s => (decimal)s.revenue),
                        orders = group.Count()
                    })
                    .ToList();
            }
            else if (filterdate == "this_month")
            {
                chartData = _dataContext.Orders
                    .Where(o => o.CreateDate >= month && o.CreateDate <= today)
                    .Join(_dataContext.OrderDetails,
                        o => o.OrderCode,
                        od => od.OrderCode,
                        (o, od) => new StatisticalModel
                        {
                            date = o.CreateDate,
                            revenue = od.Quantity * od.Price,
                            orders = 1
                        })
                    .GroupBy(s => s.date)
                    .Select(group => new StatisticalModel
                    {
                        date = group.Key,
                        revenue = group.Sum(s => (decimal)s.revenue),
                        orders = group.Count()
                    })
                    .ToList();
            }
            else if (filterdate == "all_year")
            {
                var yearStart = new DateTime(today.Year, 1, 1);
                chartData = _dataContext.Orders
                    .Where(o => o.CreateDate >= yearStart && o.CreateDate <= today)
                    .Join(_dataContext.OrderDetails,
                        o => o.OrderCode,
                        od => od.OrderCode,
                        (o, od) => new StatisticalModel
                        {
                            date = o.CreateDate,
                            revenue = od.Quantity * od.Price,
                            orders = 1
                        })
                    .GroupBy(s => s.date)
                    .Select(group => new StatisticalModel
                    {
                        date = group.Key,
                        revenue = group.Sum(s => (decimal)s.revenue),
                        orders = group.Count()
                    })
                    .ToList();
            }

            return Json(chartData);
        }

        [HttpPost]
        [Route("GetChartData")]
        public IActionResult GetChartData()
        {
            var chartData = _dataContext.Orders
                .Join(_dataContext.OrderDetails,
                    o => o.OrderCode,
                    od => od.OrderCode,
                    (o, od) => new StatisticalModel
                    {
                        date = o.CreateDate,
                        revenue = od.Quantity * od.Price,
                        orders = 1
                    })
                .GroupBy(s => s.date)
                .Select(group => new StatisticalModel
                {
                    date = group.Key,
                    revenue = group.Sum(s => (decimal)s.revenue),
                    orders = group.Count()
                })
                .ToList();

            return Json(chartData);
        }
    }
}