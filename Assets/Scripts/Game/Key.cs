using System;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable, IPickable, IDropable
{
    [SerializeField] private Transform _interactUI;
    
    public event Action OnInteract;
    
    public void Interact()
    {
        PickUp();
    }

    public void PickUp()
    {
        ServiceLocator.Player.PickUpItem(gameObject);
        _interactUI.gameObject.SetActive(false);
    }

    public void Drop()
    {
        ServiceLocator.Player.DropItem(gameObject);
        _interactUI.gameObject.SetActive(true);
    }
}