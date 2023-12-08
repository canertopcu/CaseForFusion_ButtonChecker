using Case.Main;
using Case;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Case
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        [SerializeField] private GameState _currentGameState = GameState.GameNotSterted;
        public GameState CurrentGameState
        {
            get
            {
                return _currentGameState;
            }
            set
            {
                if (value != _currentGameState)
                {
                    _currentGameState = value;
                    switch (_currentGameState)
                    {
                        case GameState.GameNotSterted:

                            break;
                        case GameState.TryingToStart:
                            //SignalBus.BroadcastSignal(nameof(SignalType.Waiting));
                            break;
                        case GameState.GameStarted:
                            //SignalBus.BroadcastSignal(nameof(SignalType.GameModeActivated));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
