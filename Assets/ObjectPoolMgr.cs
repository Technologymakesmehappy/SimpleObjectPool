using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolMgr : MonoBehaviour
{

	public static ObjectPoolMgr Instance{ get; private set; }

	private Dictionary<string,ObjectPool> poolDic = new Dictionary<string, ObjectPool> ();

	private	ObjectPoolMgr ()
	{
		Instance = this;
	}

	private ObjectPool GetPool (string name)
	{
		string key = name.Contains ("(Clone)") ? name.Substring (0, name.IndexOf ("(Clone)")) : name;
		ObjectPool pool;
		if (poolDic.ContainsKey (key)) {
			pool = poolDic [key];
		} else {
			GameObject go = new GameObject (key);
			go.transform.SetParent (transform);
			pool = go.AddComponent<ObjectPool> ();
			poolDic.Add (key, pool);
		}
		return pool;
	}

	public GameObject AllocObject (GameObject go, Vector3 position, Quaternion rotation)
	{
		ObjectPool pool = GetPool (go.name);
		return pool.Alloc (go, position, rotation);
	}


	public void RecycleObject (GameObject go)
	{
		string name = go.name;
		ObjectPool pool = GetPool (name);
		pool.Recycle (go);
	}
}
