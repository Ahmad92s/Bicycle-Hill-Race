using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip boom, win, lose;


    private void Start()
    {
        instance = this;
    }

    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "win":
                this.GetComponent<AudioSource>().PlayOneShot(win);
                break;
            case "boom":
                this.GetComponent<AudioSource>().PlayOneShot(boom);
                break;
            case "lose":
                this.GetComponent<AudioSource>().PlayOneShot(lose);
                break;
            default:
                break;
        }
    }
}
