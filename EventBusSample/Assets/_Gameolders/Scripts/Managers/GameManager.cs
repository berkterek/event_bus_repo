using EventBusSample.EventBuses;
using UnityEngine;

namespace EventBusSample.Managers
{
    public class GameManager : MonoBehaviour
    {
        void Awake()
        {
            Application.targetFrameRate = 60;
        }

        public static void Play()
        {
            Debug.Log("<color=green>Play</color>");
            EventBus<PlayStateEvent>.Raise(new PlayStateEvent());
        }

        public static void Stop()
        {
            Debug.Log("<color=red>Stop</color>");
            EventBus<StopStateEvent>.Raise(new StopStateEvent());
        }
    }    
}

