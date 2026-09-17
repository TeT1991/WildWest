using System;
using UnityEngine.InputSystem;

namespace NuclearDecline.Input
{
    public partial class InputReader
    {
        private readonly InputActionAsset _inputActions;

        public event Action<InputAction.CallbackContext> Triggered;

        public InputReader(InputActionAsset inputActions)
        {
            _inputActions = inputActions;
        }

        public void EnableMap(string name)
        {
            foreach (InputActionMap map in _inputActions.actionMaps)
            {
                if (map.name == name)
                {
                    map.Enable();
                    map.actionTriggered += OnTriggered;
                    return;
                }
            }
        }

        public void DisableMap(string name)
        {
            foreach (InputActionMap map in _inputActions.actionMaps)
            {
                if (map.name == name)
                {
                    map.Disable();
                    map.actionTriggered -= OnTriggered;
                    return;
                }
            }
        }

        private void OnTriggered(InputAction.CallbackContext context)
        {
            Triggered?.Invoke(context);
        }
    }
}

