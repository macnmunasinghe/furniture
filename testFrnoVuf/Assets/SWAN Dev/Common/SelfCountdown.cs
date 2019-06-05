/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SelfCountdown : MonoBehaviour
{
	public float time = 0.5f;
	public SDemoAnimation.LoopType loop = SDemoAnimation.LoopType.None;
	public bool destroyOnComplete = false;

	public UnityEvent onComplete;

	// Use this for initialization
	void Start () {
		SDemoAnimation.Instance.Wait(time, loop, OnComplete);
	}

	void OnComplete()
	{
		if(onComplete != null) onComplete.Invoke();
		if(destroyOnComplete) GameObject.Destroy(gameObject);
	}
}
