using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Sambono
{
    [CreateAssetMenu(menuName = "InputReader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions, GameInput.IUIActions
    {
        private const string PLAYERPREFS_BINDINGS = "Input_Bindings";

        public enum Binding
        {
            MoveUp,
            MoveDown,
            MoveLeft,
            MoveRight,
            Sprint,
            Jump,
            Interact,
            InteractAlt,
            Pause,
            GamepadInteract,
            GamepadInteractAlt,
            GamepadPause,
            GamepadJump,
            GamepadSprint,
        }

        private GameInput _gameInput;

        private void OnEnable()
        {
            Debug.Log($"{this}.OnEnable() was called.");
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYERPREFS_BINDINGS));
                _gameInput.Gameplay.SetCallbacks(this);
                _gameInput.UI.SetCallbacks(this);
                SwitchToGameplay();
            }
        }


        // EventHandler also would send the sender, which CAN be useful, in this case not needed though.
        public event Action<Vector2> MoveEvent;
        public event Action<Vector2> LookEvent;
        public event Action<Vector2> MoveEventNormalized;
        public event Action<Vector2> LookEventNormalized;
        public event Action JumpEvent;
        public event Action JumpReleasedEvent;
        public event Action SprintEvent;
        public event Action SprintReleasedEvent;
        public event Action InteractEvent;
        public event Action InteractReleasedEvent;
        public event Action InteractAltEvent;
        public event Action InteractAltReleasedEvent;
        public event Action PauseEvent;
        public event Action UnpauseEvent;

        public bool IsUsingGamepad()
        {
            return Gamepad.all.Count > 0;
        }


        public void SwitchToGameplay()
        {
            _gameInput.Gameplay.Enable();
            _gameInput.UI.Disable();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void SwitchToUI()
        {
            _gameInput.Gameplay.Disable();
            _gameInput.UI.Enable();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        public void OnLook(InputAction.CallbackContext context)
        {
            LookEvent?.Invoke(context.ReadValue<Vector2>());
            LookEventNormalized?.Invoke(context.ReadValue<Vector2>().normalized);
        }

        public Vector2 LookVectorRaw => _gameInput.Gameplay.Look.ReadValue<Vector2>();
        public Vector2 MoveVectorRaw => _gameInput.Gameplay.Move.ReadValue<Vector2>();
        public Vector2 LookVectorNormalized => _gameInput.Gameplay.Look.ReadValue<Vector2>().normalized;
        public Vector2 MoveVectorNormalized => _gameInput.Gameplay.Move.ReadValue<Vector2>().normalized;

        public bool IsSprintPressed { get; private set; }

        public bool IsJumpPressed { get; private set; }
        public bool IsInteractPressed { get; private set; }
        public bool IsInteractAltPressed { get; private set; }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                MoveEvent?.Invoke(context.ReadValue<Vector2>());
                MoveEventNormalized?.Invoke(context.ReadValue<Vector2>().normalized);
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                MoveEvent?.Invoke(Vector2.zero);
                MoveEventNormalized?.Invoke(Vector2.zero);
            }
        }


        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                SprintEvent?.Invoke();
                IsSprintPressed = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                SprintReleasedEvent?.Invoke();
                IsSprintPressed = false;
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                JumpEvent?.Invoke();
                IsJumpPressed = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                JumpReleasedEvent?.Invoke();
                IsJumpPressed = false;
            }
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                InteractEvent?.Invoke();
                IsInteractPressed = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                InteractReleasedEvent?.Invoke();
                IsInteractPressed = false;
            }
        }

        public void OnInteractAlt(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                InteractAltEvent?.Invoke();
                IsInteractPressed = true;
            }
            else if (context.phase == InputActionPhase.Canceled)
            {
                InteractAltReleasedEvent?.Invoke();
                IsInteractPressed = false;
            }
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                PauseEvent?.Invoke();
                // SwitchToUI();
            }
        }

        public void OnUnpause(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed)
            {
                UnpauseEvent?.Invoke();
                // SwitchToGameplay();
            }
        }

        public void RebindBinding(Binding binding, Action onInputRebound)
        {
            _gameInput.Disable();
            InputAction inputAction;
            int bindingIndex;

            switch (binding)
            {
                case Binding.MoveUp:
                    inputAction = _gameInput.Gameplay.Move;
                    bindingIndex = 1;
                    break;
                case Binding.MoveDown:
                    inputAction = _gameInput.Gameplay.Move;
                    bindingIndex = 2;
                    break;
                case Binding.MoveLeft:
                    inputAction = _gameInput.Gameplay.Move;
                    bindingIndex = 3;
                    break;
                case Binding.MoveRight:
                    inputAction = _gameInput.Gameplay.Move;
                    bindingIndex = 4;
                    break;
                case Binding.Interact:
                    inputAction = _gameInput.Gameplay.Interact;
                    bindingIndex = 0;
                    break;
                case Binding.InteractAlt:
                    inputAction = _gameInput.Gameplay.InteractAlt;
                    bindingIndex = 0;
                    break;
                case Binding.Jump:
                    inputAction = _gameInput.Gameplay.Jump;
                    bindingIndex = 0;
                    break;
                case Binding.Sprint:
                    inputAction = _gameInput.Gameplay.Sprint;
                    bindingIndex = 0;
                    break;
                case Binding.Pause:
                    inputAction = _gameInput.Gameplay.Pause;
                    bindingIndex = 0;
                    break;
                case Binding.GamepadInteract:
                    inputAction = _gameInput.Gameplay.Interact;
                    bindingIndex = 1;
                    break;
                case Binding.GamepadInteractAlt:
                    inputAction = _gameInput.Gameplay.InteractAlt;
                    bindingIndex = 1;
                    break;
                case Binding.GamepadPause:
                    inputAction = _gameInput.Gameplay.Pause;
                    bindingIndex = 1;
                    break;
                case Binding.GamepadJump:
                    inputAction = _gameInput.Gameplay.Jump;
                    bindingIndex = 1;
                    break;
                case Binding.GamepadSprint:
                    inputAction = _gameInput.Gameplay.Sprint;
                    bindingIndex = 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(binding), binding, null);
            }

            inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
            {
                _gameInput.Enable();
                onInputRebound?.Invoke();
                callback.Dispose();
                PlayerPrefs.SetString(PLAYERPREFS_BINDINGS, _gameInput.SaveBindingOverridesAsJson());
            }).Start();
        }

        public string GetBindingKeyString(Binding binding)
        {
            string outputString = "?";
            switch (binding)
            {
                case Binding.MoveUp:
                    outputString = _gameInput.Gameplay.Move.bindings[1].ToDisplayString();
                    break;
                case Binding.MoveDown:
                    outputString = _gameInput.Gameplay.Move.bindings[2].ToDisplayString();
                    break;
                case Binding.MoveLeft:
                    outputString = _gameInput.Gameplay.Move.bindings[3].ToDisplayString();
                    break;
                case Binding.MoveRight:
                    outputString = _gameInput.Gameplay.Move.bindings[4].ToDisplayString();
                    break;
                case Binding.Interact:
                    outputString = _gameInput.Gameplay.Interact.bindings[0].ToDisplayString();
                    break;
                case Binding.Jump:
                    outputString = _gameInput.Gameplay.Jump.bindings[0].ToDisplayString();
                    break;
                case Binding.Sprint:
                    outputString = _gameInput.Gameplay.Sprint.bindings[0].ToDisplayString();
                    break;
                case Binding.InteractAlt:
                    outputString = _gameInput.Gameplay.InteractAlt.bindings[0].ToDisplayString();
                    break;
                case Binding.Pause:
                    outputString = _gameInput.Gameplay.Pause.bindings[0].ToDisplayString();
                    break;
                case Binding.GamepadInteract:
                    outputString = _gameInput.Gameplay.Interact.bindings[1].ToDisplayString();
                    break;
                case Binding.GamepadInteractAlt:
                    outputString = _gameInput.Gameplay.InteractAlt.bindings[1].ToDisplayString();
                    break;
                case Binding.GamepadPause:
                    outputString = _gameInput.Gameplay.Pause.bindings[1].ToDisplayString();
                    break;
                case Binding.GamepadJump:
                    outputString = _gameInput.Gameplay.Jump.bindings[1].ToDisplayString();
                    break;
                case Binding.GamepadSprint:
                    outputString = _gameInput.Gameplay.Sprint.bindings[1].ToDisplayString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(binding), binding, null);
            }

            return outputString;
        }
    }
}