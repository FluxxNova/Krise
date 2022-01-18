using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Mixer : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
    public AudioMixer masterMixer;

    public void VolumeMix(float musicLvl)
    {
        masterMixer.SetFloat("Volume", musicLvl);

    }
    public void FXVolumeMix(float musicLvl)
    {
        masterMixer.SetFloat("FXVolume", musicLvl);
    }

    public void MusicVolumeMix(float musicLvl)
    {
        masterMixer.SetFloat("MusicVolume", musicLvl);
    }

}
