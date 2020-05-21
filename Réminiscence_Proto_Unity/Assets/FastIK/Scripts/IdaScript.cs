using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.GameObject;

public class IdaScript : MonoBehaviour


{
    
    public GameObject target;

    public GameObject player;
    public GameObject tête;

    private Vector3 prevPos;


    Vector3 targetpos;

    Vector3 playerpos;
    
    Vector3 dirPlayer;

    private float negativeXToX, negativeXToY, negativeXToZ; // on each line, one is 1 and two are 0
    private float negativeYToX, negativeYToY, negativeYToZ;
    private float negativeZToX, negativeZToY, negativeZToZ;
    private float positiveXToX, positiveXToY, positiveXToZ;
    private float positiveYToX, positiveYToY, positiveYToZ;
    private float positiveZToX, positiveZToY, positiveZToZ;

    [Tooltip("-X")]
    public Vector3 negativeX;
    [Tooltip("+X")]
    public Vector3 positiveX;
    [Tooltip("-Y")]
    public Vector3 negativeY;
    [Tooltip("+Y")]
    public Vector3 positiveY;
    [Tooltip("-Z")]
    public Vector3 negativeZ;
    [Tooltip("+Z")]
    public Vector3 positiveZ;

    public void OnValidate()
    {
        negativeXToX = negativeX.x = negativeX.x == 0 ? 0 : negativeX.x / Mathf.Abs(negativeX.x);
        negativeXToY = negativeX.y = negativeX.y == 0 ? 0 : negativeX.y / Mathf.Abs(negativeX.y);
        negativeXToZ = negativeX.z = negativeX.z == 0 ? 0 : negativeX.z / Mathf.Abs(negativeX.z);
        negativeYToX = negativeY.x = negativeY.x == 0 ? 0 : negativeY.x / Mathf.Abs(negativeY.x);
        negativeYToY = negativeY.y = negativeY.y == 0 ? 0 : negativeY.y / Mathf.Abs(negativeY.y);
        negativeYToZ = negativeY.z = negativeY.z == 0 ? 0 : negativeY.z / Mathf.Abs(negativeY.z);
        negativeZToX = negativeZ.x = negativeZ.x == 0 ? 0 : negativeZ.x / Mathf.Abs(negativeZ.x);
        negativeZToY = negativeZ.y = negativeZ.y == 0 ? 0 : negativeZ.y / Mathf.Abs(negativeZ.y);
        negativeZToZ = negativeZ.z = negativeZ.z == 0 ? 0 : negativeZ.z / Mathf.Abs(negativeZ.z);

        positiveXToX = positiveX.x = positiveX.x == 0 ? 0 : positiveX.x / Mathf.Abs(positiveX.x);
        positiveXToY = positiveX.y = positiveX.y == 0 ? 0 : positiveX.y / Mathf.Abs(positiveX.y);
        positiveXToZ = positiveX.z = positiveX.z == 0 ? 0 : positiveX.z / Mathf.Abs(positiveX.z);
        positiveYToX = positiveY.x = positiveY.x == 0 ? 0 : positiveY.x / Mathf.Abs(positiveY.x);
        positiveYToY = positiveY.y = positiveY.y == 0 ? 0 : positiveY.y / Mathf.Abs(positiveY.y);
        positiveYToZ = positiveY.z = positiveY.z == 0 ? 0 : positiveY.z / Mathf.Abs(positiveY.z);
        positiveZToX = positiveZ.x = positiveZ.x == 0 ? 0 : positiveZ.x / Mathf.Abs(positiveZ.x);
        positiveZToY = positiveZ.y = positiveZ.y == 0 ? 0 : positiveZ.y / Mathf.Abs(positiveZ.y);
        positiveZToZ = positiveZ.z = positiveZ.z == 0 ? 0 : positiveZ.z / Mathf.Abs(positiveZ.z);
    }


