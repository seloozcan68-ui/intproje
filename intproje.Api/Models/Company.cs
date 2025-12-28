using System.Collections.Generic;
namespace intproje.Api.Models {
    public class Company {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Sector { get; set; } = "";
        public List<JobPost> JobPosts { get; set; } = new(); // One-to-Many 
    }
}