using System.ComponentModel.DataAnnotations;

namespace NetCoreApiTokenAuth.Models
{
    public class IpYetki
    {
        [Key]
        public int id { get; set; }
        public string ip { get; set; }
        public bool aktif { get; set; }
    }
}
