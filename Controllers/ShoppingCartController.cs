using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanBanhMi.Models;

namespace WebBanBanhMi.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult CartSummary()
        {
            ViewBag.CartCount = GetShoppingCartFromSession().Count();
            return PartialView("CartSummary");
        }

        public List<CartItem> GetShoppingCartFromSession()
        {
            var listShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if (listShoppingCart == null)
            {
                listShoppingCart = new List<CartItem>();
                Session["ShoppingCart"] = listShoppingCart;
            }
            return listShoppingCart;
        }
        public ActionResult Index()
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            if (ShoppingCart.Count == 0)
            {
                return RedirectToAction("Index", "BanhMi");
            }
            ViewBag.TongSoLuong = ShoppingCart.Sum(p => p.Quantity);
            ViewBag.TongTien = ShoppingCart.Sum(p => p.Quantity * p.Price);
            return View(ShoppingCart);
        }
        public RedirectToRouteResult AddToCart(int id)
        {
            BanhMiModelContext db = new BanhMiModelContext();
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            CartItem findCardItem = ShoppingCart.FirstOrDefault(m => m.Id == id);
            if (findCardItem == null)
            {
                BanhMi bread = db.BanhMis.First(m => m.Id == id);
                CartItem newItem = new CartItem()
                {
                    Id = bread.Id,
                    Name = bread.Name,
                    Quantity = 1,
                    Image = bread.Image,
                    Price = bread.Price.Value
                };
                ShoppingCart.Add(newItem);
            }
            else
                findCardItem.Quantity++;
            return RedirectToAction("Index", "ShoppingCart");
        }
        public RedirectToRouteResult RemoveCartItem(int id)
        {
            BanhMiModelContext db = new BanhMiModelContext();
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            CartItem findCardItem = ShoppingCart.FirstOrDefault(m => m.Id == id);
            ShoppingCart.Remove(findCardItem);
            return RedirectToAction("Index", "ShoppingCart");
        }
        public RedirectToRouteResult UpdateCart(int id, int txtQuantity)
        {
            var itemFind = GetShoppingCartFromSession().FirstOrDefault(p => p.Id == id);
            if (itemFind != null)
                itemFind.Quantity = txtQuantity;
            return RedirectToAction("Index", "ShoppingCart");
        }
        public ActionResult Order()
        {
            OrderModelContext context = new OrderModelContext();
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order max = context.Orders.OrderByDescending(u => u.OrderNo).FirstOrDefault();
                    int a = max.OrderNo;
                    Order objOrder = new Order()
                    {
                        OrderNo = a++,
                        CustomerId = null,
                        OrderDate = DateTime.Now,
                        DeliveryDate = null,
                        isPaid = false,
                        isComplete = false

                    };
                    objOrder = context.Orders.Add(objOrder);
                    context.SaveChanges();
                    List<CartItem> listCartItems = GetShoppingCartFromSession();
                    foreach (var item in listCartItems)
                    {
                        OrderDetail ctdh = new OrderDetail()
                        {
                            OrderNo = objOrder.OrderNo,
                            Id = item.Id,
                            Quantity = item.Quantity,
                            Price = item.Price
                        };
                        context.OrderDetails.Add(ctdh);
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return Content("Gặp lỗi khi đặt hàng: " + ex.Message);
                }
            }
            Session["Giohang"] = null;
            return RedirectToAction("ConfirmOrder", "ShoppingCart");
        }
        public ActionResult ConfirmOrder()
        {
            return View();
        }
    }
}