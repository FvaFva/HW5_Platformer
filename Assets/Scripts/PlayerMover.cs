using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    private Rigidbody2D _body;
    private Animator _animator;
    private Coroutine _jump;
    private bool _isRun;
    private WaitForSeconds _pause = new WaitForSeconds(0.1f);
    private float _distanceToGroundForJump = 0000.1f;
    private readonly RaycastHit2D[] _downPlace = new RaycastHit2D[10];

    private void Awake()
    {     
        TryGetComponent<Rigidbody2D>(out _body);
        TryGetComponent<Animator>(out _animator);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(IsGrounded())
            {
                Jumping();
            }
        }

        _isRun = false;

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(_speed * Time.deltaTime,0,0);
            transform.rotation = new Quaternion(0, 0, 0, 0);
            _isRun = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(_speed * Time.deltaTime, 0, 0);
            transform.rotation = new Quaternion(0, 180, 0, 0);
            _isRun = true;
        }

        _animator.SetBool("isRun", _isRun);
    }

    private void Jumping()
    {
        _body.AddForce(Vector2.up * _jumpForce);
        _animator.SetBool("isFall", true);
        _animator.SetTrigger("isJump");

        if (_jump != null)
            StopCoroutine(_jump);

        _jump = StartCoroutine(FlyInJump());
    }

    private IEnumerator FlyInJump()
    {
        yield return _pause;

        while (IsGrounded() == false)
            yield return null;

        _animator.SetBool("isFall", false);
    }

    private bool IsGrounded()
    {        
        if(_body.Cast(Vector2.down, _downPlace, _distanceToGroundForJump) == 0)
            return false;

        foreach (RaycastHit2D place in _downPlace)
        {
            if(place && place.transform.TryGetComponent<Ground>(out Ground ground))
                return true;
        }
        
        return false;
    }
}
