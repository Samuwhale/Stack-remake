using System;
using System.Collections;
using System.Collections.Generic;
using Sambono;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


    public class SettingsUI : MonoBehaviour
    {
        private IHasSettings _previousObject;
        [SerializeField] private GameObject _visibilityObject;

        [SerializeField] private GameObject _rebindOverlayUI;

        [SerializeField] private Button _masterButton;
        private TextMeshProUGUI _masterButtonText;
        [SerializeField] private Button _sfxButton;
        private TextMeshProUGUI _sfxButtonText;
        [SerializeField] private Button _musicButton;
        private TextMeshProUGUI _musicButtonText;
        [SerializeField] private Button _closeButton;

        private TextMeshProUGUI _moveUpButtonText;
        [SerializeField] private Button _moveUpButton;

        private TextMeshProUGUI _moveDownButtonText;
        [SerializeField] private Button _moveDownButton;

        private TextMeshProUGUI _moveRightButtonText;
        [SerializeField] private Button _moveRightButton;

        private TextMeshProUGUI _moveLeftButtonText;
        [SerializeField] private Button _moveLeftButton;

        private TextMeshProUGUI _sprintButtonText;
        [SerializeField] private Button _sprintButton;
        
        private TextMeshProUGUI _jumpButtonText;
        [SerializeField] private Button _jumpButton;
        
        private TextMeshProUGUI _interactButtonText;
        [SerializeField] private Button _interactButton;

        private TextMeshProUGUI _interactAltButtonText;
        [SerializeField] private Button _interactAltButton;

        private TextMeshProUGUI _pauseButtonText;
        [SerializeField] private Button _pauseButton;
    
        private TextMeshProUGUI _gamepadInteractButtonText;
        [SerializeField] private Button _gamepadInteractButton;

        private TextMeshProUGUI _gamepadInteractAltButtonText;
        [SerializeField] private Button _gamepadInteractAltButton;

        private TextMeshProUGUI _gamepadPauseButtonText;
        [SerializeField] private Button _gamepadPauseButton;
        
        private TextMeshProUGUI _gamepadSprintButtonText;
        [SerializeField] private Button _gamepadSprintButton;
        
        private TextMeshProUGUI _gamepadJumpButtonText;
        [SerializeField] private Button _gamepadJumpButton;

        [SerializeField] private InputReader _inputReader;
        private void Awake() {
            _masterButton.onClick.AddListener(OnMasterButtonPressed);
            _sfxButton.onClick.AddListener(OnSfxButtonPressed);
            _musicButton.onClick.AddListener(OnMusicButtonPressed);
            _closeButton.onClick.AddListener(OnCloseButtonPressed);
            _moveUpButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.MoveUp));
            _moveDownButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.MoveDown));
            _moveLeftButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.MoveLeft));
            _moveRightButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.MoveRight));
            _interactButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.Interact));
            _interactAltButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.InteractAlt));
            _pauseButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.Pause));
            _jumpButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.Jump));
            _sprintButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.Sprint));
            _gamepadInteractButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.GamepadInteract));
            _gamepadInteractAltButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.GamepadInteractAlt));
            _gamepadPauseButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.GamepadPause));
            _gamepadJumpButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.GamepadJump));
            _gamepadSprintButton.onClick.AddListener(() => RebindBinding(InputReader.Binding.GamepadSprint));

            _masterButtonText = _masterButton.GetComponentInChildren<TextMeshProUGUI>();
            _sfxButtonText = _sfxButton.GetComponentInChildren<TextMeshProUGUI>();
            _musicButtonText = _musicButton.GetComponentInChildren<TextMeshProUGUI>();

            _moveUpButtonText = _moveUpButton.GetComponentInChildren<TextMeshProUGUI>();
            _moveDownButtonText = _moveDownButton.GetComponentInChildren<TextMeshProUGUI>();
            _moveLeftButtonText = _moveLeftButton.GetComponentInChildren<TextMeshProUGUI>();
            _moveRightButtonText = _moveRightButton.GetComponentInChildren<TextMeshProUGUI>();
            _interactButtonText = _interactButton.GetComponentInChildren<TextMeshProUGUI>();
            _interactAltButtonText = _interactAltButton.GetComponentInChildren<TextMeshProUGUI>();
            _jumpButtonText = _sprintButton.GetComponentInChildren<TextMeshProUGUI>();
            _sprintButtonText = _jumpButton.GetComponentInChildren<TextMeshProUGUI>();
            _pauseButtonText = _pauseButton.GetComponentInChildren<TextMeshProUGUI>();
            _gamepadInteractButtonText = _gamepadInteractButton.GetComponentInChildren<TextMeshProUGUI>();
            _gamepadInteractAltButtonText = _gamepadInteractAltButton.GetComponentInChildren<TextMeshProUGUI>();
            _gamepadPauseButtonText = _gamepadPauseButton.GetComponentInChildren<TextMeshProUGUI>();
            _gamepadJumpButtonText = _gamepadSprintButton.GetComponentInChildren<TextMeshProUGUI>();
            _gamepadSprintButtonText = _gamepadJumpButton.GetComponentInChildren<TextMeshProUGUI>();
            
            
        }
        
    

        private void GameManager_OnGameResumed() {
            Hide();
        }

        private void OnCloseButtonPressed() {
            _previousObject.Show();
            Hide();
        }


        private void Start()
        {
            MS.Main.GameManager.OnGameResumed += GameManager_OnGameResumed;
            _rebindOverlayUI.SetActive(false);
            Hide();
        }

        private void OnMasterButtonPressed() {
            MS.Main.AudioManager.ChangeMasterVolume();
            UpdateVisuals();
        }

        private void OnSfxButtonPressed() {
            MS.Main.AudioManager.ChangeSfxVolume();
            UpdateVisuals();
        }

        private void OnMusicButtonPressed()
        {
            MS.Main.AudioManager.ChangeMusicVolume();
            UpdateVisuals();
        }

        private void UpdateVisuals() {
            _masterButtonText.SetText($"Master: {AudioManager.ConvertDbToPercentage(MS.Main.AudioManager.MasterVolume):F1}%");
            _musicButtonText.SetText($"Music: {AudioManager.ConvertDbToPercentage(MS.Main.AudioManager.MusicVolume):F1}%");
            _sfxButtonText.SetText($"Sfx: {AudioManager.ConvertDbToPercentage(MS.Main.AudioManager.SfxVolume):F1}%");
        
            _moveUpButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.MoveUp));
            _moveDownButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.MoveDown));
            _moveLeftButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.MoveLeft));
            _moveRightButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.MoveRight));
            _jumpButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.Jump));
            _gamepadJumpButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.GamepadJump));
            _sprintButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.Sprint));
            _gamepadSprintButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.GamepadSprint));
            _interactButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.Interact));
            _interactAltButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.InteractAlt));
            _pauseButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.Pause));
            _gamepadInteractButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.GamepadInteract));
            _gamepadInteractAltButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.GamepadInteractAlt));
            _gamepadPauseButtonText.SetText(_inputReader.GetBindingKeyString(InputReader.Binding.GamepadPause));
        }

        void RebindBinding(InputReader.Binding binding) {
            _rebindOverlayUI.SetActive(true);
            _inputReader.RebindBinding(binding, OnRebindComplete);

        }

        void OnRebindComplete() {
            UpdateVisuals();
            _rebindOverlayUI.SetActive(false);
            _sfxButton.Select();
        }

        public void SetPreviousObject(IHasSettings previousObject)
        {
            _previousObject = previousObject;
        }
        public void Show() {
            _visibilityObject.SetActive(true);
            UpdateVisuals();
            _sfxButton.Select();
        }

        public void Hide() {
            _visibilityObject.SetActive(false);
        }
    }
