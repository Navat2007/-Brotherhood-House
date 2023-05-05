using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private Transform _hallDoor;
    [SerializeField] private Transform _garageDoor;
    [SerializeField] private Transform _galleryDoor;
    [SerializeField] private Transform _galleryDoor2;
    [SerializeField] private Transform _windowDoor;
    [SerializeField] private Transform _flowersDoor;

    private void Awake()
    {
        EventBus.MiniGamesEvents.OnPlumberGameEnd += OnPlumberGameEnd;
        EventBus.MiniGamesEvents.OnSpotsGameEnd += OnSpotsGameEnd;
        EventBus.MiniGamesEvents.OnPairsGameEnd += OnPairsGameEnd;
        
        EventBus.ItemEvents.OnWheelPickUp += OnWheelPickUp;
        EventBus.ItemEvents.OnFlowerPickUp += OnFlowerPickUp;
        EventBus.ItemEvents.OnPicturePickUp += OnPicturePickUp;
    }

    private void OnDestroy()
    {
        EventBus.MiniGamesEvents.OnPlumberGameEnd -= OnPlumberGameEnd;
        EventBus.MiniGamesEvents.OnSpotsGameEnd -= OnSpotsGameEnd;
        EventBus.MiniGamesEvents.OnPairsGameEnd -= OnPairsGameEnd;
        
        EventBus.ItemEvents.OnWheelPickUp -= OnWheelPickUp;
        EventBus.ItemEvents.OnFlowerPickUp -= OnFlowerPickUp;
        EventBus.ItemEvents.OnPicturePickUp -= OnPicturePickUp;
    }
    
    private void OnPicturePickUp()
    {
        if(_galleryDoor2 != null)
            Destroy(_galleryDoor2.gameObject);
    }

    private void OnFlowerPickUp()
    {
        if(_flowersDoor != null)
            Destroy(_flowersDoor.gameObject);
    }

    private void OnWheelPickUp()
    {
        if(_hallDoor != null)
            Destroy(_hallDoor.gameObject);
    }

    private void OnPairsGameEnd(bool success)
    {
        if(success)
            Destroy(_windowDoor.gameObject);
    }

    private void OnSpotsGameEnd(bool success)
    {
        if(success)
            Destroy(_galleryDoor.gameObject);
    }

    private void OnPlumberGameEnd(bool success)
    {
        if(success)
            Destroy(_garageDoor.gameObject);
    }
}