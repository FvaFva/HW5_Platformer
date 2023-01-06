using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HomeObserver : MonoBehaviour
{
    [SerializeField] private UnityEvent _infiltrated;
    [SerializeField] private UnityEvent _houseSafe;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Thief>(out Thief thief))
        {
            _infiltrated.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _houseSafe.Invoke();
        }
    }
}
