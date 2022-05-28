using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundFx : MonoBehaviour
{
    public AudioSource hoverFx;
    public AudioSource clickFx;


    public void hoverSound()
    {
        hoverFx.Play();
    }

    public void clickSound()
    {
        clickFx.Play();
    }
    
}
