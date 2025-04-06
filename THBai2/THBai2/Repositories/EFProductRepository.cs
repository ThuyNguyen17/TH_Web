using Microsoft.EntityFrameworkCore;
using THBai2.Models;

namespace THBai2.Repositories
{
    public class EFProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // Không có Include() : Chỉ tải Products, Category bị null.
        // Có Include(): Tải cả Products và Categories, dữ liệu Category có sẵn. => product.Category?.CategoryName
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            // return await _context.Products.FindAsync(id); 
            // lấy thông tin kèm theo category 
            return await _context.Products.Include(p =>p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }
        // FirstOrDefaultAsync là truy vấn có điều kiện, hỗ trỡ include
        // FindAsync không hỗ trợ include
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
