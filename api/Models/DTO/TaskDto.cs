namespace api.Models.DTO;

public class TaskDto
{
    public string subjectId { get; set; }
    public string taskName { get; set; }
    public int maxScore { get; set; }
    public DateTime deadline { get; set; }
}