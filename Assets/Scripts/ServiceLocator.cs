using UnityEngine;

public static class ServiceLocator
{
    public static GameManager GameManager;
    public static AudioManager AudioManager;
    public static MusicManager MusicManager;
    public static InputManager InputManager;
    public static SaveLoadManager SaveLoadManager;
    
    public static Player Player;
    
    public static Timer Timer;

    public static void CheckServices()
    {
        if(GameManager == null)
            Debug.Log("GameManager is null");
        
        if(AudioManager == null)
            Debug.Log("AudioManager is null");
        
        if(MusicManager == null)
            Debug.Log("MusicManager is null");
        
        if(InputManager == null)
            Debug.Log("InputManager is null");
        
        if(SaveLoadManager == null)
            Debug.Log("SaveLoadManager is null");
        
        if(Timer == null)
            Debug.Log("Timer is null");
        
        if(Player == null)
            Debug.Log("Player is null");
    }
}