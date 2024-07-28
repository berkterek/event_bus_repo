using System;
using EventBusSample.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace EventBusSample
{
    public class PlayButton : BaseButton
    {
        protected override void HandleOnButtonClicked()
        {
            GameManager.Play();
        }
    }

    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] protected Button _button;

        void OnValidate()
        {
            if (_button == null) _button = GetComponent<Button>();
        }

        void OnEnable()
        {
            _button.onClick.AddListener(HandleOnButtonClicked);
        }

        void OnDisable()
        {
            _button.onClick.RemoveListener(HandleOnButtonClicked);
        }

        protected abstract void HandleOnButtonClicked();
    }
}

