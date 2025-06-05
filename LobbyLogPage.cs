using GorillaStats;

namespace LobbyLogger2;

public class LobbyLogPage : IGorillaStatsPage
{
    public string PageName => "Lobby Logger 2";

    public string? GetPageText()
    {
        return Main.instance?.text;
    }
}