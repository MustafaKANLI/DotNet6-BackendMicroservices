namespace OtherService.Infrastructure.Persistence.Seeds;

using OtherService.Application.Interfaces.Repositories;
using OtherService.Domain.Entities;
using Newtonsoft.Json;

public class DefaultEntities
{
  public static async Task<bool> SeedAsync(IEntityRepositoryAsync categoryRepository)
  {
    var mockData = File.ReadAllText(Path.Combine(
      Directory.GetCurrentDirectory(),
      @"SeedData/ENTITY_MOCK_DATA.json"));

    var deserializedMockData = JsonConvert.DeserializeObject<List<Entity>>(mockData);

    var _item1 = deserializedMockData[0];

    var itemList = await categoryRepository.GetAllAsync();
    var _itemCount = itemList.Where(i => i.Data.StartsWith(_item1.Data)).Count();

    if (_itemCount > 0) // ALREADY SEEDED
      return true;

    try
    {
      foreach (var deserializedItem in deserializedMockData)
      {
        await categoryRepository.AddAsync(deserializedItem);
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      throw;
    }

    return true; // NOT ALREADY SEEDED
  }
}
