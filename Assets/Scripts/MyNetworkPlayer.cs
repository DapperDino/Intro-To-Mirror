using Mirror;
using UnityEngine;
using UnityEngine.AI;

namespace DapperDino.Multiplayer
{
    public class MyNetworkPlayer : NetworkBehaviour
    {
        [Header("References")]
        [SerializeField] private NavMeshAgent agent = null;

        private Camera mainCamera;

        #region Server

        private void CmdMove(Vector3 position)
        {
            if (!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas)) { return; }

            agent.SetDestination(hit.position);
        }

        #endregion

        #region Client

        public override void OnStartAuthority()
        {
            mainCamera = Camera.main;
        }

        [ClientCallback]
        private void Update()
        {
            if (!hasAuthority) { return; }

            if (!Input.GetMouseButtonDown(1)) { return; }

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity)) { return; }

            CmdMove(hit.point);
        }

        #endregion
    }
}

