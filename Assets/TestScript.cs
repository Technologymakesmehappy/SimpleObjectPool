using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
	public GameObject cubePrefab;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
//			print ("111");
//			GameObject.Instantiate (cubePrefab, Vector3.zero, Quaternion.identity);
			GameObject go = ObjectPoolMgr.Instance.AllocObject (cubePrefab, Vector3.zero, Quaternion.identity);
		}
	}
}
