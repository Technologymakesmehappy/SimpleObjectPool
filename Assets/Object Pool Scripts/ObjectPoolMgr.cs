using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolMgr : MonoBehaviour
{
	public static ObjectPoolMgr Instance{ get; private set; }

	//保存不同对象的pool
	private Dictionary<string,ObjectPool> poolDic = new Dictionary<string, ObjectPool> ();

	private	ObjectPoolMgr ()
	{
		Instance = this;
	}

	/// <summary>
	/// 获取制定对象的pool，若不存在就创建新pool
	/// </summary>
	/// <returns>The pool.</returns>
	/// <param name="name">Name.</param>
	private ObjectPool GetPool (string name)
	{
		//pool的key名，使用预设的名称
		string key = name.Contains ("(Clone)") ? name.Substring (0, name.IndexOf ("(Clone)")) : name;
		ObjectPool pool;
		if (poolDic.ContainsKey (key)) {//获取
			pool = poolDic [key];
		} else {//创建
			GameObject go = new GameObject (key);
			go.transform.SetParent (transform);
			pool = go.AddComponent<ObjectPool> ();
			poolDic.Add (key, pool);
		}
		return pool;
	}

	/// <summary>
	/// 无父对象，世界坐标
	/// </summary>
	/// <returns>The object.</returns>
	/// <param name="go">Go.</param>
	/// <param name="position">Position.</param>
	/// <param name="rotation">Rotation.</param>
	public GameObject AllocObject (GameObject go, Vector3 position, Quaternion rotation)
	{
		ObjectPool pool = GetPool (go.name);
		return pool.Alloc (go, position, rotation);
	}

	/// <summary>
	/// 有父对象，父对象坐标
	/// </summary>
	/// <returns>The object.</returns>
	/// <param name="go">Go.</param>
	/// <param name="parent">Parent.</param>
	/// <param name="worldPositionStays">If set to <c>true</c> world position stays.</param>
	public GameObject AllocObject (GameObject go, Transform parent)
	{
		ObjectPool pool = GetPool (go.name);
		return pool.Alloc (go, parent);
	}

	/// <summary>
	/// 有父对象，世界坐标
	/// </summary>
	/// <returns>The object.</returns>
	/// <param name="go">Go.</param>
	/// <param name="position">Position.</param>
	/// <param name="rotation">Rotation.</param>
	/// <param name="parent">Parent.</param>
	public GameObject AllocObject (GameObject go, Vector3 position, Quaternion rotation, Transform parent)
	{
		ObjectPool pool = GetPool (go.name);
		return pool.Alloc (go, position, rotation, parent);
	}

	/// <summary>
	/// 立即回收对象
	/// </summary>
	/// <param name="go">Go.</param>
	public void RecycleObjectImmediately (GameObject go)
	{
		ObjectPool pool = GetPool (go.name);
		pool.Recycle (go);
	}

	/// <summary>
	/// 延迟回收对象
	/// </summary>
	/// <param name="go">Go.</param>
	/// <param name="time">Time.</param>
	public void RecycleObjectInTime (GameObject go, float time)
	{
		ObjectPool pool = GetPool (go.name);
		pool.Recycle (go, time);
	}
}