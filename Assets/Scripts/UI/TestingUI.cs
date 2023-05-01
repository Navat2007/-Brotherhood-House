using System;
using UnityEngine;
using UnityEngine.UI;

public class TestingUI : MonoBehaviour
{
    [SerializeField] private Button _rotate0Button;
    [SerializeField] private Button _rotate90Button;
    [SerializeField] private Button _rotate180Button;
    [SerializeField] private Button _rotate270Button;

    private void Awake()
    {
        _rotate0Button.onClick.AddListener(() =>
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        });
        
        _rotate90Button.onClick.AddListener(() =>
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        });
        
        _rotate180Button.onClick.AddListener(() =>
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        });
        
        _rotate270Button.onClick.AddListener(() =>
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        });
        
    }
}