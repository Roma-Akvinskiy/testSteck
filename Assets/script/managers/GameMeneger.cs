using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

namespace QuickStart
{
    public class GameMeneger : NetworkBehaviour
    {
        #region Singeltone
        private static GameMeneger _instanse;

        public static GameMeneger Instanse
        {
            get
            {
                return _instanse;
            }
        }
        #endregion
        private void Awake()
        {
            _instanse = this;
        }
        #region PanelQuite

        public void QuiteGame()
        {

            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopHost();
            }
            else if (NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopClient();
            }
            else if (NetworkServer.active)
            {
                NetworkManager.singleton.StopServer();
            }
            Cursor.lockState = CursorLockMode.Confined;
        }
        #endregion
        #region Chat
        public Text canvasStatusText;
        public Player_name playerScript;

        [SyncVar(hook = nameof(OnStatusTextChanged))]
        public string statusText;
        void OnStatusTextChanged(string _Old, string _New)
        {
            //called from sync var hook, to update info on screen for all players
            canvasStatusText.text = statusText;
        }
        public void ButtonSendMessage()
        {
            if (playerScript != null)
                playerScript.CmdSendPlayerMessage();
        }

        #endregion

       
    }
}