    /* public enum CHOICE { minus_x, minus_y, minus_z, plus_x, plus_y, plus_z };
     public CHOICE minus_x, plus_x, minus_y, plus_y, minus_z, plus_z;

     void OnValidate()
     {
         if (plus_x == CHOICE.plus_x) { positiveXToX = +1; positiveXToY = 0; positiveXToZ = 0; }
         if (plus_x == CHOICE.minus_x) { positiveXToX = -1; positiveXToY = 0; positiveXToZ = 0; }
         if (plus_x == CHOICE.plus_y) { positiveXToX = 0; positiveXToY = +1; positiveXToZ = 0; }
         if (plus_x == CHOICE.minus_y) { positiveXToX = 0; positiveXToY = -1; positiveXToZ = 0; }
         if (plus_x == CHOICE.plus_z) { positiveXToX = 0; positiveXToY = 0; positiveXToZ = +1; }
         if (plus_x == CHOICE.minus_z) { positiveXToX = 0; positiveXToY = 0; positiveXToZ = -1; }

         if (plus_y == CHOICE.plus_x) { positiveYToX = +1; positiveYToY = 0; positiveYToZ = 0; }
         if (plus_y == CHOICE.minus_x) { positiveYToX = -1; positiveYToY = 0; positiveYToZ = 0; }
         if (plus_y == CHOICE.plus_y) { positiveYToX = 0; positiveYToY = +1; positiveYToZ = 0; }
         if (plus_y == CHOICE.minus_y) { positiveYToX = 0; positiveYToY = -1; positiveYToZ = 0; }
         if (plus_y == CHOICE.plus_z) { positiveYToX = 0; positiveYToY = 0; positiveYToZ = +1; }
         if (plus_y == CHOICE.minus_z) { positiveYToX = 0; positiveYToY = 0; positiveYToZ = -1; }

         if (plus_z == CHOICE.plus_x) { positiveZToX = +1; positiveZToY = 0; positiveZToZ = 0; }
         if (plus_z == CHOICE.minus_x) { positiveZToX = -1; positiveZToY = 0; positiveZToZ = 0; }
         if (plus_z == CHOICE.plus_y) { positiveZToX = 0; positiveZToY = +1; positiveZToZ = 0; }
         if (plus_z == CHOICE.minus_y) { positiveZToX = 0; positiveZToY = -1; positiveZToZ = 0; }
         if (plus_z == CHOICE.plus_z) { positiveZToX = 0; positiveZToY = 0; positiveZToZ = +1; }
         if (plus_z == CHOICE.minus_z) { positiveZToX = 0; positiveZToY = 0; positiveZToZ = -1; }

         if (minus_x == CHOICE.plus_x) { negativeXToX = +1; negativeXToY = 0; negativeXToZ = 0; }
         if (minus_x == CHOICE.minus_x) { negativeXToX = -1; negativeXToY = 0; negativeXToZ = 0; }
         if (minus_x == CHOICE.plus_y) { negativeXToX = 0; negativeXToY = +1; negativeXToZ = 0; }
         if (minus_x == CHOICE.minus_y) { negativeXToX = 0; negativeXToY = -1; negativeXToZ = 0; }
         if (minus_x == CHOICE.plus_z) { negativeXToX = 0; negativeXToY = 0; negativeXToZ = +1; }
         if (minus_x == CHOICE.minus_z) { negativeXToX = 0; negativeXToY = 0; negativeXToZ = -1; }

         if (minus_y == CHOICE.plus_x) { negativeYToX = +1; negativeYToY = 0; negativeYToZ = 0; }
         if (minus_y == CHOICE.minus_x) { negativeYToX = -1; negativeYToY = 0; negativeYToZ = 0; }
         if (minus_y == CHOICE.plus_y) { negativeYToX = 0; negativeYToY = +1; negativeYToZ = 0; }
         if (minus_y == CHOICE.minus_y) { negativeYToX = 0; negativeYToY = -1; negativeYToZ = 0; }
         if (minus_y == CHOICE.plus_z) { negativeYToX = 0; negativeYToY = 0; negativeYToZ = +1; }
         if (minus_y == CHOICE.minus_z) { negativeYToX = 0; negativeYToY = 0; negativeYToZ = -1; }

         if (minus_z == CHOICE.plus_x) { negativeZToX = +1; negativeZToY = 0; negativeZToZ = 0; }
         if (minus_z == CHOICE.minus_x) { negativeZToX = -1; negativeZToY = 0; negativeZToZ = 0; }
         if (minus_z == CHOICE.plus_y) { negativeZToX = 0; negativeZToY = +1; negativeZToZ = 0; }
         if (minus_z == CHOICE.minus_y) { negativeZToX = 0; negativeZToY = -1; negativeZToZ = 0; }
         if (minus_z == CHOICE.plus_z) { negativeZToX = 0; negativeZToY = 0; negativeZToZ = +1; }
         if (minus_z == CHOICE.minus_z) { negativeZToX = 0; negativeZToY = 0; negativeZToZ = -1; }
     }
     */

