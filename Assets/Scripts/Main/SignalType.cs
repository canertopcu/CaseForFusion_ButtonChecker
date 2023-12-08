using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Main
{
    internal enum SignalType
    {
        Waiting,
        StartGame,
        GameModeActivated,
        Interaction,
        SetName,
        SetInteractableState,
        GameInfo, 
    }
}
