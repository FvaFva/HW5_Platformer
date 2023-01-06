using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    private Rigidbody2D _body;
    private float _distanceToGroundForJump = 0000.1f;
    private readonly RaycastHit2D[] _downPlace = new RaycastHit2D[10];

    private void Awake()
    {     
        _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            bool isUnderFeetCollision  = _body.Cast(Vector2.down, _downPlace, _distanceToGroundForJump) != 0;

            if(isUnderFeetCollision && IsGrounded())
            {
                _body.AddForce(Vector2.up * _jumpForce);               
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime,0,0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1 * _speed * Time.deltaTime, 0, 0);
        }   
    }

    private bool IsGrounded()
    {
        foreach (RaycastHit2D place in _downPlace)
        {
            return place && place.transform.TryGetComponent<Ground>(out Ground ground);
        }
        
        return false;
    }
}
