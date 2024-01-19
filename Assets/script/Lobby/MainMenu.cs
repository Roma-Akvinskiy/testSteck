using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;
using System.Text;
using Mirror;
using TMPro;
using Mirror.Examples.Basic;

[System.Serializable]
public class Match : NetworkBehaviour
{
    public string ID;
    public readonly List<GameObject> players = new List<GameObject>();

    public Match(string Id, GameObject player) 
    {
        this.ID = Id;
        players.Add(player);
    }
}

public class MainMenu : NetworkBehaviour
{
    public static MainMenu instanse;
    public readonly SyncList<Match> matches = new SyncList<Match>();
    public readonly SyncList<string> matchIds = new SyncList<string>();
    public TMP_InputField JoinInput;
    public Button HostButton, JoinButton;
    public Canvas lobbyCanvas;

    public Transform UIplayerParent;
    public GameObject UIPlayerPrefab;
    public TextMeshProUGUI IDText;
    public Button BeginGameButton;
    public GameObject TurnManager;
    public bool inGame;
    public GameObject[] lobbufabs;
    private void Start()
    {
        instanse=this;
    }

    private void Update()
    {
        if (!inGame)
        {
            Player[] players = FindObjectsOfType<Player>();
            for (int i = 0; i < players.Length; i++)
            {
                players[i].gameObject.transform.localScale = Vector3.zero;
            }
            
        }
        else
        {
            for (int i = 0; i < lobbufabs.Length; i++)
            {
                lobbufabs[i].SetActive(false);
            }
        }
    }

    public void Host()
    {
        JoinInput.interactable = false;
        HostButton.interactable = false;
        JoinButton.interactable = false;

        Player.localPlayer.HostGame();
    }

    public void HostSuccess(bool success, string matchID)
    {
        if (success)
        {
            lobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(Player.localPlayer);
            IDText.text=matchID;
            BeginGameButton.interactable = true;
        }
        else
        {
            JoinInput.interactable = true;
            HostButton.interactable = true;
            JoinButton.interactable = true;
        }
    }
    public void Join()
    {
        JoinInput.interactable = false;
        HostButton.interactable = false;
        JoinButton.interactable = false;

        Player.localPlayer.JoinGame(JoinInput.text.ToUpper());
    }

    public void JoinSuccess(bool success, string matchID)
    {
        if (success)
        {
            lobbyCanvas.enabled = true;

            SpawnPlayerUIPrefab(Player.localPlayer);
            IDText.text = matchID;
            BeginGameButton.interactable = false;
        }
        else
        {
            JoinInput.interactable = true;
            HostButton.interactable = true;
            JoinButton.interactable = true;
        }
    }


    public bool HostGame(string matchID, GameObject player)
    {
        if (!matchIds.Contains(matchID))
        {
            matchIds.Add(matchID);
            matches.Add(new Match(matchID, player));
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool JoinGame(string matchID, GameObject player)
    {
        if (matchIds.Contains(matchID))
        {
            for (int i = 0; i < matches.Count; i++)
            {
                if (matches[i].ID == matchID)
                {
                    matches[i].players.Add(player);
                    break;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string GetRandomID()
    {
        string ID = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            int random = UnityEngine.Random.Range(0,36);
            if (random < 26)
            {
                ID += (char)(random + 65);

            }
            else
            {
                ID += (random - 65).ToString();
            }
        }
        return ID;
    }


    public void SpawnPlayerUIPrefab(Player localPlayer)
    {
        GameObject newIpPlayer = Instantiate(UIPlayerPrefab, UIplayerParent);
        newIpPlayer.GetComponent<playerUi>().SetPlayer(localPlayer);
    }

    public void StartGame()
    {
        Player.localPlayer.BeginGame();
    }

    public void BeginGame(string matchID)
    {
        GameObject newTurnManager = Instantiate(TurnManager);
        NetworkServer.Spawn(newTurnManager);
        newTurnManager.GetComponent<NetworkMatch>().matchId = matchID.ToGuid();
        TurnManager turnManager = newTurnManager.GetComponent<TurnManager>();
        for (int i = 0; i < matches.Count; i++)
        {
            if (matches[i].ID == matchID)
            {
                foreach (var player in matches[i].players)
                {
                    Player playerOne = player.GetComponent<Player>();
                    turnManager.AddOlayer(playerOne);
                    playerOne.StartGame();
                }
                break;
            }

        }
    }
}

public static class MatchExtension
{
    public static Guid ToGuid(this string id)
    {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputButes = Encoding.Default.GetBytes(id);
        byte[] hasButes = provider.ComputeHash(inputButes);
        return new Guid(hasButes);
    }
}
