namespace UsersService.Domain.Entities;

using Common.Entities;

public class User : AuditableBaseEntity
{
  public string FirstName { get; set; }
}
