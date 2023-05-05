using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    public static Action StartLevelEvent;
    public static Action EndLevelEvent;
    
    public static Action PauseEvent;
    public static Action UnPauseEvent;

    public static class PlayerEvents
    {
        public static Action OnDeath;
    }

    public static class InputEvents
    {
        public static Action<Vector2> OnInputMoveChange;
        public static Action OnJump;
        public static Action OnInteract;
        public static Action OnDrop;
        public static Action OnAccelerationStart;
        public static Action OnAccelerationEnd;
    }
    
    public static class UIEvents
    {
        public static Action<float> OnTimerChanged;
        public static Action OnMainMenuWindowShow;
        public static Action OnPauseWindowShow;
        public static Action OnPauseWindowHide;
        public static Action OnSettingsWindowShow;
        public static Action OnGameOverWindowShow;
        public static Action OnPlumberGameWindowShow;
        public static Action OnSpotsGameWindowShow;
        public static Action OnElectricGameWindowShow;
        public static Action OnPairsGameWindowShow;
    }
    
    public static class MiniGamesEvents
    {
        public static Action OnMiniGameStart;
        public static Action OnMiniGameEnd;
        
        public static Action OnSpotsGameStart;
        public static Action<bool> OnSpotsGameEnd;
        public static Action OnElectricGameStart;
        public static Action<bool> OnElectricGameEnd;
        public static Action OnPairsGameStart;
        public static Action<bool> OnPairsGameEnd;
        public static Action OnPlumberGameStart;
        public static Action<bool> OnPlumberGameEnd;
    }
    
    public static class ItemEvents
    {
        public static Action OnWheelPickUp;
        public static Action OnFlowerPickUp;
        public static Action OnBatteryPickUp;
        public static Action OnPicturePickUp;
    }
    
    public static class SoundEvents
    {
        public static Action OnSoundOn;
        public static Action OnSoundOff;
    }
}