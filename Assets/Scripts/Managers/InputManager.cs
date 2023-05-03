using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        ServiceLocator.InputManager = this;

        _playerInput = new PlayerInput();

        _playerInput.Player.Interact.performed += OnInteract;
        _playerInput.Player.Drop.performed += OnDrop;
        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.Player.Acceleration.performed += OnAccelerationStart;
        _playerInput.Player.Acceleration.canceled += OnAccelerationEnd;
        _playerInput.UI.Pause.performed += OnPause;
        
        EventBus.StartLevelEvent += OnStartLevel;
        EventBus.EndLevelEvent += OnEndLevel;
        EventBus.PauseEvent += DisablePlayerInput;
        EventBus.UnPauseEvent += EnablePlayerInput;
        EventBus.UIEvents.OnMainMenuWindowShow += OnEndLevel;

        EventBus.PlayerEvents.OnDeath += DisablePlayerInput;
        EventBus.MiniGamesEvents.OnMiniGameStart += DisablePlayerInput;
        EventBus.MiniGamesEvents.OnMiniGameEnd += EnablePlayerInput;
    }

    private void OnDestroy()
    {
        _playerInput.Player.Interact.performed -= OnInteract;
        _playerInput.Player.Drop.performed -= OnDrop;
        _playerInput.Player.Jump.performed -= OnJump;
        _playerInput.Player.Acceleration.performed -= OnAccelerationStart;
        _playerInput.Player.Acceleration.canceled -= OnAccelerationEnd;
        _playerInput.UI.Pause.performed -= OnPause;
        
        EventBus.StartLevelEvent -= OnStartLevel;
        EventBus.EndLevelEvent -= OnEndLevel;
        EventBus.PauseEvent -= DisablePlayerInput;
        EventBus.UnPauseEvent -= EnablePlayerInput;
        EventBus.UIEvents.OnMainMenuWindowShow -= OnEndLevel;

        EventBus.PlayerEvents.OnDeath -= DisablePlayerInput;
        EventBus.MiniGamesEvents.OnMiniGameStart -= DisablePlayerInput;
        EventBus.MiniGamesEvents.OnMiniGameEnd -= EnablePlayerInput;
    }

    private void Update()
    {
        Vector2 inputVector = _playerInput.Player.Move.ReadValue<Vector2>();
        inputVector = new Vector2(Mathf.RoundToInt(inputVector.x), Mathf.RoundToInt(inputVector.y)).normalized;
            
        EventBus.InputEvents.OnInputMoveChange?.Invoke(inputVector);
    }

    private void OnPause(InputAction.CallbackContext obj)
    {
        if (ServiceLocator.GameManager.GetState == GameManager.GameState.PLAY)
        {
            EventBus.PauseEvent?.Invoke();
            EventBus.UIEvents.OnPauseWindowShow?.Invoke();
        }
        else
        {
            EventBus.UnPauseEvent?.Invoke();
            EventBus.UIEvents.OnPauseWindowHide?.Invoke();
        }
    }
    
    private void OnInteract(InputAction.CallbackContext obj)
    {
        EventBus.InputEvents.OnInteract?.Invoke();
    }
    
    private void OnDrop(InputAction.CallbackContext obj)
    {
        EventBus.InputEvents.OnDrop?.Invoke();
    }
    
    private void OnJump(InputAction.CallbackContext obj)
    {
        EventBus.InputEvents.OnJump?.Invoke();
    }
    
    private void OnAccelerationStart(InputAction.CallbackContext obj)
    {
        EventBus.InputEvents.OnAccelerationStart?.Invoke();
    }
    
    private void OnAccelerationEnd(InputAction.CallbackContext obj)
    {
        EventBus.InputEvents.OnAccelerationEnd?.Invoke();
    }
    
    private void OnStartLevel()
    {
        EnablePlayerInput();
        _playerInput.UI.Enable();
    }
    
    private void OnEndLevel()
    {
        DisablePlayerInput();
        _playerInput.UI.Disable();
    }

    private void EnablePlayerInput()
    {
        _playerInput.Player.Enable();
    }

    private void DisablePlayerInput()
    {
        _playerInput.Player.Disable();
    }
}