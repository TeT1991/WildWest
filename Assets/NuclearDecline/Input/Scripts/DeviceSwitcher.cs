using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NuclearDecline.Input
{
    public class DeviceSwitcher : IDisposable
    {
        private const string KeyboardAndMouseDeviceName = "KeyboardAndMouse";
        private const string GamepadDeviceName = "Gamepad";

        private readonly PlayerInput _playerInput;

        public event Action<InputDeviceType> InputDeviceChanged;

        public DeviceSwitcher(PlayerInput playerInput)
        {
            _playerInput = playerInput;

            _playerInput.onDeviceLost += OnDeviceLost;
            _playerInput.onControlsChanged += OnControllChanged;
        }

        private void NotifyControllChanged(string deviceName)
        {
            if (TryGetInputDeviceTypeBySchemeName(deviceName, out InputDeviceType type))
            {
                InputDeviceChanged?.Invoke(type);
                Debug.Log(type);
            }
        }

        private bool TryGetInputDeviceTypeBySchemeName(string schemeName, out InputDeviceType deviceType)
        {
            deviceType = InputDeviceType.None;

            switch (schemeName)
            {
                case KeyboardAndMouseDeviceName:
                    deviceType = InputDeviceType.KeyboardAndMouse;
                    break;

                case GamepadDeviceName:
                    deviceType = InputDeviceType.Gamepad;
                    break;
            }

            if (deviceType == InputDeviceType.None)
            {
                return false;
            }

            return true;
        }

        private void OnDeviceLost(PlayerInput playerInput)
        {
            _playerInput.user.UnpairDevices();
            _playerInput.user.ActivateControlScheme(_playerInput.defaultControlScheme).AndPairRemainingDevices();
        }

        private void OnControllChanged(PlayerInput playerInput)
        {
            string deviceName = playerInput.currentControlScheme;
            NotifyControllChanged(deviceName);
        }

        public void Dispose()
        {
            _playerInput.onDeviceLost -= OnDeviceLost;
            _playerInput.onControlsChanged -= OnControllChanged;
        }
    }
}

