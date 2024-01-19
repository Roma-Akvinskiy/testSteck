using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using QuickStart;
public class PayerHealth : NetworkBehaviour
{
    #region Health
    [Header("Player Health")]
    [SyncVar(hook = nameof(OnValueChanged))]
    public int _haelth = 100;
    public TextMeshProUGUI textLife;
    public PlayerMovenent movement;
    public GameObject canvas;
    private Player _pl;
    private void Start()
    {
        _pl = GetComponent<Player>();
        if (!isLocalPlayer)
        {
            canvas.SetActive(false);
        }
        
        textLife.text = string.Format("{0}/100", _haelth);
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        
        if (_haelth <= 0 || Input.GetKeyDown(KeyCode.K))
        {
            onRespawn();
        }
    }
    [Server]
    public void Damage(int damage)
    {
        _haelth -= damage;
        if (_haelth <= 0)
        {
            _haelth = 100;
            movement._isdead = true;
        }
    }

    void OnValueChanged(int OldValue, int Newvalue)
    {

        textLife.text = string.Format("{0}/100", Newvalue);


    }

    [Command]
    public void onRespawn()
    {
        movement._isdead = true;
    }
    #endregion

    




}
