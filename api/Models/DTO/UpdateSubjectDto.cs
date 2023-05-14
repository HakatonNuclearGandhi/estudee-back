namespace api.Models.DTO;

public class UpdateSubjectDto
{
    public string subjectId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int maxScore { get; set; }
    public int currentScore { get; set; }
}