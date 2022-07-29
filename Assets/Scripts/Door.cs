using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        CableManager.Instance.OnEveryCableOn += CableManager_OnEveryCableOn;
    }

    private void OpenDoor()
    {
        _animator.SetTrigger("OpenDoor");

        ScreenShake.Instance.Shake();

        AudioSource audioSource = AudioManager.Instance.GetComponent<AudioSource>();
        audioSource.clip = AudioManager.Instance.doorOpenedAudio;
        audioSource.pitch = 1f;
        audioSource.Play();
    }

    private void CableManager_OnEveryCableOn()
    {
        OpenDoor();
    }

}
