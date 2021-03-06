﻿namespace Mayhem.Core
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
        public static string RoomToJoin;
        public static string RoomPassword;
    }
}
