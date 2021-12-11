using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Player player;
    public AudioClip[] clips;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void PlayClip(int index)
    {
        PlayClip(index, 1f, 1f);
    }

    public void PlayClip(int index, float volume, float pitch)
    {
        AudioSource source = player.GetComponent<AudioSource>();

        source.clip = clips[index];

        source.Play();

    }
}
