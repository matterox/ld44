using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomPitchSound : MonoBehaviour
{
    [SerializeField]
    private float minPitch = 0.95f;
    [SerializeField]
    private float maxPitch = 1.05f;
    private void Start()
    {
        AudioSource src = GetComponent<AudioSource>();
        src.pitch = Random.Range(minPitch, maxPitch);
        src.Play();
    }
}
