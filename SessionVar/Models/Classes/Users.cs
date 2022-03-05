using System.ComponentModel.DataAnnotations;

namespace SessionVar.Models.Classes
{
    public class Users
    {
        [Key]
        
        public string ?UserId { get; set; }
        public byte[] ?PasswordHash { get; set; } //Özel karakterlerin de içinde olabilmesi için byte kullanıyoruz
        public byte[] ?PasswordSalt { get; set; }

    }
}
