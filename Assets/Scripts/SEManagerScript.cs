using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManagerScript : MonoBehaviour
{
    public AudioClip SE;
    AudioSource audioSource;

    void Start () {
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(SE);
    }

    void Update() {
        if (!GetComponent<AudioSource>().isPlaying) {
            Destroy(this);
        }
    }

}
