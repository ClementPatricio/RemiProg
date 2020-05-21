using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credits : The ida_script !

public class MatrixTransposer : MonoBehaviour
{
    public SOMatrix parameterMatrix;

    private float negativeXToX, negativeXToY, negativeXToZ; // on each line, one is 1 and two are 0
    private float negativeYToX, negativeYToY, negativeYToZ;
    private float negativeZToX, negativeZToY, negativeZToZ;
    private float positiveXToX, positiveXToY, positiveXToZ;
    private float positiveYToX, positiveYToY, positiveYToZ;
    private float positiveZToX, positiveZToY, positiveZToZ;

    private void UpdateFields()
    {
        negativeXToX = parameterMatrix.negativeX.x = parameterMatrix.negativeX.x == 0 ? 0 : parameterMatrix.negativeX.x / Mathf.Abs(parameterMatrix.negativeX.x);
        negativeXToY = parameterMatrix.negativeX.y = parameterMatrix.negativeX.y == 0 ? 0 : parameterMatrix.negativeX.y / Mathf.Abs(parameterMatrix.negativeX.y);
        negativeXToZ = parameterMatrix.negativeX.z = parameterMatrix.negativeX.z == 0 ? 0 : parameterMatrix.negativeX.z / Mathf.Abs(parameterMatrix.negativeX.z);
        negativeYToX = parameterMatrix.negativeY.x = parameterMatrix.negativeY.x == 0 ? 0 : parameterMatrix.negativeY.x / Mathf.Abs(parameterMatrix.negativeY.x);
        negativeYToY = parameterMatrix.negativeY.y = parameterMatrix.negativeY.y == 0 ? 0 : parameterMatrix.negativeY.y / Mathf.Abs(parameterMatrix.negativeY.y);
        negativeYToZ = parameterMatrix.negativeY.z = parameterMatrix.negativeY.z == 0 ? 0 : parameterMatrix.negativeY.z / Mathf.Abs(parameterMatrix.negativeY.z);
        negativeZToX = parameterMatrix.negativeZ.x = parameterMatrix.negativeZ.x == 0 ? 0 : parameterMatrix.negativeZ.x / Mathf.Abs(parameterMatrix.negativeZ.x);
        negativeZToY = parameterMatrix.negativeZ.y = parameterMatrix.negativeZ.y == 0 ? 0 : parameterMatrix.negativeZ.y / Mathf.Abs(parameterMatrix.negativeZ.y);
        negativeZToZ = parameterMatrix.negativeZ.z = parameterMatrix.negativeZ.z == 0 ? 0 : parameterMatrix.negativeZ.z / Mathf.Abs(parameterMatrix.negativeZ.z);

        positiveXToX = parameterMatrix.positiveX.x = parameterMatrix.positiveX.x == 0 ? 0 : parameterMatrix.positiveX.x / Mathf.Abs(parameterMatrix.positiveX.x);
        positiveXToY = parameterMatrix.positiveX.y = parameterMatrix.positiveX.y == 0 ? 0 : parameterMatrix.positiveX.y / Mathf.Abs(parameterMatrix.positiveX.y);
        positiveXToZ = parameterMatrix.positiveX.z = parameterMatrix.positiveX.z == 0 ? 0 : parameterMatrix.positiveX.z / Mathf.Abs(parameterMatrix.positiveX.z);
        positiveYToX = parameterMatrix.positiveY.x = parameterMatrix.positiveY.x == 0 ? 0 : parameterMatrix.positiveY.x / Mathf.Abs(parameterMatrix.positiveY.x);
        positiveYToY = parameterMatrix.positiveY.y = parameterMatrix.positiveY.y == 0 ? 0 : parameterMatrix.positiveY.y / Mathf.Abs(parameterMatrix.positiveY.y);
        positiveYToZ = parameterMatrix.positiveY.z = parameterMatrix.positiveY.z == 0 ? 0 : parameterMatrix.positiveY.z / Mathf.Abs(parameterMatrix.positiveY.z);
        positiveZToX = parameterMatrix.positiveZ.x = parameterMatrix.positiveZ.x == 0 ? 0 : parameterMatrix.positiveZ.x / Mathf.Abs(parameterMatrix.positiveZ.x);
        positiveZToY = parameterMatrix.positiveZ.y = parameterMatrix.positiveZ.y == 0 ? 0 : parameterMatrix.positiveZ.y / Mathf.Abs(parameterMatrix.positiveZ.y);
        positiveZToZ = parameterMatrix.positiveZ.z = parameterMatrix.positiveZ.z == 0 ? 0 : parameterMatrix.positiveZ.z / Mathf.Abs(parameterMatrix.positiveZ.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateFields();
    }

    /*public void MoveTarget()
    {
        Vector3 deltaPos = TranslatePosition(player.transform.position - prevPos);
        this.transform.position += deltaPos;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.0f, 1.0f), transform.position.y, Mathf.Clamp(transform.position.z, -1.0f, 1.0f));
        Debug.Log("le mouvement " + (player.transform.position - prevPos) + " a généré " + deltaPos);
        prevPos = player.transform.position;
    }*/ 

    public Vector3 TranslatePosition(Vector3 playerPos)
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


        return matrixIda * playerPos4;
    }
}
