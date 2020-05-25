using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Robot/TranspositionMatrix")]
public class SOMatrix : ScriptableObject
{
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
}
