using Assets.Scripts.Main;
using Fusion;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Case
{
    public class UIManager : MonoBehaviour
    {
        //public static UIManager Instance;
        public Button StartGameButton;
        public Button InteractionButton;
        
        public TextMeshProUGUI infoText;
        public TextMeshProUGUI playerInfo;

        public TextMeshProUGUI warningText;

        public GameObject PlayArea;
        public GameObject StartingArea;
        private void Awake()
        {
            // Instance = this;

            StartGameButton.interactable = true;
            StartingArea.SetActive(true);
            PlayArea.SetActive(false);
            warningText.text = "Press Start Game to begin";

        }
        private void OnEnable()
        {
            StartGameButton.onClick.AddListener(StartGame);
            InteractionButton.onClick.AddListener(Interaction);

            SignalBus.Subscribe(nameof(SignalType.GameModeActivated), GameActivated);
            SignalBus.Subscribe(nameof(SignalType.Waiting), Waiting);
            SignalBus.Subscribe<string>(nameof(SignalType.GameInfo), SetGameInfo); 
            SignalBus.Subscribe<string>(nameof(SignalType.SetName), SetNameInfo); 
            SignalBus.Subscribe<bool>(nameof(SignalType.SetInteractableState), SetStateOfInteractionButton);

        }

        private void SetStateOfInteractionButton(bool state)
        {
           InteractionButton.interactable = state;
        }

        private void SetGameInfo(string param1)
        {
           infoText.text = param1;
        }
        private void SetNameInfo(string name)
        {
            playerInfo.text = name;
            InteractionButton.interactable = nameof(GameMode.Host).Equals(name);
        }

        private void Waiting()
        {
           warningText.text = "Waiting to enter the game...";
        }

        private void GameActivated()
        { 
            PlayArea.SetActive(true);
            StartingArea.SetActive(false);
        }


        private void Interaction()
        {
            SignalBus.BroadcastSignal(nameof(SignalType.Interaction), InteractionButton);
        }

        private void StartGame()
        {
            warningText.text = "StartGame Pressed";
            StartGameButton.interactable = false;
            SignalBus.BroadcastSignal(nameof(SignalType.StartGame), StartGameButton);
        }

        private void OnDisable()
        {
            StartGameButton.onClick.RemoveListener(StartGame);
            InteractionButton.onClick.RemoveListener(Interaction);

            SignalBus.Unsubscribe(nameof(SignalType.GameModeActivated), GameActivated);
            SignalBus.Unsubscribe(nameof(SignalType.Waiting), Waiting);
            SignalBus.Unsubscribe<string>(nameof(SignalType.GameInfo), SetGameInfo);
            SignalBus.Unsubscribe<string>(nameof(SignalType.SetName), SetNameInfo);
            SignalBus.Unsubscribe<bool>(nameof(SignalType.SetInteractableState), SetStateOfInteractionButton);

        }
    }
}
