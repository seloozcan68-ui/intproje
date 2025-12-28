namespace intproje.Mvc.Models
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string CvSummary { get; set; } = "";
        public int JobPostId { get; set; }
        public JobPost? JobPost { get; set; }
    }
}

