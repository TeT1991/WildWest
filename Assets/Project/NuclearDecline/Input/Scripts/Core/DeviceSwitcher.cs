using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Windows;

namespace NuclearDecline.Input
{
    public class DeviceSwitcher : IDisposable
    {
        private const string KeyboardAndMouseDeviceName = "KeyboardAndMouse";
        private const string GamepadDeviceName = "Gamepad";

        private readonly PlayerInput _playerInput;
        private readonly IDisposable _buttonPressSubscription;

        private InputDeviceType _currentDevice;

        public event Action<InputDeviceType> InputDeviceChanged;
        public event Action DefaultInputDeviceLost;

        public DeviceSwitcher(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _currentDevice = InputDeviceType.KeyboardAndMouse;

            _buttonPressSubscription = InputSystem.onAnyButtonPress.Call(OnButtonPressed);
            InputSystem.onDeviceChange += OnDeviceChange;
        }

        private void NotifyDeviceChanged()
        {
            if (_currentDevice == InputDeviceType.None)
            {
                DefaultInputDeviceLost?.Invoke();
            }
            else
            {
                InputDeviceChanged?.Invoke(_currentDevice);
            }

            Debug.Log(_currentDevice);
        }

        private void SetCurrentDevice(InputDeviceType type)
        {
            _currentDevice = type;
        }

        private InputDeviceType GetInputDeviceType(InputDevice device)
        {
            if (device is Keyboard || device is Mouse) { return InputDeviceType.KeyboardAndMouse; }
            if (device is Gamepad) { return InputDeviceType.Gamepad; }

            return InputDeviceType.None;
        }

        public void Dispose()
        {
            InputSystem.onDeviceChange -= OnDeviceChange;
            _buttonPressSubscription.Dispose();
        }

        private void OnButtonPressed(InputControl control)
        {
            InputDeviceType type = GetInputDeviceType(control.device);

            if (type == InputDeviceType.None || type == _currentDevice)
            {
                return;
            }

            if (type == InputDeviceType.KeyboardAndMouse && (Keyboard.current == null || Mouse.current == null))
            {
                return;
            }

            SetCurrentDevice(type);
            NotifyDeviceChanged();
        }

        private void OnDeviceChange(InputDevice device, InputDeviceChange change)
        {
            InputDeviceType type = GetInputDeviceType(device);

            if (change == InputDeviceChange.Removed)
            {
                switch (type)
                {
                    case InputDeviceType.Gamepad:
                        SetCurrentDevice(InputDeviceType.KeyboardAndMouse);
                        break;

                    case InputDeviceType.KeyboardAndMouse:
                        SetCurrentDevice(InputDeviceType.None);
                        break;

                    default:
                        return;
                }

                NotifyDeviceChanged();
            }
        }
    }
}

