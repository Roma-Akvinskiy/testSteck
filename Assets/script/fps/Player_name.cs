using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using QuickStart;

public class Player_name : NetworkBehaviour
{
    public TextMesh playerNameText;
    
    public GameObject floatingInfo;

    private Material playerMaterialClone;

    [SyncVar(hook = nameof(OnNameChanged))]
    public string playerName;

    [SyncVar(hook = nameof(OnColorChanged))]
    public Color playerColor = Color.white;



    void OnNameChanged(string _Old, string _New)
    {
        playerNameText.text = playerName;
    }

    void OnColorChanged(Color _Old, Color _New)
    {
        playerNameText.color = _New;
    }
    public override void OnStartLocalPlayer()
    {
         GameMeneger.Instanse.playerScript = this;
        
        string name = "Player" + Random.Range(100, 999);
        Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        CmdSetupPlayer(name, color,0);
    }
    
    [Command]
    public void CmdSetupPlayer(string _name, Color _col, int damage)
    { 
        // player info sent to server, then server updates sync vars which handles it on all clients
        playerName = _name;
        playerColor = _col;
        GameMeneger.Instanse.statusText = $"{playerName} joined.";
        
    }

    [Command]
    public void CmdSendPlayerMessage()
    {
        
            GameMeneger.Instanse.statusText = $"{playerName} says hello {Random.Range(10, 99)}";
    }


}
