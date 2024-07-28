using UnityEngine;
using UnityEngine.InputSystem;

namespace EventBusSample.Inputs 
{
    public class NewInputReader
    {
        readonly GameInputSystem _input;
        
        public Vector2 Direction { get; private set; }
        
        public NewInputReader()
        {
            _input = new GameInputSystem();

            _input.Player.Move.performed += HandleOnMove;
            _input.Player.Move.canceled += HandleOnMove;
            
            _input.Enable();
        }

        ~NewInputReader()
        {
            _input.Player.Move.performed -= HandleOnMove;
            _input.Player.Move.canceled -= HandleOnMove;
            
            _input.Disable();
        }
        
        void HandleOnMove(InputAction.CallbackContext context)
        {
            Direction = context.ReadValue<Vector2>();
        }
    }
}