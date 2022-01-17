using Domain.Entities;

namespace Infrastructure.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
