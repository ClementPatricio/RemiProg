using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class HotSpot : MonoBehaviour
{

    public float hotness;
    public Transform player;
    private bool inZone;
    private SphereCollider zone;

    public IK ikMotor;

    float stepAngle;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        hotness = 0;
        this.zone = this.GetComponent<SphereCollider>();
        this.stepAngle =360f / ikMotor.stepPerRevolution ;
    }

    // Update is called once per frame
    void Update()
    {
        
        hotness = this.HotOrCold();

        AkSoundEngine.SetRTPCValue("Music_RTPC", hotness, null);
        this.IkToPlaceHotSpot();
        if(hotness > 90 && once)
        {
            AkSoundEngine.PostEvent("LogAudio_Unlocked", this.gameObject);
            once = false;
            Arduino.sendMessageToArduino("bli");
        }

    }

    float HotOrCold()
    {
        float hotPercentage = 0;
        if (!inZone)
        {
            return hotPercentage;
        }
        hotPercentage = (zone.radius-Vector3.Distance(player.position, this.transform.position)) /zone.radius*100;

        return hotPercentage;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            this.inZone = true;
            player = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.inZone = false;
            player = null;
        }
    }

    void IkToPlaceHotSpot()
    {
        
        //ikMotor.resolveIK();
        //this.transform.position = ikMotor.end.position;
        //ikMotor.resetRotations();
    }
}