    // Start is called before the first frame update
    void Start()
    {
        target = this.gameObject;
        player = GameObject.FindWithTag("Player");
        tête = GameObject.FindWithTag("Tête");
        //player = GameObject.transform.Find("Player");

        targetpos = target.transform.position;
        playerpos = player.transform.position;

        prevPos = player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        

       // dirPlayer = (playerpos - tête.transform.position).normalized;

        Debug.DrawLine(player.transform.position, tête.transform.position, Color.red);
        
        MoveTarget();
    }

    public void MoveTarget()
    {
        Vector3 deltaPos = TranslatePosition(player.transform.position - prevPos);
        this.transform.position += deltaPos;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, - 1.0f, 1.0f), transform.position.y, Mathf.Clamp(transform.position.z, - 1.0f, 1.0f));
        Debug.Log("le mouvement " + (player.transform.position - prevPos) + " a généré " + deltaPos);
        prevPos = player.transform.position;
    }

    Vector3 TranslatePosition (Vector3 playerPos)
    {
        Vector4 playerPos4 = playerPos;
        Matrix4x4 matrixIda = new Matrix4x4(new Vector4(positiveXToX * (1 + Mathf.Sign(playerPos.x)) / 2f - negativeXToX * (1 - Mathf.Sign(playerPos.x)) / 2f,
                                                        positiveXToY * (1 + Mathf.Sign(playerPos.x)) / 2f - negativeXToY * (1 - Mathf.Sign(playerPos.x)) / 2f,
                                                        positiveXToZ * (1 + Mathf.Sign(playerPos.x)) / 2f - negativeXToZ * (1 - Mathf.Sign(playerPos.x)) / 2f, 0),
                                            new Vector4(
                                                        positiveYToX * (1 + Mathf.Sign(playerPos.y)) / 2f - negativeYToX * (1 - Mathf.Sign(playerPos.y)) / 2f,
                                                        positiveYToY * (1 + Mathf.Sign(playerPos.y)) / 2f - negativeYToY * (1 - Mathf.Sign(playerPos.y)) / 2f,
                                                        positiveYToZ * (1 + Mathf.Sign(playerPos.y)) / 2f - negativeYToZ * (1 - Mathf.Sign(playerPos.y)) / 2f, 0),
                                            new Vector4(
                                                        positiveZToX * (1 + Mathf.Sign(playerPos.z)) / 2f - negativeZToX * (1 - Mathf.Sign(playerPos.z)) / 2f,
                                                        positiveZToY * (1 + Mathf.Sign(playerPos.z)) / 2f - negativeZToY * (1 - Mathf.Sign(playerPos.z)) / 2f,
                                                        positiveZToZ * (1 + Mathf.Sign(playerPos.z)) / 2f - negativeZToZ * (1 - Mathf.Sign(playerPos.z)) / 2f, 0),
                                            Vector4.zero);


        return matrixIda*playerPos4;
    }

} // Finish