using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;

    private void Awake()
    {
        ServiceLocator.InputManager = this;

        _playerInput = new PlayerInput();

        _playerInput.Player.Jump.performed += OnJump;
        _playerInput.UI.Pause.performed += OnPause;
        
        EventBus.StartLevelEvent += OnStartLevel;
        EventBus.EndLevelEvent += OnEndLevel;
        EventBus.PauseEvent += () =>
        {
            DisablePlayerInput();
        };
        EventBus.UnPauseEvent += () =>
        {
            EnablePlayerInput();
        };
        EventBus.UIEvents.OnMainMenuWindowShow += OnEndLevel;
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
    
    private void OnJump(InputAction.CallbackContext obj)
    {
        EventBus.InputEvents.OnJump?.Invoke();
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