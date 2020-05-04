using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_RTPC : MonoBehaviour
{
    [Range(0.0f, 100.0f)]
    public float rtpcValue = 0.0f;
    

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        AkSoundEngine.SetRTPCValue("Music_RTPC", rtpcValue, null);
    }
}
