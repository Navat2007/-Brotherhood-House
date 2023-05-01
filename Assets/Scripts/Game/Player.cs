using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string ANIMATOR_WALK = "Walk";
    
    [SerializeField] private float _speed = 100;
    [SerializeField] private float _jumpForce = 5;
    
    [Header("Visual")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        EventBus.InputEvents.OnInputMoveChange += OnInputMoveChange;
        EventBus.InputEvents.OnJump += OnJump;
    }

    private void OnJump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private void OnInputMoveChange(Vector2 moveDirection)
    {
        float moveX = moveDirection.x * _speed * Time.deltaTime;
        _rigidbody.velocity = new Vector2(moveX, _rigidbody.velocity.y);
        
        if(moveDirection.x != 0)
            _spriteRenderer.flipX = moveDirection.x < 0;
        
        _animator.SetBool(ANIMATOR_WALK, moveDirection.x != 0);
    }
}