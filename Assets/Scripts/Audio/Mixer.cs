using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Mixer : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unpaused;
    public AudioMixer masterMixer;

    public void MusicVolumeMix(float musicLvl)
    {
        masterMixer.SetFloat("Volume", musicLvl);
    }



}
