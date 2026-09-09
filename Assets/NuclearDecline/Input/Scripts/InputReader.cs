using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NuclearDecline.Input
{
    public class InputReader : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;

        public event Action<InputAction.CallbackContext> ActionTriggered;

        private void OnEnable()
        {
            foreach (var action in _inputActions)
            {
                action.performed += OnActionTriggered;
            }
        }

        private void OnDisable()
        {
            foreach (var action in _inputActions)
            {
                action.performed -= OnActionTriggered;
            }
        }

        private void OnActionTriggered(InputAction.CallbackContext context)
        {
            if (context.action.name == "Attack")
            {
                Debug.Log("!!!");
            }
        }
    }

}

