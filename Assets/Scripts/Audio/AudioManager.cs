using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private Player player;
    public AudioClip[] clips;
    public AudioSource source;

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
        //source = new AudioSource();
        AudioSource source = player.GetComponent<AudioSource>();

        source.clip = clips[index];

        source.pitch = Random.Range(1f, 1.5f);

        source.Play();

    }
}
