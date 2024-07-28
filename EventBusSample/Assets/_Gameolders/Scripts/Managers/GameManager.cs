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
        }

        public static void Stop()
        {
            Debug.Log("<color=red>Stop</color>");
        }
    }    
}

