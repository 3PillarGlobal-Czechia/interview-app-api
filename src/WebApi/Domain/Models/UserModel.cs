namespace Domain.Models;

public class UserModel : ModelBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
