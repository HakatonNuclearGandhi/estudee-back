namespace api.Models.DTO;

public class GetTaskDto
{
    public string taskId { get; set; }
    public int score { get; set; }
    public string statusName { get; set; }
    public string subjectId { get; set; }
    public string taskName { get; set; }
    public int maxScore { get; set; }
    public DateTime deadline { get; set; }
}