// We use a custom NetworkManager that also takes care of login, character
// selection, character creation and more.
//
// We don't use the playerPrefab, instead all available player classes should be
// dragged into the spawnable objects property.
//
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Mirror;

namespace uSurvival
{
    // we need a clearly defined state to know if we are offline/in world/in lobby
    // otherwise UICharacterSelection etc. never know 100% if they should be visible
    // or not.
    public enum NetworkState {Offline, Handshake, Lobby, World}


    public class NetworkManagerSurvival : NetworkManager
    {
        // current network manager state on client
        public NetworkState state = NetworkState.Offline;

        // <conn, account> dict for the lobby
        // (people that are still creating or selecting characters)
        public Dictionary<NetworkConnection, string> lobby = new Dictionary<NetworkConnection, string>();



        // we may want to add another game server if the first one gets too crowded.
        // the server list allows people to choose a server.
        //
        // note: we use one port for all servers, so that a headless server knows
        // which port to bind to. otherwise it would have to know which one to
        // choose from the list, which is far too complicated. one port for all
        // servers will do just fine for an Indie game.
        [Serializable]
        public class ServerInfo
        {
            public string name;
            public string ip;
        }
        public List<ServerInfo> serverList = new List<ServerInfo>()
        {
            new ServerInfo{name="Local", ip="localhost"}
        };

        [Header("Logout")]
        [Tooltip("Players shouldn't be able to log out instantly to flee combat. There should be a delay.")]
        public float combatLogoutDelay = 5;

        [Header("Database")]
        public int characterLimit = 4;
        public int characterNameMaxLength = 16;
        public float saveInterval = 60f; // in seconds

        [Header("Debug")]
        public bool showDebugGUI = true;

        // cache player classes in Awake
        [HideInInspector] public List<GameObject> playerClasses = new List<GameObject>();

        // store characters available message on client so that UI can access it
        [HideInInspector] public CharactersAvailableMsg charactersAvailableMsg;
    }
}