using UnityEngine;
using System.Collections;


[CreateAssetMenu(fileName = "KinnectParam", menuName = "Reminescence/Kinnectparam",order = 1 )]
public class KinnectParameter : ScriptableObject
{
	public float kinnectScalePosition = 10;
	public Vector3 kinnectOffset;
	public bool overrideKinnect;
}
