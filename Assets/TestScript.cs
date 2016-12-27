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
		if (Input.GetMouseButton (0)) {
			GameObject go = ObjectPoolMgr.Instance.AllocObject (cubePrefab, Vector3.zero, Quaternion.identity);
		}
	}
}
