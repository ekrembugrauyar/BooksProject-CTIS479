using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class RoleModel
    {
        public Role Record { get; set; }
        
        public string Name => Record.Name;

        [DisplayName("User Count")]
        public string UserCount => Record.Users?.Count.ToString() ?? "0";
    }
}
