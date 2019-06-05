/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SelfAnimation : MonoBehaviour
{
	public enum SelfAnimType{
		None = 0,
		Move,
		Rotate,
		Scale,
	}
	public SelfAnimType m_SelfAnimType = SelfAnimType.None;

	public Vector3 initValue;
	public Vector3 fromValue;
	public Vector3 toValue;
	public float time = 0.5f;
	public float delay = 0f;
	public SDemoAnimation.LoopType loop = SDemoAnimation.LoopType.None;
	public bool destroyOnComplete = false;
	public bool executeAtStart = true;
	public bool enableInitValue = false;
	public UnityEvent onComplete;

	// Use this for initialization
	void Start()
	{
		if(!executeAtStart) return;
		StartAnimation();
	}

	void OnComplete()
	{
		if(onComplete != null) onComplete.Invoke();
		if(destroyOnComplete) GameObject.Destroy(gameObject);
	}


	public void StartAnimation()
	{
		switch(m_SelfAnimType)
		{
		case SelfAnimType.Move:
			if(enableInitValue) gameObject.transform.localPosition = initValue;
			SDemoAnimation.Instance.Move(gameObject, fromValue, toValue, time, delay, loop, OnComplete);
			break;

		case SelfAnimType.Rotate:
			if(enableInitValue) gameObject.transform.localEulerAngles = initValue;
			SDemoAnimation.Instance.Rotate(gameObject, fromValue, toValue, time, delay, loop, OnComplete);
			break;

		case SelfAnimType.Scale:
			if(enableInitValue) gameObject.transform.localScale = initValue;
			SDemoAnimation.Instance.Scale(gameObject, fromValue, toValue, time, delay, loop, OnComplete);
			break;
		}
	}

}
