using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightShowerHotSpot : MonoBehaviour
{
    public float hotness;
    public Transform player;
    public float distanceMax;
    public CapsuleCollider zone;
    public Mesh gizmo;

    public float speedToLock;

    private bool once = true;
    public float durationMax = 6f;
    float duration;
    public AK.Wwise.Event logEvent;
    public AnimationCurve musicCurve;

    // Start is called before the first frame update
    void Start()
    {
        hotness = 0;
        duration = durationMax;
        GameManager.instance.playerStartPos = this.transform;
    }

    // Update is called once per frame
    void Update()
    {

        hotness = this.HotOrCold();

        AkSoundEngine.SetRTPCValue("Music_RTPC", musicCurve.Evaluate(hotness / 100.0f) * 100, null);

    }

    float HotOrCold()
    {
        float hotPercentage = 0;
        if (Vector3.Distance(player.position, this.transform.position) > distanceMax)
        {
            return hotPercentage;
        }
        hotPercentage = (distanceMax - Vector3.Distance(player.position, this.transform.position)) / distanceMax * 100;

        return hotPercentage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine("Lock");
            StartCoroutine("Unlock");

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StopCoroutine("Unlock");
            StartCoroutine("Lock");
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawMesh(gizmo,transform.position, Quaternion.identity, new Vector3(zone.radius*2,zone.height/2,zone.radius*2));
    }

    IEnumerator Unlock()
    {
        
        for (; ; )
        {
            

            if (duration < 0 && once)
            {
                //GameManager.instance.setLog(this.logEvent);


                once = false;
                GameManager.instance.setLog(this.logEvent);

                GameManager.instance.FinishLevel();
                StopCoroutine("Unlock");

            }
            else
            {
                duration -= Time.deltaTime;
            }

            yield return null;
        }

    }

    IEnumerator Lock()
    {
        
        for (; ; )
        {
            

            if (duration < durationMax)
            {
                duration += Time.deltaTime * speedToLock;
            }

            yield return null;
        }

    }
}
