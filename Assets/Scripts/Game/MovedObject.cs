using UnityEngine;

public class MovedObject : MonoBehaviour
{
    [Header("Sensor")]
    [SerializeField] private bool _useSensors;
    [SerializeField] private Transform _sensorLeft;
    [SerializeField] private Transform _sensorRight;
    [SerializeField] private float _raycastLength = 0.2f;
    [Space]
    [Header("Settings")]
    [SerializeField] private bool _shouldMove;
    [SerializeField] private bool _stopMoveAtEnd;
    [SerializeField] private Vector2 _direction = Vector2.up;
    [SerializeField] private float _distance = 2f;
    [SerializeField] private float _speed = 2f;

    private SpriteRenderer _spriteRenderer;
    private Vector2 _startPosition;
    private bool _collide;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (_useSensors)
            ScanSensors();
        else
            ScanDistance();
        
        if(_shouldMove) Move();
    }

    private void Move()
    {
        transform.Translate(_direction.normalized * (_speed * Time.deltaTime));
    }

    private void ScanSensors()
    {
        var hitHorizontal = GetHorizontalSensorHit();
        var hitVertical = GetVerticalSensorHit();

        _collide = (hitHorizontal.collider != null && !hitHorizontal.collider.isTrigger) 
                   || (hitVertical.collider == null);
            
        if (_collide && _stopMoveAtEnd)
            _shouldMove = false;
        else if(_collide)
        {
            _collide = false;
            _sensorLeft.gameObject.SetActive(!_sensorLeft.gameObject.activeSelf);
            _sensorRight.gameObject.SetActive(!_sensorRight.gameObject.activeSelf);
            
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            
            ChangeDirection();
        }
    }

    private void ScanDistance()
    {
        var distance = Vector2.Distance(_startPosition, transform.position);
            
        if (distance >= _distance)
        {
            if(_stopMoveAtEnd) _shouldMove = false;
            _startPosition = transform.position;
            ChangeDirection();
        }
    }

    private RaycastHit2D GetHorizontalSensorHit()
    {
        var hit = new RaycastHit2D();
        
        if (_sensorLeft.gameObject.activeSelf)
        {
            hit = Physics2D.Raycast(_sensorLeft.position, Vector2.left, _raycastLength);
            Debug.DrawLine(_sensorLeft.position, new Vector3(_sensorLeft.position.x + (_raycastLength * _direction.x), _sensorLeft.position.y), Color.black);
        }

        if (_sensorRight.gameObject.activeSelf)
        {
            hit = Physics2D.Raycast(_sensorRight.position, Vector2.right, _raycastLength);
            Debug.DrawLine(_sensorRight.position, new Vector3(_sensorRight.position.x + (_raycastLength * _direction.x), _sensorRight.position.y), Color.black);
        }

        return hit;
    }
    
    private RaycastHit2D GetVerticalSensorHit()
    {
        var hit = new RaycastHit2D();
        
        if (_sensorLeft.gameObject.activeSelf)
        {
            hit = Physics2D.Raycast(_sensorLeft.position, Vector2.down, _raycastLength);
            Debug.DrawLine(_sensorLeft.position, new Vector3(_sensorLeft.position.x, _sensorLeft.position.y - _raycastLength ), Color.black);
        }

        if (_sensorRight.gameObject.activeSelf)
        {
            hit = Physics2D.Raycast(_sensorRight.position, Vector2.down, _raycastLength);
            Debug.DrawLine(_sensorRight.position, new Vector3(_sensorRight.position.x, _sensorRight.position.y - _raycastLength), Color.black);
        }

        return hit;
    }
    
    public void StartMove()
    {
        if(_shouldMove)
            return;

        _shouldMove = !_shouldMove;
    }
    
    public void StopMove()
    {
        _shouldMove = false;
    }

    public void Disable()
    {
        this.enabled = false;
    }

    public void ChangeDirection()
    {
        _direction *= -1;
    }

}
