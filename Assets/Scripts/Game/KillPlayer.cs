using System;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        EventBus.PlayerEvents.OnDeath?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        EventBus.PlayerEvents.OnDeath?.Invoke();
    }
}