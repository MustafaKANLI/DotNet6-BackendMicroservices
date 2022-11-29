using Newtonsoft.Json;
using RulesEngine.Interfaces;
using RulesEngine.Models;

namespace UsersService.API.Extensions;

public static class RulesEngineExtension
{
  public static void AddRulesEngineExtension(this IServiceCollection services)
  {
    var workflows = new List<Workflow>();

    var userRules = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Rules", "User_Rules.json"));
    var userWorkFlow = JsonConvert.DeserializeObject<Workflow>(userRules);

    workflows.Add(userWorkFlow);

    services.AddSingleton<IRulesEngine>(_ => new RulesEngine.RulesEngine(workflows.ToArray()));
  }
}
