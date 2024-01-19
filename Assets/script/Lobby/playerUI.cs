using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerUi : MonoBehaviour
{
    public TextMeshProUGUI textnamePlayer;
    private Player player;

    public void SetPlayer(Player player) 
    { 
        this.player = player;
        textnamePlayer.text = "Name";
    }
}
