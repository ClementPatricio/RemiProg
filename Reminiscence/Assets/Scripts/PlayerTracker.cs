using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;


public class PlayerTracker : MonoBehaviour
{

    public GameObject player;

    public Dictionary<Kinect.JointType, Vector3> oldPos = new Dictionary<Kinect.JointType, Vector3>();
    public Dictionary<Kinect.JointType, Vector3> newPos = new Dictionary<Kinect.JointType, Vector3>();

    public Dictionary<Kinect.JointType, GameObject> joints = new Dictionary<Kinect.JointType, GameObject>();

    private Vector3 chosenTranslate;

    private Vector3 chosenPos;

    private Vector3 posBetweenHands;

    private float distBetwweenHands;

    public GameObject robotPoint;
    bool firstTime = true;

    public float sensitivity;

    #region Singleton
    public static PlayerTracker instance;


    void Awake()
    {
        instance = this;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        //float chosenY = Mathf.NegativeInfinity;
        if(player == null)
        {
            firstTime = true;
            return;
        }

        /*
        chosenTranslate.x = newPos[Kinect.JointType.SpineMid].x - this.transform.position.x;
        chosenTranslate.z = newPos[Kinect.JointType.SpineMid].z - this.transform.position.z;

        foreach (KeyValuePair<Kinect.JointType, GameObject>  jt in joints)
        {
            
            if(this.newPos[jt.Key].y > chosenY)
            {
                chosenY = this.newPos[jt.Key].y;
            }

        }

        chosenTranslate.y = chosenY - this.transform.position.y;
        */

        distBetwweenHands = (newPos[Kinect.JointType.HandLeft] - newPos[Kinect.JointType.HandRight]).magnitude;

        posBetweenHands = Vector3.Lerp(newPos[Kinect.JointType.HandLeft], newPos[Kinect.JointType.HandRight], 0.5f);
        chosenPos = Vector3.Lerp(newPos[Kinect.JointType.SpineMid], posBetweenHands, 0.5f);

        //Debug.Log("Distance Between Hands : " + distBetwweenHands);

        chosenTranslate = chosenPos - this.transform.position;

        this.transform.Translate(chosenTranslate);
        if (firstTime)
        {
            firstTime = false;
        }
        else
        {
            this.robotPoint.transform.Translate(chosenTranslate*sensitivity);
        }
    }
}
