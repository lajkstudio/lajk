using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LajkStudio.Models;

namespace LajkStudio.Controllers;

[ApiController]
[Route("tervezes/")]
public class PersonalizationController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public PersonalizationController(AppDbContext context)
    {
        _context = context;
    }

    private static readonly ConcurrentDictionary<string, string> pageCache = new();
    
    [HttpGet("{id}/{colorNumber}")]
    public async Task<IActionResult> GetTervezesById(int id, int colorNumber)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        if (item == null) return NotFound();
        
        string htmlContent = GetHtmlContent(Path.Combine(Directory.GetCurrentDirectory(), "Skeletons", "terv.html"));
        
        htmlContent = htmlContent.Replace("{{name}}", item.Name);
        
        if (colorNumber == 1)
        {
            htmlContent = htmlContent.Replace("{{img}}", item.ImgUrl1);
        } 
        else if (colorNumber == 2)
        {
            htmlContent = htmlContent.Replace("{{img}}", item.ImgUrl2);
        } 
        else if (colorNumber == 3)
        {
            htmlContent = htmlContent.Replace("{{img}}", item.ImgUrl3);
        }
        else
        {
            return BadRequest();
        }
        
        htmlContent = htmlContent.Replace("{{price}}", item.Price.ToString());
        htmlContent = htmlContent.Replace("{{emoji_price}}", item.EmojiPriceIncrement.ToString());
        
        HttpContext.Session.SetString("SessionCooked", "yaaaaas");
        Console.WriteLine("Session: " + HttpContext.Session.Id);
        
        return Content(htmlContent, "text/html");
    }

    private static string GetHtmlContent(string fileName)
    {
        return pageCache.GetOrAdd(fileName, path => System.IO.File.ReadAllText(path));
    }
    
    [HttpPost("{id}/add-to-cart")]
    public async Task<IActionResult> AddToCart(int id, [FromForm] AddToCartRequest request)
    {
        var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == id);
        if (item == null) return NotFound();
        
        if (request.DesignImage.Length == 0)
        {
            return BadRequest("Image is required.");
        }

        byte[] imageBytes;
        using (var memoryStream = new MemoryStream())
        {
            await request.DesignImage.CopyToAsync(memoryStream);
            imageBytes = memoryStream.ToArray();
        }

        var sessionId = GetCartId();
        
        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.Id == sessionId);
        
        if (cart == null)
        {
            var newCart = new Cart
            {
                Id = sessionId
            };
    
            _context.Carts.Add(newCart);
        }
        else
        {
            cart.UpdatedAt = DateTime.Now;
        }
        await _context.SaveChangesAsync(); 
        
        int calculatedTotalPrice = item.Price + (request.EmojisUsed * item.EmojiPriceIncrement);

        if (calculatedTotalPrice != request.TotalPrice)
        {
            return BadRequest("Price doesn't match the item and the emojis used.");
        }
        
        var cartItem = new CartItem
        {
            CartId = sessionId,
            ItemId = item.Id,
            Img = imageBytes,
            TotalPrice = calculatedTotalPrice
        };
        
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
        
        return Ok();
    }

    [HttpGet("~/cart/items")]
    public async Task<IActionResult> GetCartItems()
    {
        var cartItems = await _context.CartItems
            .AsNoTracking()
            .Where(cartItem => cartItem.CartId == GetCartId())
            .OrderBy(cartItem => cartItem.CreatedAt)
            .Select(cartItem => new
            {
                id = cartItem.Id,
                name = cartItem.Item.Name,
                price = cartItem.TotalPrice,
                imageBytes = cartItem.Img
            })
            .ToListAsync();

        return Ok(new
        {
            items = cartItems.Select(cartItem => new
            {
                cartItem.id,
                cartItem.name,
                cartItem.price,
                image = Convert.ToBase64String(cartItem.imageBytes)
            }),
            total = cartItems.Sum(cartItem => cartItem.price)
        });
    }

    private Guid GetCartId()
    {
        var sessionHash = SHA256.HashData(Encoding.UTF8.GetBytes(HttpContext.Session.Id));
        return new Guid(sessionHash[..16]);
    }
    
    public class AddToCartRequest
    {
        public required IFormFile DesignImage { get; set; }
        public required int EmojisUsed { get; set; }
        public required int TotalPrice { get; set; }
    }
}
