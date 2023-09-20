using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    private AudioSource _audioSource;

    public AudioSource audioSource => this._audioSource;

    private void Start()
    {
        this._audioSource = this.GetComponentInChildren<AudioSource>();
    }
}
