using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

	private Queue queue = new Queue ();



	public virtual GameObject Alloc (GameObject go, Vector3 position, Quaternion rotation)
	{
		GameObject goTemp;
		if (queue.Count > 0) {
			goTemp = queue.Dequeue () as GameObject;
			goTemp.transform.position = position;
			goTemp.transform.rotation = rotation;
		} else {
			goTemp = Instantiate<GameObject> (go, position, rotation);
		}
		goTemp.SetActive (true);
		return goTemp;
	}


	public virtual void Recycle (GameObject go)
	{
		queue.Enqueue (go);
		go.transform.SetParent (transform);
		go.SetActive (false);
	}

}
