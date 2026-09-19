using System;
using UnityEngine.InputSystem;

namespace NuclearDecline.Input
{
    public class InputReader : IDisposable
    {
        private readonly PlayerInput _playerInput;

        public event Action<InputAction.CallbackContext> Triggered;

        public InputReader(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _playerInput.onActionTriggered += OnTriggered;
        }

        private void OnTriggered(InputAction.CallbackContext context)
        {
            Triggered?.Invoke(context);
        }

        public void Dispose()
        {
            _playerInput.onActionTriggered -= OnTriggered;
        }
    }
}

