using Microsoft.AspNetCore.Mvc;
using SessionVar.Models.Classes;
using SessionVar.Models.Data;
using SessionVar.Models.Data.ViewModel;
using System.Security.Cryptography;

namespace SessionVar.Controllers
{
    public class AuthController : Controller
    {
        LoginModel _loginModel;
        Users _users;
        Context _db;
        
        public AuthController(LoginModel loginModel, Users users, Context db) //modeli tanımlarken constructer eklememiz gerekşyor (ctor)
        {
            _loginModel = loginModel;
            _db = db;
            _users = users;

        }

        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Register(LoginModel model)

        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);//out koyarsak girilen bilgiler kaybolmaz.
            Users newUser = new Users();
            newUser.UserId = model.UserId;
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            //Generate yapıyoruz yeni metot oluşturduğumuz için
            _db.Set<Users>().Add(newUser);
            _db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        private void CreatePasswordHash(string? password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View("Register",_loginModel);
        }

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {

            Users? user = _db.Set<Users>().Find(model.UserId);
            if (user==null)
            {
                return RedirectToAction("Login");

            }
            else
            {
                if (!VerifyPassword(model.Password, user.PasswordHash, user.PasswordSalt))
                {

                    return RedirectToAction("Login");
                }
                else
                {
                    string SessionKeyName = "User"; /*//Name altında the doctor diye değer saklıyor _age altında ise 73 dye değer saklıyor.*/
                   

                    HttpContext.Session.SetString(SessionKeyName, user.UserId);
                   //string ve int olarak döndürebilmek için böyle değerler tanımlıyoruz.
                    return RedirectToAction("Index", "Home");
                }
               
            }

            
        }
        private bool VerifyPassword(string password,byte[] userPasswordHash,byte[] userPasswordSalt)
        {
            var hmac=new HMACSHA512(userPasswordSalt);
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i <computedHash.Length; i++)
            {
                if (computedHash[i] != userPasswordHash[i])
                {
                    return false;
                }

            }
            return true; 
        }
    }   
}
