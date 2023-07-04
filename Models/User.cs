using System.Text.Json.Serialization;

namespace JTable_test2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Score { get; set; }

        public int? Gender { get; set; }
        public int? IsActive { get; set; }
        public int? IsToggled { get; set; }
    }
}
