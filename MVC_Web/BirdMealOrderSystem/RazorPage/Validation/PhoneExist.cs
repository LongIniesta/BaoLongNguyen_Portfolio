using Repository;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RazorPage.Validation
{
    public class PhoneExist : ValidationAttribute
    {
        IUserRepository userRepository = new UserRepository();
        public PhoneExist()
        {
            ErrorMessage = "Phone already use, can't register";
        }
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            string phone = value as string;
            if (userRepository.GetAll().ToList().SingleOrDefault(u => u.PhoneNumber == phone) != null
                || phone.Equals("0929828328"))
            {
                return false;
            }
            else return true;
        }
    }
}
