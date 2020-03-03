using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cart.Models;

namespace Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartContext _context;

        public CartController(CartContext context)
        {
            _context = context;
        }

        // GET: api/Cart
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartItems>>> GetCartItems()
        {
            return await _context.CartItems.ToListAsync();
        }

        // GET: api/Cart/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartItems>> GetCartItems(int id)
        {
            var cartItems = await _context.CartItems.FindAsync(id);

            if (cartItems == null)
            {
                return NotFound();
            }

            return cartItems;
        }

        // PUT: api/Cart/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartItems(int id, CartItems cartItems)
        {
            if (id != cartItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(cartItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartItemsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cart
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CartItems>> PostCartItems(CartItems cartItems)
        {
            _context.CartItems.Add(cartItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCartItems), new { id = cartItems.Id }, cartItems);
        }

        // DELETE: api/Cart/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartItems>> DeleteCartItems(int id)
        {
            var cartItems = await _context.CartItems.FindAsync(id);
            if (cartItems == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItems);
            await _context.SaveChangesAsync();

            return cartItems;
        }

        private bool CartItemsExists(int id)
        {
            return _context.CartItems.Any(e => e.Id == id);
        }


        //Count the numbers in the cart
        [HttpGet("CartNum/{Id}")]

        public async Task<ActionResult<IEnumerable<OrderStatus>>> GetNum(int Id)
        {
            
            var numItems = await _context.OrderStatus.ToListAsync();

            if (numItems== null)
            {
                return NotFound();
            }
            numItems.RemoveAll(t => t.CSUNID != Id);
            return numItems;
        }
    }
}
