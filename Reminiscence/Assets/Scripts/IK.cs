using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    public int stepPerRevolution = 512;

    private float stepAngle;

    public GameObject[] arms;

    public GameObject[] pivots;

    public Transform end;

    public Transform pointToReach;

    private float q1, q2;

    private Vector3 pointToRotateAround;

    float lenghtArm, lenghtForeArm;
    float angle, trueAngle;
    float q2angle, q1angle;
    int actualStep = 0;
    int moveStep = 0;


    void Awake()
    {
        lenghtArm = Vector3.Distance(this.pivots[0].transform.position, this.pivots[1].transform.position);
        lenghtForeArm = Vector3.Distance(this.pivots[1].transform.position, this.end.position);
        stepAngle = 360f / stepPerRevolution;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.end.position, this.pointToReach.position) > 0.02f && Vector3.Distance(this.pivots[0].transform.position, this.pointToReach.position) < lenghtArm+lenghtForeArm )
        {
            actualStep = AngleToStepCount(-angle);
            resetRotations();
            applyRotationsIK(resolveIK());
            moveStep = AngleToStepCount(-angle) - actualStep;
            //Debug.Log(moveStep);

        }

    }

    public Vector3 getAnglesForMotorAsVec3()
    {
        return new Vector3(-this.angle, this.q1angle, this.q2angle);
    }

    public Vector3 resolveIK()
    {
        
        pointToRotateAround = this.pivots[0].transform.position;
        pointToRotateAround.y = this.pointToReach.position.y;
        angle = Vector3.SignedAngle((pointToReach.position - pointToRotateAround), (new Vector3(this.end.position.x, pointToRotateAround.y, this.end.position.z)-pointToRotateAround),Vector3.up);
        trueAngle = angle;
        this.pointToReach.RotateAround(pointToRotateAround, Vector3.up, trueAngle);
        

        
        if (Mathf.Repeat(angle, stepAngle) <= stepAngle / 2f)
        {
            angle -= (Mathf.Repeat(angle, stepAngle));
        }
        else
        {
            angle += stepAngle - (Mathf.Repeat(angle, stepAngle));
        }
        


        Vector3 pointOffset = pointToReach.position - this.pivots[0].transform.position;
        
        //lenghtArm = 0.9f;
        //lenghtForeArm = 0.9f;

        q2 = -Mathf.Acos((Mathf.Pow(pointOffset.x, 2) + Mathf.Pow(pointOffset.y, 2) - Mathf.Pow(lenghtArm, 2) - Mathf.Pow(lenghtForeArm, 2))/(2*lenghtArm*lenghtForeArm));

        
        
      

        //q2 = q2angle * Mathf.Deg2Rad;


        q1 = Mathf.Atan(pointOffset.y / pointOffset.x) - Mathf.Atan((lenghtForeArm*Mathf.Sin(q2)) / (lenghtArm + lenghtForeArm*Mathf.Cos(q2)));


        //adapting q2 to stepMotor
        q2angle = q2 * Mathf.Rad2Deg;
       
        if (Mathf.Repeat(q2angle, stepAngle) <= stepAngle / 2f)
        {
            q2angle -= Mathf.Repeat(q2angle, stepAngle);
        }
        else
        {
            q2angle += stepAngle - (Mathf.Repeat(q2angle, stepAngle));
        }
        


        //adapting q1 to stepMotor
        q1angle = q1 * Mathf.Rad2Deg;
        
        if (Mathf.Repeat(q1angle, stepAngle) <= stepAngle / 2f)
        {
            q1angle -= Mathf.Repeat(q1angle, stepAngle);
        }
        else
        {
            q1angle += stepAngle - (Mathf.Repeat(q1angle, stepAngle));
        }
        

        this.pointToReach.RotateAround(pointToRotateAround, Vector3.up, -trueAngle);


        return new Vector3(-angle,q1angle,q2angle);
    }

    public void resetRotations()
    {
        
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, Vector3.up, angle);
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, this.transform.forward, -q1angle);
            this.arms[1].transform.RotateAround(this.pivots[1].transform.position, this.transform.forward, -q2angle);
        

    }
    public void applyRotationsIK(Vector3 angles)
    {
        
            
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, Vector3.up, angles[0]);
            this.arms[0].transform.RotateAround(this.pivots[0].transform.position, this.transform.forward, angles[1]);
            this.arms[1].transform.RotateAround(this.pivots[1].transform.position, this.transform.forward, angles[2]);

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, lenghtArm + lenghtForeArm);
    }

    int AngleToStepCount(float angle)
    {
        return (int)(angle / stepAngle) + 256;
    }
}
