using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclableCube : RecyclableObject
{
	public override void SetObjectStatus (GameObject go)
	{
		gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
//		throw new System.NotImplementedException ();
	}

}
