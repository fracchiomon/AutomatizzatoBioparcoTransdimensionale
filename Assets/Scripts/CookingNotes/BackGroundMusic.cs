using UnityEngine;
using FMODUnity;

public class BackGroundMusic : MonoBehaviour
{
    private StudioEventEmitter _audioSource;
    public StudioEventEmitter audioSource => this._audioSource;

    private void Start()
    {
        this._audioSource = this.GetComponentInChildren<StudioEventEmitter>();
    }
}
