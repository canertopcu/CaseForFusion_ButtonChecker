using Case.Main;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

namespace Case.Network
{
    public class NetworkPlayer : NetworkBehaviour
    {
        private Button button;
        NetworkObject _networkObject;
        NetworkObject networkObject
        {
            get
            {
                if (_networkObject == null)
                {
                    _networkObject = GetComponent<NetworkObject>();
                }
                return _networkObject;
            }
        }

        private void OnEnable()
        {
            SignalBus.Subscribe<Button>(nameof(SignalType.Interaction), OnInteractionButtonPressed);

        }
        private void OnDisable()
        {
            SignalBus.Unsubscribe<Button>(nameof(SignalType.Interaction), OnInteractionButtonPressed);
        }
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void Spawned()
        {
            Debug.Log("Player spawned : " + networkObject.Id);
            if (Object.HasInputAuthority)
            {
                if ((Runner.GameMode == GameMode.Client))
                {
                    SignalBus.BroadcastSignal(nameof(SignalType.SetName), $"{Runner.GameMode}{Runner.LocalPlayer.PlayerId}");
                }
                else
                {
                    SignalBus.BroadcastSignal(nameof(SignalType.SetName), $"{Runner.GameMode}");
                }
            }
        }

        private void OnInteractionButtonPressed(Button button)
        {
            Debug.Log("Button Pressed");
            this.button = button;

            if (Object.HasInputAuthority)
            {
                RPC_SendMessage(Runner.GameMode.ToString(), Runner.LocalPlayer.PlayerId.ToString());
            }
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void RPC_SendMessage(string gameMode, string playerId, RpcInfo info = default)
        {
            if (info.IsInvokeLocal)
            {
                button.interactable = false;
            }
            else
            {
                switch (gameMode)
                {
                    case nameof(GameMode.Host):
                        SignalBus.BroadcastSignal(nameof(SignalType.SetInteractableState), true);
                        break;
                    case nameof(GameMode.Client):
                        ResetPlayer();
                        break;
                    default:
                        break;
                }
            }
            SignalBus.BroadcastSignal(nameof(SignalType.GameInfo), $"{gameMode}{playerId} pushed the button");

        }

        public void ResetPlayer()
        {
            if (Runner.GameMode == GameMode.Host)
            {
                SignalBus.BroadcastSignal(nameof(SignalType.SetInteractableState), true);
            }
            else
            {
                SignalBus.BroadcastSignal(nameof(SignalType.SetInteractableState), false);
            }
        }


    }
}
