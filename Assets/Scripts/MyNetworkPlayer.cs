using Mirror;
using UnityEngine;

namespace DapperDino.Multiplayer
{
    public class MyNetworkPlayer : NetworkBehaviour
    {
        [SyncVar(hook = nameof(ClientHandleDisplayNameUpdated))]
        private string displayName;

        #region Server

        [Command]
        private void CmdDoSomething()
        {
            // Server authority checks here

            displayName = "Random Name";
        }

        #endregion

        #region Client

        [ClientCallback]
        private void Update()
        {
            if (!hasAuthority) { return; }

            if (!Input.GetKeyDown(KeyCode.Space)) { return; }

            CmdDoSomething();
        }

        [Client]
        private void ClientHandleDisplayNameUpdated(string oldDisplayName, string newDisplayName)
        {
            // Update UI
        }

        #endregion
    }
}

