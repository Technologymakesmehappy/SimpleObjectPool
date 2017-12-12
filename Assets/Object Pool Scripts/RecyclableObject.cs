using UnityEngine;
using System.Collections;

//用于回收再调用时恢复对象初始状态
public abstract class RecyclableObject : MonoBehaviour
{
	//延迟回收的标记
	public bool recyclable = false;

	public abstract void SetObjectStatus (GameObject go);

}