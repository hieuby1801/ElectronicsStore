using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectronicsStore.Models;

namespace ElectronicsStore.Controllers
{
    public class CartDetailsController : Controller
    {
        private readonly DbMSContextNew _context;

        public CartDetailsController(DbMSContextNew context)
        {
            _context = context;
        }

        // GET: CartDetails
        public async Task<IActionResult> Index()
        {
            var dbMSContextNew = _context.CartDetails.Include(c => c.IdCartNavigation).Include(c => c.IdProductNavigation);
            return View(await dbMSContextNew.ToListAsync());
        }

        // GET: CartDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartDetail = await _context.CartDetails
                .Include(c => c.IdCartNavigation)
                .Include(c => c.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdCartDetail == id);
            if (cartDetail == null)
            {
                return NotFound();
            }

            return View(cartDetail);
        }

        // GET: CartDetails/Create
        public IActionResult Create()
        {
            ViewData["IdCart"] = new SelectList(_context.Carts, "IdCart", "IdCart");
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int count)
        {
            CartDetail cartDetail = new CartDetail
            {
                IdCartDetail = 1,
                IdCart = 1,
                IdProduct = productId,
                Count = count
            };
            await _context.SaveChangesAsync();
            return RedirectToAction("Create", new { IdCartDetail = cartDetail.IdCartDetail, IdCart = cartDetail.IdCart, IdProduct = cartDetail.IdProduct, Count = cartDetail.Count }); ; // Chuyển hướng về trang giỏ hàng
        }

        // POST: CartDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int productId, int count, int cartId)
        {
            CartDetail cartDetail = new CartDetail
            {
                IdCart = 1,
                IdProduct = productId,
                Count = count
            };
            if (ModelState.IsValid)
            {
                // Kiểm tra xem mục với IdCart, IdProduct đã tồn tại chưa
                var existingCartDetail = await _context.CartDetails
                    .FirstOrDefaultAsync(c => c.IdCart == cartDetail.IdCart && c.IdProduct == cartDetail.IdProduct);
                if (existingCartDetail != null)
                {
                    // Nếu đã tồn tại, bạn có thể cập nhật số lượng (Count)
                    existingCartDetail.Count += cartDetail.Count; // Hoặc thay thế bằng giá trị mới tùy theo yêu cầu
                    _context.Update(existingCartDetail);
                }
                else
                {
                    // Nếu chưa tồn tại, thêm mục mới
                    _context.Add(cartDetail);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCart"] = new SelectList(_context.Carts, "IdCart", "IdCart", cartDetail.IdCart);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", cartDetail.IdProduct);
            return View(cartDetail);
        }

        // GET: CartDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartDetail = await _context.CartDetails.FindAsync(id);
            if (cartDetail == null)
            {
                return NotFound();
            }
            ViewData["IdCart"] = new SelectList(_context.Carts, "IdCart", "IdCart", cartDetail.IdCart);
            ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", cartDetail.IdProduct);
            ViewData["IdCartDetail"] = cartDetail.IdCartDetail;
            ViewData["Count"] = cartDetail.Count;
            return View(cartDetail);
        }

        // POST: CartDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CartDetail cartDetail)
        {
            //if (id != cartDetail.IdCartDetail)
            //{
            //    return NotFound();
            //}

            
                try
                {
                    _context.Update(cartDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartDetailExists(cartDetail.IdCartDetail))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            //ViewData["IdCart"] = new SelectList(_context.Carts, "IdCart", "IdCart", cartDetail.IdCart);
            //ViewData["IdProduct"] = new SelectList(_context.Products, "IdProduct", "IdProduct", cartDetail.IdProduct);
            //return View(cartDetail);
        }

        // GET: CartDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartDetail = await _context.CartDetails
                .Include(c => c.IdCartNavigation)
                .Include(c => c.IdProductNavigation)
                .FirstOrDefaultAsync(m => m.IdCartDetail == id);
            if (cartDetail == null)
            {
                return NotFound();
            }

            return View(cartDetail);
        }

        // POST: CartDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cartDetail = await _context.CartDetails.FindAsync(id);
            if (cartDetail != null)
            {
                _context.CartDetails.Remove(cartDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartDetailExists(int id)
        {
            return _context.CartDetails.Any(e => e.IdCartDetail == id);
        }
    }
}
