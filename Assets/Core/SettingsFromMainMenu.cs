using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mayhem.Core
{
    public enum GameMode
    {
        RandomGame,
        FriendsGame,
        PrivateGame
    }
    public static class SettingsFromMainMenu
    {
        public static string PlayerNickName;
        public static GameMode SpecifiedGameMode;
    }
}
