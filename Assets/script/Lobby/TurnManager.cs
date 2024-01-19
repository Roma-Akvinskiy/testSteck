using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TurnManager : MonoBehaviour
{
    private List<Player> players= new List<Player>();

    public void AddOlayer(Player player)
    {
        players.Add(player);
    }
}
