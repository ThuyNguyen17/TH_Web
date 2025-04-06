using THBai2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using THBai2.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace THBai2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }
    //// Hiển thị form thêm sản phẩm mới 
    //public async Task<IActionResult> Add()
    //{
    //    var categories = await _categoryRepository.GetAllAsync();
    //    ViewBag.Categories = new SelectList(categories, "Id", "Name");

    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> Add(Product product, IFormFile imageUrl)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        if (imageUrl != null)
    //        {
    //            product.ImageUrl = await SaveImage(imageUrl);
    //        }

    //        await _productRepository.AddAsync(product);
    //        return RedirectToAction(nameof(Index));
    //    }
    //    var categories = await _categoryRepository.GetAllAsync();
    //    ViewBag.Categories = new SelectList(categories, "Id", "Name");
    //    return View(product);
    //}
    //private async Task<string> SaveImage(IFormFile image)
    //{
    //    // Thay đổi đường dẫn theo cấu hình của bạn 
    //    var savePath = Path.Combine("wwwroot/Images", image.FileName);
    //    using (var fileStream = new FileStream(savePath, FileMode.Create))
    //    {
    //        await image.CopyToAsync(fileStream);
    //    }
    //    return "/Images/" + image.FileName; // Trả về đường dẫn tương đối 
    //}
    //public IActionResult Privacy()
    //{
    //    return View();
    //}

    //public async Task<IActionResult> Display(int id)
    //{
    //    var product = await _productRepository.GetByIdAsync(id);
    //    if (product == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(product);
    //}
    //public async Task<IActionResult> Update(int id)
    //{
    //    var product = await _productRepository.GetByIdAsync(id);
    //    if (product == null)
    //    {
    //        return NotFound();
    //    }

    //    var categories = await _categoryRepository.GetAllAsync();
    //    ViewBag.Categories = new SelectList(categories, "Id", "Name",product.CategoryId);
    //    return View(product);
    //}
    //[HttpPost]
    //public async Task<IActionResult> Update(int id, Product product,IFormFile imageUrl)
    //{
    //    ModelState.Remove("ImageUrl"); 
    //    if (id != product.Id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {

    //        var existingProduct = await _productRepository.GetByIdAsync(id);
    //        if (imageUrl == null)
    //        {
    //            product.ImageUrl = existingProduct.ImageUrl;
    //        }
    //        else
    //        {
    //            product.ImageUrl = await SaveImage(imageUrl);
    //        }
    //        existingProduct.Name = product.Name;
    //        existingProduct.Price = product.Price;
    //        existingProduct.Description = product.Description;
    //        existingProduct.CategoryId = product.CategoryId;
    //        existingProduct.ImageUrl = product.ImageUrl;
    //        await _productRepository.UpdateAsync(existingProduct);
    //        return RedirectToAction(nameof(Index));
    //    }
    //    var categories = await _categoryRepository.GetAllAsync();
    //    ViewBag.Categories = new SelectList(categories, "Id", "Name");
    //    return View(product);
    //}
    //public async Task<IActionResult> Delete(int id)
    //{
    //    var product = await _productRepository.GetByIdAsync(id);
    //    if (product == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(product);
    //}
    //[HttpPost, ActionName("DeleteConfirmed")]
    //public async Task<IActionResult> DeleteConfirmed(int id)
    //{
    //    await _productRepository.DeleteAsync(id);
    //    return RedirectToAction(nameof(Index));
    //}
}
}
