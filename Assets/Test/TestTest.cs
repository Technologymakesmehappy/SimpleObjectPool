using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
	public GameObject cubePrefab;
	[Header ("是否恢复回收对象的初始状态")]
	public bool SetDefault;

	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			GameObject go = ObjectPoolMgr.Instance.AllocObject (cubePrefab, Vector3.zero, Quaternion.identity);

			if (SetDefault) {//是否把对象恢复初始状态，可以看出两种效果上的区别
				if (go.GetComponent<RecyclableObject> ()) {
					go.GetComponent<RecyclableObject> ().SetObjectStatus (go);
				}
			}
		}
	}

	void OnTriggerExit (Collider col)
	{
		ObjectPoolMgr.Instance.RecycleObjectImmediately (col.gameObject);
	}
}
