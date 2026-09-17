using UnityEngine;
using UnityEngine.InputSystem;

namespace NuclearDecline.Input
{
    public class InputBootstrap : MonoBehaviour
    {
        [SerializeField] private InputActionAsset _inputActions;

        private InputReader _inputReader;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _inputReader = new InputReader(_inputActions);

            foreach (InputActionMap map in _inputActions.actionMaps)
            {
                _inputReader.EnableMap(map.name);
            }

            _inputReader.Triggered += Test;
        }

        public void Test(InputAction.CallbackContext context)
        {
            Debug.Log(context.phase);
        }
    }
}

