namespace api.Models.DTO;

public class UpdateTaskDto
{
    public string subjectId { get; set; }
    public string taskName { get; set; }
    public int maxScore { get; set; }
    public int score { get; set; }
    public string statusId { get; set; }
    public DateOnly deadline { get; set; }
}