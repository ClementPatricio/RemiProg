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
    public MatrixTransposer transposer;
    bool firstTime = true;

    public Vector3 sensitivity;

    #region Singleton
    public static PlayerTracker instance;


    void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        this.robotPoint = GameManager.instance.ikMotor.pointToReach.gameObject;
        GameManager.instance.sensitivity = new Vector3(this.sensitivity.x, this.sensitivity.y, this.sensitivity.z);
        GameManager.instance.setNewMatrix(this.transposer);
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.player = this.gameObject;
        //float chosenY = Mathf.NegativeInfinity;
        if (player == null)
        {
            firstTime = true;
            oldPos = new Dictionary<Kinect.JointType, Vector3>();
            newPos = new Dictionary<Kinect.JointType, Vector3>();
            joints = new Dictionary<Kinect.JointType, GameObject>();
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
            this.robotPoint.transform.Translate(new Vector3(transposer.TranslatePosition(chosenTranslate).x * sensitivity.x, transposer.TranslatePosition(chosenTranslate).y * sensitivity.y, transposer.TranslatePosition(chosenTranslate).z * sensitivity.z));
        }
    }

    void setChosenTranslate(Vector3 tr)
    {

    }
}
