namespace OtherService.Domain.Entities;

using Common.Entities;

public class Entity : AuditableBaseEntity
{
  public string Data { get; set; }
}
