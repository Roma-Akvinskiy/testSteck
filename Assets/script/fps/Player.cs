using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour
{
    public static Player localPlayer;
    [SyncVar] public string matchID;
    public bool isStart=false;
    private NetworkMatch networkMatch;
    private PlayerMovenent player;
    private void Start()
    {
        player = GetComponent<PlayerMovenent>();
        networkMatch = GetComponent<NetworkMatch>();
        if (isLocalPlayer)
        {
            localPlayer = this;
        }
        else
        {
            MainMenu.instanse.SpawnPlayerUIPrefab(this);
        }
    }

    public void HostGame()
    {
        string ID = MainMenu.GetRandomID();
        CmdHostGame(ID);
    }
    [Command]
    public void CmdHostGame(string ID)
    {
        matchID = ID;
        if(MainMenu.instanse.HostGame(ID, gameObject))
        {
            Debug.Log("Lobby was created success");
            networkMatch.matchId = ID.ToGuid();
            TargetHostGame(true, ID);
        }
        else
        {
            Debug.Log("Lobby was created Warning");
            TargetHostGame(false, ID);
        }
    }

    [TargetRpc]
    void TargetHostGame(bool success, string ID)
    {
        matchID = ID;
        Debug.Log($"ID {matchID}=={ID}");
        MainMenu.instanse.HostSuccess(success, ID);
    }

    public void JoinGame(string inputID)
    {
        CmdJoinGame(inputID);

    }
    [Command]
    public void CmdJoinGame(string ID)
    {
        matchID = ID;
        if (MainMenu.instanse.JoinGame(ID, gameObject))
        {
            Debug.Log("Join success");
            networkMatch.matchId = ID.ToGuid();
            TargetJoinGame(true, ID);
        }
        else
        {
            Debug.Log("Join warning");
            TargetJoinGame(false, ID);
        }
    }

    [TargetRpc]
    void TargetJoinGame(bool success, string ID)
    {
        matchID = ID;
        Debug.Log($"ID {matchID}=={ID}");
        MainMenu.instanse.JoinSuccess(success, ID);
    }

    public void BeginGame()
    {
        CmdBeginGame();

    }
    [Command]
    public void CmdBeginGame()
    {
        MainMenu.instanse.BeginGame(matchID);
            
            Debug.Log("Game Started");
     
    }
    public void StartGame()
    {
        TargetBeginGame();
    }
    [TargetRpc]
    void TargetBeginGame()
    {
        Debug.Log($"ID {matchID}| begin");
        DontDestroyOnLoad(gameObject);
        MainMenu.instanse.inGame = true;
        transform.localScale = new Vector3(1, 1, 1);
        SceneManager.LoadScene("Map_v6", LoadSceneMode.Additive);
        isStart = true;
        player._isdead = true;
    }
}
