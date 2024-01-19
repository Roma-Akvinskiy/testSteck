using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QuickStart;
using Mirror;
public class PlayerButton : NetworkBehaviour
{
    public GameObject panelQuite;
    private bool a;
    private Player _pl;
    // Update is called once per frame

    private void Start()
    {
        _pl=GetComponent<Player>();
    }
    void Update()
    {
        if (!isLocalPlayer) return;
        
        a = a == Input.GetKeyDown(KeyCode.Escape) ? false : true;
        OnEScapeMenu(a);
    }
    private void OnEScapeMenu(bool isActive)
    {
        if (isActive == true)
        {
            Cursor.lockState = CursorLockMode.Confined;
            panelQuite.SetActive(true);
        }
        else if (isActive == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            panelQuite.SetActive(false);
        }

    }

    public void OnStupbutton()
    {
        GameMeneger.Instanse.QuiteGame();
    }
}
