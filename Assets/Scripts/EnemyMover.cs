using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _patrolDistance;

    private Vector3 _firstPosition;
    private Vector3 _secondPosition;
    private Vector3 _targetPosition;
    private Vector2 _trevelDirection = Vector2.right;
    private float _distanceRested = 0000.1f;
    private readonly RaycastHit2D[] _rest = new RaycastHit2D[1];
    private Rigidbody2D _body;

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
        _firstPosition = transform.position;
        _secondPosition = transform.position + new Vector3(_patrolDistance,0);
        _targetPosition = _secondPosition;
    }

    private void Update()
    {
        MovingOn();
    }

    private void MovingOn()
    {
        if (_body.Cast(_trevelDirection, _rest, _distanceRested) != 0 || transform.position == _targetPosition)
        {
            SwapDirection();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
    }

    private void SwapDirection()
    {
        if(_trevelDirection == Vector2.right)
        {
            _trevelDirection = Vector2.left;
            _targetPosition = _firstPosition;           
        }
        else
        {
            _trevelDirection = Vector2.right;
            _targetPosition = _secondPosition;
        }

        transform.Rotate(0, 180, 0);
    }
}
