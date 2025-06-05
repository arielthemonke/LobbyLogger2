using System;
using System.IO;
using System.Reflection;
using BepInEx;
using GorillaStats;
using Photon.Pun;
using static LobbyLogger2.Info;
using UnityEngine;

namespace LobbyLogger2
{
    [BepInPlugin(LobbyLogger2.Info.Guid, Name, LobbyLogger2.Info.Version)]
    public class Main : BaseUnityPlugin
    {
        private string? currentLobby;
        public string? lastLobby;
        private string? txtpath;
        public static Main? instance;
        public string? text;

        private void Awake()
        {
            instance = this;
            GorillaTagger.OnPlayerSpawned(Init);
        }

        void Init()
        {
            GorillaStatsPageManager.RegisterPage(new LobbyLogPage());
            NetworkSystem.Instance.OnMultiplayerStarted += OnJoin;

            var assembly = Assembly.GetExecutingAssembly().Location;
            var assemblyPath = Path.GetDirectoryName(assembly);
            txtpath = Path.Combine(assemblyPath, "lobbylog.txt");
            
            text = "Lobby Logger 2";
        }

        void OnJoin()
        {
            if (currentLobby != null)
            {
                lastLobby = currentLobby;
                text = $"Lobby Logger:\n{lastLobby}";
            }
            else
            {
                text = "Lobby Logger:\nno last lobby yet";
            }
            
            try
            {
                currentLobby = PhotonNetwork.CurrentRoom.Name;
                File.AppendAllText(txtpath, currentLobby + Environment.NewLine);
                Debug.Log($"joined: {currentLobby}");
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }
    }
}