using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    public static Action StartLevelEvent;
    public static Action EndLevelEvent;
    
    public static Action PauseEvent;
    public static Action UnPauseEvent;

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
        public static Action OnLeaderBoardWindowShow;
        public static Action OnGameOverWindowShow;
    }
    
    public static class SoundEvents
    {
        public static Action OnSoundOn;
        public static Action OnSoundOff;
    }
}