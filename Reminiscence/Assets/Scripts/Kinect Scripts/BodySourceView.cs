using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class BodySourceView : MonoBehaviour 
{
   
    public GameObject trakedJointPrefab;


    public BodySourceManager bodyManager;

    private Dictionary<ulong, GameObject> bodies = new Dictionary<ulong, GameObject>();

   
    private List<Kinect.JointType> joints = new List<Kinect.JointType> { 
    
        Kinect.JointType.HandLeft,
        Kinect.JointType.HandRight,
        Kinect.JointType.SpineMid,
        Kinect.JointType.Head
    };
    
    
    
    void Update () 
    {
        #region Get Kinect data
        if (bodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = bodyManager.GetData();
        if (data == null)
        {
            return;
        }
        
        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
              }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        #endregion

        #region Delete Kinect bodies
        List<ulong> knownIds = new List<ulong>(bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                Destroy(bodies[trackingId]);
                bodies.Remove(trackingId);
            }
        }
        #endregion


        #region Create Kinect bodies
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                if(!bodies.ContainsKey(body.TrackingId))
                {
                    bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                
                UpdateBodyObject(body, bodies[body.TrackingId]);
            }
        }
        #endregion
    }

    private GameObject CreateBodyObject(ulong id)
    {
        GameObject body = new GameObject("Body:" + id);
        PlayerTracker.instance.player = body;

        foreach (Kinect.JointType jt in joints)
        {
            GameObject newJoint = Instantiate(trakedJointPrefab);
            PlayerTracker.instance.joints.Add(jt, newJoint);
            newJoint.name = jt.ToString();            
            newJoint.transform.parent = body.transform;
        }
        
        return body;
    }
    
    private void UpdateBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        foreach (Kinect.JointType jt in joints)
        {
            Kinect.Joint sourceJoint = body.Joints[jt];
            Vector3 targetPosition = GetVector3FromJoint(sourceJoint);
           
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            PlayerTracker.instance.oldPos[jt] = jointObj.position;
            PlayerTracker.instance.newPos[jt] = targetPosition;

            jointObj.position = targetPosition;
            
           
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
