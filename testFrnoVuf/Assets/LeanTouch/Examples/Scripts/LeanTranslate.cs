using UnityEngine;

namespace Lean.Touch
{
	// This script allows you to transform the current GameObject
	public class LeanTranslate : MonoBehaviour
	{
		[Tooltip("Ignore fingers with StartedOverGui?")]
		public bool IgnoreGuiFingers = true;

		[Tooltip("Ignore fingers if the finger count doesn't match? (0 = any)")]
		public int RequiredFingerCount;

		[Tooltip("Does translation require an object to be selected?")]
		public LeanSelectable RequiredSelectable;

		[Tooltip("The camera the translation will be calculated using (None = MainCamera)")]
		public Camera Camera;

		public Vector3 initialpos;

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			Start();
		}
#endif

		protected virtual void Start()
		{
			initialpos =new Vector3(0,this.gameObject.GetComponentInParent<Transform>().position.y,0);
			if (RequiredSelectable == null)
			{
				RequiredSelectable = GetComponent<LeanSelectable>();
			}
		}

		protected virtual void Update()
		{


			initialpos =new Vector3(0,this.gameObject.GetComponentInParent<Transform>().position.y,0);
			// If we require a selectable and it isn't selected, cancel translation
			if (RequiredSelectable != null && RequiredSelectable.IsSelected == false)
			{
				return;
			}

			// Get the fingers we want to use
			var fingers = LeanTouch.GetFingers(IgnoreGuiFingers, RequiredFingerCount, RequiredSelectable);

			// Calculate the screenDelta value based on these fingers
			var screenDelta = LeanGesture.GetScreenDelta(fingers);

			// Perform the translation
			Translate(screenDelta);
		}

		protected virtual void Translate(Vector2 screenDelta)
		{
			// Make sure the camera exists
			var camera = LeanTouch.GetCamera(Camera, gameObject);

			if (camera != null)
			{
				// Screen position of the transform
				var screenPosition = camera.WorldToScreenPoint(transform.position);
				Vector3 jj = new Vector3 (screenDelta.x*0.8f, 0 ,screenDelta.y*0.05f);
				// Add the deltaPosition
//				screenPosition += (Vector3)screenDelta;//
				screenPosition +=jj;
				Debug.Log ("x "+screenPosition.x +"and y " +screenPosition.y);
				// Convert back to world space
				Vector3 pos = camera.ScreenToWorldPoint(screenPosition);
				Debug.Log ("converted x "+pos.x +"converted and y " +pos.z);
//				transform.position = camera.ScreenToWorldPoint(screenPosition);
//				Vector3 pp = new Vector3(0,0,transform.position.z+pos.y);
				transform.position = new Vector3(Mathf.Clamp(pos.x,-100f,100f),initialpos.y,Mathf.Clamp(pos.z,-100f,100f));//Mathf.Clamp(pos.z,1f,75f));//pos.z,pos.x
			}
		}
	}
}