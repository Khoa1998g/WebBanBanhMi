using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanBanhMi.Models;
using PagedList;

namespace WebBanBanhMi.Controllers
{
    public class BanhMiController : Controller
    {
        // GET: BanhMi
        public ActionResult Index(int? page)
        {
            var context = new BanhMiModelContext();
            int pageSize = 6; //So luong trang
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.BanhMis.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }
        public ActionResult GetBanhMiByCategory(int id)
        {
            var context = new BanhMiModelContext();
            return View("Index", context.BanhMis.Where(p => p.CategoryId == id).ToList().ToPagedList(1,6));
        }
        public ActionResult GetCategory()
        {
            var context = new BanhMiModelContext();
            var listCategory = context.Categories.ToList();
            return PartialView(listCategory);
        }
        public ActionResult Details(int id)
        {
            var context = new BanhMiModelContext();
            var firstBanhMi = context.BanhMis.FirstOrDefault(p => p.Id == id);
            if (firstBanhMi == null)
                return HttpNotFound("Không tìm thấy mã sách này!");
            return View(firstBanhMi);
        }
        [Authorize]
        public ActionResult Search(string searchString)
        {
            var context = new BanhMiModelContext();
            var result =
                (from m in context.BanhMis
                 where
                    m.Name.Contains(searchString)
                    || m.Description.Contains(searchString)
                 select m);
            if (result.Count() > 0)
                return View("Index", result.ToList().ToPagedList(1,2));
            return HttpNotFound("Thông tin tìm kiếm chưa có, Xin mời nhập thông tin khác");
        }
    }
}