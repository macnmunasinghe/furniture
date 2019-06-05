/// <summary>
/// Created by SWAN DEV
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SDemoAnimation : MonoBehaviour
{
	private static SDemoAnimation _instance = null;
	public static SDemoAnimation Instance
	{
		get{
			if(_instance == null)
			{
				_instance = new GameObject("[SDemoAnimation]").AddComponent<SDemoAnimation>();
			}
			return _instance;
		}
	}

	public enum LoopType
	{
		None = 0,
		Loop,
		PingPong,
	}

	public void Move(GameObject targetGO, Vector3 fromPosition, Vector3 toPosition, float time, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Move(targetGO, fromPosition, toPosition, time, 0f, loop, onComplete));
	}
	public void Move(GameObject targetGO, Vector3 fromPosition, Vector3 toPosition, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Move(targetGO, fromPosition, toPosition, time, delay, loop, onComplete));
	}
	private IEnumerator _Move(GameObject targetGO, Vector3 fromPosition, Vector3 toPosition, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		targetGO.transform.localPosition = fromPosition;
		float elapsedTime = 0;
		while (elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			targetGO.transform.localPosition = Vector3.Lerp(fromPosition, toPosition, (elapsedTime / time));
			yield return new WaitForEndOfFrame();
		}
		targetGO.transform.localPosition = toPosition;
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Move(targetGO, fromPosition, toPosition, time, delay, loop, onComplete));
		else if(loop == LoopType.PingPong) StartCoroutine(_Move(targetGO, toPosition, fromPosition, time, delay, loop, onComplete));
	}

	public void Scale(GameObject targetGO, Vector3 fromScale, Vector3 toScale, float time, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Scale(targetGO, fromScale, toScale, time, 0f, loop, onComplete));
	}
	public void Scale(GameObject targetGO, Vector3 fromScale, Vector3 toScale, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Scale(targetGO, fromScale, toScale, time, delay, loop, onComplete));
	}
	private IEnumerator _Scale(GameObject targetGO, Vector3 fromScale, Vector3 toScale, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		targetGO.transform.localScale = fromScale;
		float elapsedTime = 0;
		while (elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			targetGO.transform.localScale = Vector3.Lerp(fromScale, toScale, (elapsedTime / time));
			yield return new WaitForEndOfFrame();
		}
		targetGO.transform.localScale = toScale;
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Scale(targetGO, fromScale, toScale, time, delay, loop, onComplete));
		else if(loop == LoopType.PingPong) StartCoroutine(_Scale(targetGO, toScale, fromScale, time, delay, loop, onComplete));
	}

	public void Rotate(GameObject targetGO, Vector3 fromEulerAngle, Vector3 toEulerScale, float time, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Rotate(targetGO, fromEulerAngle, toEulerScale, time, 0f, loop, onComplete));
	}
	public void Rotate(GameObject targetGO, Vector3 fromEulerAngle, Vector3 toEulerScale, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Rotate(targetGO, fromEulerAngle, toEulerScale, time, delay, loop, onComplete));
	}
	private IEnumerator _Rotate(GameObject targetGO, Vector3 fromEulerAngle, Vector3 toEulerAngle, float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		targetGO.transform.localEulerAngles = fromEulerAngle;
		float elapsedTime = 0;
		while (elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			targetGO.transform.localEulerAngles = Vector3.Lerp(fromEulerAngle, toEulerAngle, (elapsedTime / time));
			yield return new WaitForEndOfFrame();
		}
		targetGO.transform.localEulerAngles = toEulerAngle;
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Rotate(targetGO, fromEulerAngle, toEulerAngle, time, delay, loop, onComplete));
		else if(loop == LoopType.PingPong) StartCoroutine(_Rotate(targetGO, toEulerAngle, fromEulerAngle, time, delay, loop, onComplete));
	}

	public void FloatTo(float fromValue, float toValue, float time, Action<float> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_FloatTo(fromValue, toValue, time, 0f, onUpdate, loop, onComplete));
	}
	public void FloatTo(float fromValue, float toValue, float time, float delay, Action<float> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_FloatTo(fromValue, toValue, time, delay, onUpdate, loop, onComplete));
	}
	private IEnumerator _FloatTo(float fromValue, float toValue, float time, float delay, Action<float> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		float elapsedTime = 0;
		float val = 0f;
		while (elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			val = Mathf.Lerp(fromValue, toValue, (elapsedTime / time));
			onUpdate(val);
			yield return new WaitForEndOfFrame();
		}
		onUpdate(toValue);
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_FloatTo(fromValue, toValue, time, delay, onUpdate, loop, onComplete));
		else if(loop == LoopType.PingPong) StartCoroutine(_FloatTo(toValue, fromValue, time, delay, onUpdate, loop, onComplete));
	}

	public void Vector2To(Vector2 fromValue, Vector2 toValue, float time, Action<Vector2> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Vector2To(fromValue, toValue, time, 0f, onUpdate, loop, onComplete));
	}
	public void Vector2To(Vector2 fromValue, Vector2 toValue, float time, float delay, Action<Vector2> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Vector2To(fromValue, toValue, time, delay, onUpdate, loop, onComplete));
	}
	private IEnumerator _Vector2To(Vector2 fromValue, Vector2 toValue, float time, float delay, Action<Vector2> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		float elapsedTime = 0;
		Vector2 val = Vector2.zero;
		while (elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			val = Vector2.Lerp(fromValue, toValue, (elapsedTime / time));
			onUpdate(val);
			yield return new WaitForEndOfFrame();
		}
		onUpdate(toValue);
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Vector2To(fromValue, toValue, time, delay, onUpdate, loop, onComplete));
		else if(loop == LoopType.PingPong) StartCoroutine(_Vector2To(toValue, fromValue, time, delay, onUpdate, loop, onComplete));
	}

	public void Vector3To(Vector3 fromValue, Vector3 toValue, float time, Action<Vector3> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Vector3To(fromValue, toValue, time, 0f, onUpdate, loop, onComplete));
	}
	public void Vector3To(Vector3 fromValue, Vector3 toValue, float time, float delay, Action<Vector3> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Vector3To(fromValue, toValue, time, delay, onUpdate, loop, onComplete));
	}
	private IEnumerator _Vector3To(Vector3 fromValue, Vector3 toValue, float time, float delay, Action<Vector3> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		float elapsedTime = 0;
		Vector3 val = Vector3.zero;
		while (elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			val = Vector3.Lerp(fromValue, toValue, (elapsedTime / time));
			onUpdate(val);
			yield return new WaitForEndOfFrame();
		}
		onUpdate(toValue);
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Vector3To(fromValue, toValue, time, delay, onUpdate, loop, onComplete));
		else if(loop == LoopType.PingPong) StartCoroutine(_Vector3To(toValue, fromValue, time, delay, onUpdate, loop, onComplete));
	}

	public void Vector4To(Vector4 fromValue, Vector4 toValue, float time, Action<Vector4> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Vector4To(fromValue, toValue, time, 0f, onUpdate, loop, onComplete));
	}
	public void Vector4To(Vector4 fromValue, Vector4 toValue, float time, float delay, Action<Vector4> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Vector4To(fromValue, toValue, time, delay, onUpdate, loop, onComplete));
	}
	private IEnumerator _Vector4To(Vector4 fromValue, Vector4 toValue, float time, float delay, Action<Vector4> onUpdate, LoopType loop = LoopType.None, Action onComplete = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		float elapsedTime = 0;
		Vector4 val = Vector4.zero;
		while (elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			val = Vector4.Lerp(fromValue, toValue, (elapsedTime / time));
			onUpdate(val);
			yield return new WaitForEndOfFrame();
		}
		onUpdate(toValue);
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Vector4To(fromValue, toValue, time, delay, onUpdate, loop, onComplete));
		else if(loop == LoopType.PingPong) StartCoroutine(_Vector4To(toValue, fromValue, time, delay, onUpdate, loop, onComplete));
	}

	public void Wait(float time, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Wait(time, 0f, loop, onComplete));
	}
	public void Wait(float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		StartCoroutine(_Wait(time, delay, loop, onComplete));
	}
	private IEnumerator _Wait(float time, float delay, LoopType loop = LoopType.None, Action onComplete = null)
	{
		if(delay > 0) yield return new WaitForSeconds(delay);

		yield return new WaitForSeconds(time);
		if(onComplete != null) onComplete(); 

		if(loop == LoopType.Loop) StartCoroutine(_Wait(time, delay, loop, onComplete));
		else if(loop == LoopType.PingPong) StartCoroutine(_Wait(time, delay, loop, onComplete));
	}
}