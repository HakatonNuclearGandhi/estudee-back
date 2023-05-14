namespace api.Models.Domain;

public class Subject
{
    public Guid subjectId { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int maxScore { get; set; }
    public int currentScore { get; set; }

    public User User { get; set; }
}