using UnityEngine;
using UnityEngine.InputSystem;

namespace NuclearDecline.Input
{
    public class InputBootstrap : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;

        private InputReader _inputReader;
        private DeviceSwitcher _deviceSwitcher;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _inputReader = new InputReader(_playerInput);
            _deviceSwitcher = new DeviceSwitcher(_playerInput);
        }

        public void Test(InputAction.CallbackContext context)
        {
            Debug.Log(context.phase);
        }
    }
}

