using ASM.Helpers;
using ASM.Models;
using ASM_THIEN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ASM_THIEN.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        AsmContext _context;

        public HomeController(ILogger<HomeController> logger, AsmContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_context.Products.ToList());
        }

        public IActionResult Login()
        {
            return View();
            // đợi t gỡ mấy file thư viện ra,
        }

        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            AddCart addCart = new AddCart() { Id = id, Quantity = 1 };
            ViewData["product"] = product;
            return View(addCart);
        }

        [HttpPost]
        public async Task<IActionResult> Buy(AddCart addCard)
        {
            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem() { product = await _context.Products.FindAsync(addCard.Id), Quantity = addCard.Quantity });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

                int index = IsExist(addCard.Id);
                if (index != -1)
                {
                    cart[index].Quantity += addCard.Quantity;
                }
                else
                {
                    cart.Add(new CartItem { product = await _context.Products.FindAsync(addCard.Id), Quantity = addCard.Quantity });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Cart");
        }
        private int IsExist(int id)
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].product.Id.Equals(id))
                    return i;
            }
            return -1;
        }

        public IActionResult Cart()
        {
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            if (cart != null)
            {
                ViewBag.Total = cart.Sum(i => i.product.Price * i.Quantity);
            }
            else
            {
                ViewBag.Total = 0;
            }

            return View(cart);
        }

        [HttpPost]
        public IActionResult Update(IFormCollection fc)
        {
            StringValues quantites;
            fc.TryGetValue("quantity", out quantites);
            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].Quantity = Convert.ToInt32(quantites[i]);
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Cart");
        }
        public IActionResult Remove(int index)
        {

            List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);

            return RedirectToAction("Cart");
        }
     
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
