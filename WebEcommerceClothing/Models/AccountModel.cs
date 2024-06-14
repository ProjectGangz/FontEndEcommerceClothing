using System.ComponentModel.DataAnnotations;

namespace WebEcommerceClothing.Models
{
    public class AccountModel
    {
        [Key]
        public int AccountId { get; set; }
        public string UserName { get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
    }
}
