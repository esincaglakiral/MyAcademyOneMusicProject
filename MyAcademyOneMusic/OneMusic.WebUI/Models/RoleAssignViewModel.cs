namespace OneMusic.WebUI.Models
{
    public class RoleAssignViewModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool RoleExist { get; set; } // o rol kullanıcıya atanmış mı atanmamış mı bunu tutar.
    }
}
