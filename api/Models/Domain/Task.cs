namespace api.Models.Domain;

public class Task
{
    public Guid taskId { get; set; }
    public Guid subjectId { get; set; }
    public string taskName { get; set; }
    public int maxScore { get; set; }
    public int score { get; set; }
    public DateTime deadLine { get; set; }
    public Guid statusId { get; set; }

    public Subject Subject { get; set; }
    public Status Status { get; set; }
}