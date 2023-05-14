namespace api.Models.DTO;

public class GetTaskDto : TaskDto
{
    public string taskId { get; set; }
    public int score { get; set; }
    public string statusName { get; set; }
}