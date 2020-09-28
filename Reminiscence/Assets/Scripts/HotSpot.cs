using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class HotSpot : MonoBehaviour
{

    public float hotness;
    public Transform player;
    private bool inZone;
    public SphereCollider zone;
    public float unlockAtHotness = 90;


    public IK ikMotor;

    float stepAngle;
    private bool once = true;
    public float duration = 2f;
    public AK.Wwise.Event logEvent;
    public AnimationCurve musicCurve;

    // Start is called before the first frame update
    void Start()
    {
        hotness = 0;
        //this.zone = this.GetComponent<SphereCollider>();
        this.ikMotor = GameManager.instance.ikMotor;
        this.stepAngle =360f / ikMotor.stepPerRevolution ;
    }

    // Update is called once per frame
    void Update()
    {
        
        hotness = this.HotOrCold();

        AkSoundEngine.SetRTPCValue("Music_RTPC", musicCurve.Evaluate(hotness / 100.0f) * 100, null);
        
        this.IkToPlaceHotSpot();
        if(hotness > unlockAtHotness)
        {
            StartCoroutine("Unlock");
            //Arduino.sendMessageToArduino("bli");
            
        }
        else
        {
            StopCoroutine("Unlock");
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

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, zone.radius*(1-(unlockAtHotness/100)));
    }

    IEnumerator Unlock()
    {
        float i = 0;
        for(; ; )
        {
            i += Time.deltaTime;
            
            if (i > duration && once)
            {
                //AkSoundEngine.PostEvent("LogAudio_Unlocked", this.gameObject);
                GameManager.instance.setLog(this.logEvent);
                
                
                once = false;
                GameManager.instance.FinishLevel();
            }
            
            yield return null;
        }
        
    }

    
}
