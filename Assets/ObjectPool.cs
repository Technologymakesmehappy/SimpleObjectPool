using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	//保存pool中的对象
	private Queue queue = new Queue ();

	/// <summary>
	/// 从pool中取出所需的对象，若pool中对象不足，就创建新的
	/// </summary>
	/// <param name="go">Go.</param>
	/// <param name="position">Position.</param>
	/// <param name="rotation">Rotation.</param>
	public virtual GameObject Alloc (GameObject go, Vector3 position, Quaternion rotation)
	{
		GameObject goTemp;
		if (queue.Count > 0) {//获取已存在的对象
			goTemp = queue.Dequeue () as GameObject;
			goTemp.transform.position = position;
			goTemp.transform.rotation = rotation;
		} else {//创建新的对象
			goTemp = Instantiate<GameObject> (go, position, rotation);
		}
		goTemp.SetActive (true);
		return goTemp;
	}

	/// <summary>
	/// 回首对象到pool
	/// </summary>
	/// <param name="go">Go.</param>
	public virtual void Recycle (GameObject go)
	{
		queue.Enqueue (go);
		go.transform.SetParent (transform);
		go.SetActive (false);
	}

}
