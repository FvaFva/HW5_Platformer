using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class SirenaVolumeController : MonoBehaviour
{
    [SerializeField] private float _volumeChangeStep;

    private AudioSource _sirena;
    private Coroutine _volumeChange;

    private void Awake()
    {
        _sirena = GetComponent<AudioSource>();

        if (_volumeChangeStep <= 0) 
            _volumeChangeStep = 0.1f;
    }

    public void SetVolume(float targetVolume)
    {
        if(_volumeChange != null)
            StopCoroutine(_volumeChange);

        _volumeChange = StartCoroutine(MoveVolume(targetVolume));
    }

    private IEnumerator MoveVolume(float targetVolume)
    {
        while(_sirena.volume != targetVolume)
        {
            _sirena.volume = Mathf.MoveTowards(_sirena.volume, targetVolume, _volumeChangeStep * Time.deltaTime);
            yield return null;
        }
    }
}

