namespace api.Models.Domain;

public class User
{
    public Guid userId { get; set; }

    public string userName { get; set; }

    public string email { get; set; }

    public string passwod { get; set; }
}