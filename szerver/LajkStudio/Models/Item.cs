using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LajkStudio.Models;

[Table("item")] 
public class Item
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("img_url_1")]
    public string ImgUrl1 { get; set; } = string.Empty;

    [Column("img_url_2")]
    public string ImgUrl2 { get; set; } = string.Empty;

    [Column("img_url_3")]
    public string ImgUrl3 { get; set; } = string.Empty;

    [Column("price")]
    public int Price { get; set; }

    [Column("emoji_price_increment")]
    public int EmojiPriceIncrement { get; set; }
}