using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const string ANIMATOR_WALK = "Walk";
    
    [SerializeField] private float _speed = 100;
    [SerializeField] private float _acceleration = 2;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _downPull = 5;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _interactLayer;
    
    [Header("Visual")]
    [SerializeField] private Transform _item;
    [SerializeField] private Transform _feet;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private Rigidbody2D _rigidbody;
    private Vector2 _startPosition;
    private int _jumpsLeft;
    private bool _isGrounded;
    private bool _isRunning;
    private float _fallTimer;
    private GameObject _currentItem;

    public void PickUpItem(GameObject item)
    {
        item.transform.SetParent(_item);
        item.transform.localPosition = Vector3.zero;
        _currentItem = item;
    }
    
    public void DropItem(GameObject item)
    {
        item.transform.SetParent(null);
        item.transform.position = transform.position;
        _currentItem = null;
    }

    private void Awake()
    {
        ServiceLocator.Player = this;
        
        _rigidbody = GetComponent<Rigidbody2D>();
        
        EventBus.StartLevelEvent += Reset;
        EventBus.InputEvents.OnInputMoveChange += OnInputMoveChange;
        EventBus.InputEvents.OnAccelerationStart += () => { _isRunning = true; };
        EventBus.InputEvents.OnAccelerationEnd += () => { _isRunning = false; };
        EventBus.InputEvents.OnJump += OnJump;
        EventBus.InputEvents.OnInteract += OnInteract;
        EventBus.InputEvents.OnDrop += OnDrop;
        
        _startPosition = transform.position;
        _jumpsLeft = _maxJumps;
    }

    private void OnDestroy()
    {
        EventBus.StartLevelEvent -= Reset;
        EventBus.InputEvents.OnInputMoveChange -= OnInputMoveChange;
        EventBus.InputEvents.OnAccelerationStart -= () => { _isRunning = true; };
        EventBus.InputEvents.OnAccelerationEnd -= () => { _isRunning = false; };
        EventBus.InputEvents.OnJump -= OnJump;
        EventBus.InputEvents.OnInteract -= OnInteract;
        EventBus.InputEvents.OnDrop -= OnDrop;
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_feet.position, 0.1f, _groundLayer) != null;
        
        if (_isGrounded)
        {
            _fallTimer = 0;
        }
        else
        {
            _fallTimer += Time.deltaTime;

            var downForce = _downPull * _fallTimer * _fallTimer;
            
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y - downForce);
        }
    }

    private void Reset()
    {
        _fallTimer = 0;
        _jumpsLeft = _maxJumps;
        transform.position = _startPosition;
        _rigidbody.velocity = Vector2.zero;
    }

    private void OnJump()
    {
        if (_jumpsLeft > 0)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _jumpsLeft--;
        }
    }

    private void OnInputMoveChange(Vector2 moveDirection)
    {
        float moveX = (moveDirection.x * _speed * Time.deltaTime) * (_isRunning ? _acceleration : 1);
        _rigidbody.velocity = new Vector2(moveX, _rigidbody.velocity.y);
        
        if(moveDirection.x != 0)
            _spriteRenderer.flipX = moveDirection.x < 0;
        
        _animator.SetBool(ANIMATOR_WALK, moveDirection.x != 0);
    }
    
    private void OnInteract()
    {
        Collider2D[] hits = new Collider2D[30];
        var size = Physics2D.OverlapCircleNonAlloc(transform.position, 0.3f, hits, _interactLayer);
        
        foreach (var hit in hits)
        {
            if (hit != null && hit.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }
    
    private void OnDrop()
    {
        if (_currentItem != null)
        {
            if (_currentItem.TryGetComponent(out IDropable dropable))
            {
                dropable.Drop();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(_isGrounded)
            _jumpsLeft = _maxJumps;
    }
}