using System;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> _triggers = new List<GameObject>();
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _rightSprite;
    
    public event Action OnInteract;

    private SpriteRenderer _spriteRenderer;
    private bool _isActive;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        foreach (GameObject trigger in _triggers)
        {
            if(trigger.TryGetComponent(out ITriggerable triggerable))
            {
                triggerable.OnTriggerEnd += () =>
                {
                    _isActive = false;
                    _spriteRenderer.sprite = _rightSprite;
                };
            }
        }
    }

    public void Interact()
    {
        if (_isActive == false)
        {
            OnInteract?.Invoke();
            _isActive = true;
            _spriteRenderer.sprite = _leftSprite;

            foreach (GameObject trigger in _triggers)
            {
                if(trigger.TryGetComponent(out ITriggerable triggerable))
                {
                    triggerable.Trigger();
                }
            }
        }
    }
}