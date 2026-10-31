using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Web;

namespace BotPlacementSystemServer;

public record ModMetadata : IModMetadata, IModBlazorMetadata
{
    public string ModGuid { get; init; } = MyModInfo.MOD_GUID;
    public string Name { get; init; } = MyModInfo.MOD_NAME;
    public string Author { get; init; } = MyModInfo.MOD_AUTHOR;
    public List<string>? Contributors { get; init; }
    public SemanticVersioning.Version Version { get; init; } = new(MyModInfo.MOD_VERSION);
    public SemanticVersioning.Range SptVersion { get; init; } = new(MyModInfo.SERVER_SPT_VERSION);
    public bool HasPrepatcher { get; init; }
    public List<string>? Incompatibilities { get; init; }
    public Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public string? Url { get; init; }
    public string License { get; init; } = "BY-NC-ND 4.0";
    public string? WWWRootUrl { get; init; }
    public string? HomePage { get; init; } = "/botplacementsystem";
    public string? HomePageDescription { get; init; } = "Configure ABPS";
}