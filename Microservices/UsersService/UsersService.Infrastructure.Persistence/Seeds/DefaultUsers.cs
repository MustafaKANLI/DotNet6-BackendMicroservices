namespace UsersService.Infrastructure.Persistence.Seeds;

using UsersService.Application.Interfaces.Repositories;
using UsersService.Domain.Entities;
using Newtonsoft.Json;

public class DefaultUsers
{
  public static async Task<bool> SeedAsync(IUserRepositoryAsync categoryRepository)
  {
    var mockData = File.ReadAllText(Path.Combine(
      Directory.GetCurrentDirectory(),
      @"SeedData/USER_MOCK_DATA.json"));

    var deserializedMockData = JsonConvert.DeserializeObject<List<User>>(mockData);

    var _item1 = deserializedMockData[0];

    var itemList = await categoryRepository.GetAllAsync();
    var _itemCount = itemList.Where(i => i.FirstName.StartsWith(_item1.FirstName)).Count();

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
