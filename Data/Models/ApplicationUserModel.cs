using System.Threading.Tasks;

namespace gantt_backend.Data.Models
{
    public class ApplicationUserModel
    {
        public string UserName { get; set; }
        public string Email {get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

    }
}