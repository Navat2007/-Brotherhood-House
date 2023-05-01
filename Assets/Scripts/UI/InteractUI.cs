using System;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] private Transform _text;

    private void Awake()
    {
        _text.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _text.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _text.gameObject.SetActive(false);
    }
}