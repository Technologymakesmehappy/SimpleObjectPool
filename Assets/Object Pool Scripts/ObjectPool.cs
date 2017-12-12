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
			goTemp = Instantiate (go, position, rotation) as GameObject;
		}
		goTemp.SetActive (true);
		return goTemp;
	}

	//有父对象,直接使用父对象位置
	public virtual GameObject Alloc (GameObject go, Transform parent)
	{
		GameObject goTemp;
		if (queue.Count > 0) {//获取已存在的对象
			goTemp = queue.Dequeue () as GameObject;
			goTemp.transform.SetParent (parent, true);
			goTemp.transform.localPosition = Vector3.zero;
			goTemp.transform.localRotation = Quaternion.identity;
		} else {//创建新的对象
			goTemp = Instantiate (go, parent, false) as GameObject;
		}
		goTemp.SetActive (true);
		return goTemp;
	}

	//有父对象,使用世界坐标
	public virtual GameObject Alloc (GameObject go, Vector3 position, Quaternion rotation, Transform parent)
	{
		GameObject goTemp;
		if (queue.Count > 0) {//获取已存在的对象
			goTemp = queue.Dequeue () as GameObject;
			goTemp.transform.position = position;
			goTemp.transform.rotation = rotation;
			goTemp.transform.SetParent (parent, true);
		} else {//创建新的对象
			goTemp = Instantiate (go, position, rotation, parent) as GameObject;
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
		StartCoroutine (RecycleCoroutine (go, 0f));
	}

	public virtual void Recycle (GameObject go, float time)
	{
		StartCoroutine (RecycleCoroutine (go, time));
	}

	IEnumerator RecycleCoroutine (GameObject go, float time)
	{
		if (go.GetComponent<RecyclableObject> ()) {
			go.GetComponent<RecyclableObject> ().recyclable = true;
		}
		yield return new WaitForSeconds (time);
		if (go && go.GetComponent<RecyclableObject> ().recyclable) {//避免重复操作,把空对象压到queue里。判断一下这个对象是否已经被回收过了
			go.GetComponent<RecyclableObject> ().recyclable = false;
			queue.Enqueue (go);
			go.transform.SetParent (transform);
			go.SetActive (false);
		}
	}

}