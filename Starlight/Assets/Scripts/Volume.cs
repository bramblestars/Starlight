using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
    public AudioMixer master;
    public void SetLevel(float slider_val)
    {
        master.SetFloat("MasterVol", Mathf.Log10(slider_val) * 20);
    }
}
