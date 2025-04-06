
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using THBai2.Models;

namespace THBai2.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ShoppingCartController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new ShoppingCart() : JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new ShoppingCart() : JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
            cart.AddItem(new CartItem
            {
                ProductId = productId,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity
            });
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));

            return RedirectToAction("Index");
        }
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new ShoppingCart() : JsonConvert.DeserializeObject<ShoppingCart>(cartJson);

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null && quantity > 0)
            {
                item.Quantity = quantity;
            }

            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? new ShoppingCart() : JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
            if (cart != null)
            {
                cart.RemoveItem(productId);
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
            }
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            var cart = string.IsNullOrEmpty(cartJson) ? null : JsonConvert.DeserializeObject<ShoppingCart>(cartJson);
            if (cart == null || !cart.Items.Any())
            {
                ModelState.AddModelError("", "Your cart is empty.");
                return View(order);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }

            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow;
            order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
            order.OrderDetails = cart.Items.Select(i => new OrderDetail
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();

            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            HttpContext.Session.Remove("Cart");
            return View("OrderCompleted", order.Id);
        }

        public IActionResult History()
        {
            var orders = _context.Order.Include(o => o.ApplicationUser)
                                        .Include(o => o.OrderDetails)
                                        .ToList();
            return View(orders);
        }
    }
}