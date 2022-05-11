
using Domain.Entities;

namespace Infrastructure.Entities;

public class User : EntityBase, IEntity
{
   public int Id { get; set; }
   public string GoogleId { get; set; }
   public string FirstName { get; set; }
   public string LastName { get; set; }
   public string UserName { get; set; }
   public string Email { get; set; }
   public string ImageURL { get; set; }

}
