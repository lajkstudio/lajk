namespace LajkStudio.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public string CartId { get; set; }
        public byte[] Img { get; set; } 
        public int TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }

        public Cart Cart { get; set; }
    }
}