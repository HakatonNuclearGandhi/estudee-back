namespace api.Models.DTO;

public class TaskResponseDto
{
    public Guid subjectId { get; set; }
    public string subjectName { get; set; }
    public string taskName { get; set; }
    public int maxScore { get; set; }
    public DateTime deadline { get; set; }
    public string statusName { get; set; }
    public Guid statusId { get; set; }
}