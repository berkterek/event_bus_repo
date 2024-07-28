using UnityEngine;

namespace EventBusSample.Managers
{
    public class GameManager : MonoBehaviour
    {
        void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }    
}

