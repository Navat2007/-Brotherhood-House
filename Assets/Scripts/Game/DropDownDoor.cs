using System;
using UnityEngine;

public class DropDownDoor : MonoBehaviour, ITriggerable
{
    private const string ANIMATION_TRIGGER = "Open";
    
    private Animator _animator;
    
    public event Action OnTriggerEnd;

    public void Trigger()
    {
        Debug.Log("Interact trigger");
        _animator.SetTrigger(ANIMATION_TRIGGER);
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void TriggerEnd()
    {
        OnTriggerEnd?.Invoke();
    }
}