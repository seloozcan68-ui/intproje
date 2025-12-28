namespace intproje.Api.Models {
    public class Applicant {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CvSummary { get; set; } = ""; // Ödevde istenen CV özeti [cite: 14]
        public int JobPostId { get; set; }
        public JobPost? JobPost { get; set; }
    }
}