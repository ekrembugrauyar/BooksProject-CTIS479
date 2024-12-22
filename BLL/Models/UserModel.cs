using BLL.DAL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserModel
    {
        public User Record { get; set; }

        [DisplayName("Username")]
        public string UserName => Record.UserName;

        [DisplayName("Password")]
        public string Password => Record.Password;

        [DisplayName("Is Active?")]
        public string IsActive => Record.IsActive ? "Active" : "Not Active";

        [DisplayName("Role")]
        public string Role => Record.Role?.Name;
    }
}
