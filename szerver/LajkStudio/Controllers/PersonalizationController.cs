using System.Collections.Concurrent;
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
        
        return Content(htmlContent, "text/html");
    }

    private static string GetHtmlContent(string fileName)
    {
        return pageCache.GetOrAdd(fileName, path => System.IO.File.ReadAllText(path));
    }
}