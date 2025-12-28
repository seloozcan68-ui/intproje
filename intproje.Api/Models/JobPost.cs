using System.Collections.Generic;
namespace intproje.Api.Models {
    public class JobPost {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public List<Applicant> Applicants { get; set; } = new(); // One-to-Many 
    }
}