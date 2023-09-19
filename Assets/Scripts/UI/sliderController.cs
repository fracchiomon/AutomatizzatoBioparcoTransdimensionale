using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderController : MonoBehaviour
{
    private GiocoManager _game;
    // Start is called before the first frame update
    void Awake()
    {
        this._game = GiocoManager.Instance;

    }

    public void SetMasterVolume(float volume)
    {
        if (this._game != null)
            this._game.SETTINGS_MasterVolumeSlider(volume);
    }
    public void SetMusicVolume(float volume)
    {
        if (this._game != null)
            this._game.SETTINGS_MusicVolumeSlider(volume);
    }
    public void SetEFXVolume(float volume)
    {
        if (this._game != null)
            this._game.SETTINGS_EFXVolumeSlider(volume);
    }


}
