using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{

    public GameObject[] arms;

    public GameObject[] pivots;

    public Transform end;

    public Transform pointToReach;

    private float q1, q2;

    private Vector3 pointToRotateAround;

    float lenghtArm, lenghtForeArm;
    float angle;


    void Awake()
    {
        lenghtArm = Vector3.Distance(this.pivots[0].transform.position, this.pivots[1].transform.position);
        lenghtForeArm = Vector3.Distance(this.pivots[1].transform.position, this.end.position);
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.end.position, this.pointToReach.position) > 0.01 && Vector3.Distance(this.pivots[0].transform.position, this.pointToReach.position) < lenghtArm+lenghtForeArm )
        {
            resetRotations();
            resolveIK();
        }

    }

    void resolveIK()
    {
        pointToRotateAround = this.pivots[0].transform.position;
        pointToRotateAround.y = this.pointToReach.position.y;
        angle = Vector3.SignedAngle((pointToReach.position - pointToRotateAround), (new Vector3(this.end.position.x, pointToRotateAround.y, this.end.position.z)-pointToRotateAround),Vector3.up);

        this.pointToReach.RotateAround(pointToRotateAround, Vector3.up, angle);

        
        Vector3 pointOffset = pointToReach.position - this.pivots[0].transform.position;
        
        //lenghtArm = 0.9f;
        //lenghtForeArm = 0.9f;

        q2 = Mathf.Acos((Mathf.Pow(pointOffset.x, 2) + Mathf.Pow(pointOffset.y, 2) - Mathf.Pow(lenghtArm, 2) - Mathf.Pow(lenghtForeArm, 2))/(2*lenghtArm*lenghtForeArm));

        q1 = Mathf.Atan(pointOffset.y / pointOffset.x) - Mathf.Atan((lenghtForeArm*Mathf.Sin(q2)) / (lenghtArm + lenghtForeArm*Mathf.Cos(q2)));

        q2 = q2 * Mathf.Rad2Deg;

        q1 = q1 * Mathf.Rad2Deg;

        if(pointOffset.x > 0)
        {
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, Vector3.up, -angle);
            this.pointToReach.RotateAround(pointToRotateAround, Vector3.up, -angle);
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, this.transform.forward, q1);
            this.arms[1].transform.RotateAround(this.pivots[1].transform.position, this.transform.forward, q2);
            

        }
        else
        {
            q1 += 180;
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, Vector3.up, -angle);
            this.pointToReach.RotateAround(pointToRotateAround, Vector3.up, -angle);
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, this.transform.forward, q1);
            this.arms[1].transform.RotateAround(this.pivots[1].transform.position, this.transform.forward, q2);
            
        }


    }

    void resetRotations()
    {
        if((pointToReach.transform.position - this.pivots[0].transform.position).x > 0)
        {
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, Vector3.up, angle);
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, this.transform.forward, -q1);
            this.arms[1].transform.RotateAround(this.pivots[1].transform.position, this.transform.forward, -q2);
        }
        else
        {
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, Vector3.up, angle);
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, this.transform.forward, -q1);
            this.arms[1].transform.RotateAround(this.pivots[1].transform.position, this.transform.forward, -q2);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.9f);
    }
}
