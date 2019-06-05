/// <summary>
/// By SwanDEV 2017
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ScreenshotHelper : MonoBehaviour
{
	public UnityEvent m_MainOnCaptured;
	public Vector2 m_CaptureSize = new Vector2(512, 512);

	private bool _isOnCapture = false;
	private Texture2D _texture2D = null;
	private RenderTexture _renderTexture = null;
	public Text m_DebugText;

	private static ScreenshotHelper _instance = null;
	public static ScreenshotHelper Instance
	{
		get{
			if(_instance == null)
			{
				_instance = new GameObject("[ScreenshotHelper]").AddComponent<ScreenshotHelper>();
			}
			return _instance;
		}
	}

	/// <summary>
	/// Clear this instance: 
	/// Destroy stored textures, remove callbacks, un-register cameras, remove this script from this gameObject.
	/// </summary>
	public void Clear()
	{
		if(_texture2D != null)
		{
			Texture2D.Destroy(_texture2D);
		}

		if(_renderTexture != null)
		{
			RenderTexture.Destroy(_renderTexture);
		}

		if(m_MainOnCaptured != null)
		{
			m_MainOnCaptured.RemoveAllListeners();
			m_MainOnCaptured = null;
		}

		UnRegisterAllRenderCameras();

		//Destroy(this);
	}

	private void Awake()
	{
		if(_instance == null) _instance = this;
	}

	private void _InitMainOnCaptured()
	{
		if(m_MainOnCaptured == null) m_MainOnCaptured = new UnityEvent();
		m_MainOnCaptured.RemoveAllListeners();
	}

	/// <summary>
	/// Set the main onCaptured callback for receiving all images from all capturing methods.
	/// </summary>
	/// <param name="mainOnCaptured">The callback to be fired at each capture.</param>
	public void SetMainOnCapturedCallback(Action mainOnCaptured)
	{
		_InitMainOnCaptured();
		m_MainOnCaptured.AddListener(delegate {
			mainOnCaptured();
		});
	}

	/// <summary>
	/// Set the main onCaptured callback for receiving all images from all capturing methods. Return the captured images as Texture2D.
	/// </summary>
	/// <param name="mainOnCaptured">The callback to be fired at each capture, return a Texture2D.</param>
	public void SetMainOnCapturedCallback(Action<Texture2D> mainOnCaptured)
	{
		_InitMainOnCaptured();
		m_MainOnCaptured.AddListener(delegate {
			mainOnCaptured(_texture2D);
		});
	}

	/// <summary>
	/// Set the main onCaptured callback for receiving all images from all capturing methods. Return the captured images as Sprite.
	/// </summary>
	/// <param name="mainOnCaptured">The callback to be fired at each capture, return a Sprite.</param>
	public void SetMainOnCapturedCallback(Action<Sprite> mainOnCaptured)
	{
		_InitMainOnCaptured();
		m_MainOnCaptured.AddListener(delegate {
			mainOnCaptured(GetCurrentSprite());
		});
	}

	/// <summary>
	/// Set the main onCaptured callback for receiving all images from all capturing methods. Return the captured images as RenderTexture.
	/// </summary>
	/// <param name="mainOnCaptured">The callback to be fired at each capture, return a RenderTexture.</param>
	public void SetMainOnCapturedCallback(Action<RenderTexture> mainOnCaptured)
	{
		_InitMainOnCaptured();
		m_MainOnCaptured.AddListener(delegate {
			mainOnCaptured(_renderTexture);
		});
	}

	/// <summary>
	/// Capture the whole screen.
	/// </summary>
	/// <param name="onCaptured">On captured.</param>
	public void CaptureScreen(Action<Texture2D> onCaptured = null)
	{
		Capture(new Vector2(Screen.width/2, Screen.height/2), new Vector2(Screen.width, Screen.height), onCaptured);
	}

	/// <summary>
	/// Capture a portion of the screen at a specific screen position.
	/// </summary>
	/// <param name="screenPosition">Screen position.</param>
	/// <param name="imageSize">The maximum image size for the screenshot.</param>
	/// <param name="onCaptured">On captured action, return the captured texture2D</param>
	public void Capture(Vector2 screenPosition, Vector2 imageSize, Action<Texture2D> onCaptured = null)
	{
		if(_isOnCapture){
			Debug.LogWarning("Screenshot being captured, please wait for at least 1 frame for starting another capture!");
			return;
		}
		_isOnCapture = true;

		//size correction
		if(imageSize.x > Screen.width) imageSize = new Vector2(Screen.width, imageSize.y);
		if(imageSize.y > Screen.height) imageSize = new Vector2(imageSize.x, Screen.height);

		//position correction
		if(screenPosition.x + imageSize.x/2 > Screen.width) 
			screenPosition = new Vector2(screenPosition.x - (screenPosition.x + imageSize.x/2 - Screen.width), screenPosition.y);
		if(screenPosition.x - imageSize.x/2 < 0)
			screenPosition = new Vector2(screenPosition.x + (imageSize.x/2 - screenPosition.x), screenPosition.y);
		if(screenPosition.y + imageSize.y/2 > Screen.height) 
			screenPosition = new Vector2(screenPosition.x, screenPosition.y - (screenPosition.y + imageSize.y/2 - Screen.height));
		if(screenPosition.y - imageSize.y/2 < 0)
			screenPosition = new Vector2(screenPosition.x, screenPosition.y + (imageSize.y/2 - screenPosition.y));

		UpdateDebugText("Capture screenPosition: " + screenPosition + " | imageSize: " + imageSize);
		Rect rect = new Rect(screenPosition, imageSize);
		StartCoroutine(_ReadPixelWithRect(rect, onCaptured));
	}

	/// <summary>
	/// Capture image with the view of the target camera. Return a Texture2D.
	/// </summary>
	/// <param name="camera">Target Camera.</param>
	/// <param name="onCaptured">On captured action, return the captured Texture2D</param>
	public void CaptureWithCamera(Camera camera, Action<Texture2D> onCaptured = null)
	{
		UpdateDebugText(camera.name + " rect: " + camera.pixelWidth + " x " + camera.pixelHeight);

		RegisterRenderCamera(camera);
		CameraOnRender camOnRender = camera.gameObject.GetComponent<CameraOnRender>();

		if(camOnRender != null)
		{
			camOnRender.SetOnCaptureCallback((Texture2D tex)=>{
				_texture2D = tex;
				if(m_MainOnCaptured != null) m_MainOnCaptured.Invoke();
				if(onCaptured != null) onCaptured(_texture2D);
			});
		}
		else
		{
			Debug.LogWarning("Require this camera to be registered with method RegisterCaptureCamera!");
		}
	}

	/// <summary>
	/// Capture image with the view of the target camera. Return a RenderTexture.
	/// </summary>
	/// <param name="camera">Target Camera.</param>
	/// <param name="onCaptured">On captured action, return the captured RenderTexture</param>
	public void CaptureRenderTextureWithCamera(Camera camera, Action<RenderTexture> onCaptured = null)
	{
		UpdateDebugText(camera.name + " rect: " + camera.pixelWidth + " x " + camera.pixelHeight);

		RegisterRenderCamera(camera);
		CameraOnRender camOnRender = camera.gameObject.GetComponent<CameraOnRender>();

		if(camOnRender != null)
		{
			camOnRender.SetOnCaptureCallback((RenderTexture rTex)=>{
				_renderTexture = rTex;
				if(m_MainOnCaptured != null) m_MainOnCaptured.Invoke();
				if(onCaptured != null) onCaptured(_renderTexture);
			});
		}
		else
		{
			Debug.LogWarning("Require this camera to be registered with method RegisterCaptureCamera!");
		}
	}

	/// <summary>
	/// Get the currently stored Texture2D (since the last capture).
	/// If you did not take any screenshot before, this will return a null.
	/// </summary>
	/// <returns>The currently stored Texture2D.</returns>
	public Texture2D GetCurrentTexture()
	{
		return _texture2D;
	}

	/// <summary>
	/// Return the sprite that converts from the current stored texture2D
	/// If you did not take any screenshot before, this will return a null.
	/// </summary>
	/// <returns>The sprite converts from _texture2D.</returns>
	public Sprite GetCurrentSprite()
	{
		return ToSprite(GetCurrentTexture());
	}

	/// <summary>
	/// Get the currently stored RenderTexture (since the last capture, that captured by Camera only).
	/// If you did not take any screenshot before, this will return a null.
	/// </summary>
	/// <returns>The currently stored RenderTexture.</returns>
	public RenderTexture GetCurrentRenderTexture()
	{
		return _renderTexture;
	}

	private IEnumerator _ReadPixelWithRect(Rect targetRect, Action<Texture2D> onCaptured)
	{
		//ensure to Read Pixels inside drawing frame
		yield return new WaitForEndOfFrame();

		//size correction for target rect
		if(targetRect.width > Screen.width) targetRect.width = Screen.width;
		if(targetRect.height > Screen.height) targetRect.height = Screen.height;

		_texture2D = new Texture2D((int)targetRect.width, (int)targetRect.height, TextureFormat.RGB24, false);
		Rect rect = new Rect(targetRect.position.x-targetRect.width/2, targetRect.position.y-targetRect.height/2, targetRect.width, targetRect.height);
		_texture2D.ReadPixels(rect, 0, 0);
		_texture2D.Apply();
		if(m_MainOnCaptured != null) m_MainOnCaptured.Invoke();
		if(onCaptured != null) onCaptured(_texture2D);

		_isOnCapture = false;
	}

	/// <summary>
	/// Attach a CameraOnRender script on the camera to capture image from camera.
	/// </summary>
	/// <param name="camera">Target Camera.</param>
	public void RegisterRenderCamera(Camera camera)
	{
		if(camera != null && camera.gameObject.GetComponent<CameraOnRender>() == null)
		{
			camera.gameObject.AddComponent<CameraOnRender>();
		}
	}

	/// <summary>
	/// Clear the instance of CameraOnRender and remove the script.
	/// </summary>
	/// <param name="camera">Target Camera.</param>
	public void UnRegisterRenderCamera(Camera camera)
	{
		if(camera != null && camera.gameObject.GetComponent<CameraOnRender>() != null)
		{
			camera.gameObject.GetComponent<CameraOnRender>().Clear();
		}
	}

	/// <summary>
	/// Clear the instance of CameraOnRender on all cameras, and remove the script.
	/// </summary>
	public void UnRegisterAllRenderCameras()
	{
		Camera[] cameras = Camera.allCameras;
		if(cameras != null)
		{
			foreach(Camera cam in cameras)
			{
				UnRegisterRenderCamera(cam);
			}
		}
	}


	#region ----- Static Methods -----
	/// <summary>
	/// Get the currently stored Texture2D (since the last capture).
	/// If you did not take any screenshot before, this will return a null.
	/// </summary>
	/// <returns>The currently stored Texture2D.</returns>
	public static Texture2D CurrentTexture
	{
		get{
			return Instance.GetCurrentTexture();
		}
	}

	/// <summary>
	/// Return the sprite that converts from the current texture2D
	/// If you did not take any screenshot before, this will return a null.
	/// </summary>
	/// <returns>The sprite converts from _texture2D.</returns>
	public static Sprite CurrentSprite
	{
		get{
			return Instance.GetCurrentSprite();
		}
	}

	/// <summary>
	/// Get the currently stored RenderTexture (since the last capture, that captured by Camera only).
	/// If you did not take any screenshot before, this will return a null.
	/// </summary>
	/// <returns>The currently stored RenderTexture.</returns>
	public static RenderTexture CurrentRenderTexture
	{
		get{
			return Instance.GetCurrentRenderTexture();
		}
	}

	public static Vector2 CurrentCaptureSize
	{
		get{
			return Instance.m_CaptureSize;
		}
	}

	public static void iSetCaptureSize(Vector2 captureSize)
	{
		Instance.m_CaptureSize = captureSize;
	}

	/// <summary>
	/// Set the main onCaptured callback for receiving all images from all capturing methods.
	/// </summary>
	/// <param name="mainOnCaptured">The callback to be fired at each capture.</param>
	public static void iSetMainOnCapturedCallback(Action mainOnCaptured)
	{
		Instance.SetMainOnCapturedCallback(mainOnCaptured);
	}

	/// <summary>
	/// Set the main onCaptured callback for receiving all images from all capturing methods. Return the captured images as Texture2D.
	/// </summary>
	/// <param name="mainOnCaptured">The callback to be fired at each capture, return a Texture2D.</param>
	public static void iSetMainOnCapturedCallback(Action<Texture2D> mainOnCaptured)
	{
		Instance.SetMainOnCapturedCallback(mainOnCaptured);
	}

	/// <summary>
	/// Set the main onCaptured callback for receiving all images from all capturing methods. Return the captured images as Sprite.
	/// </summary>
	/// <param name="mainOnCaptured">The callback to be fired at each capture, return a Sprite.</param>
	public static void iSetMainOnCapturedCallback(Action<Sprite> mainOnCaptured)
	{
		Instance.SetMainOnCapturedCallback(mainOnCaptured);
	}

	/// <summary>
	/// Set the main onCaptured callback for receiving all images from all capturing methods. Return the captured images as RenderTexture.
	/// </summary>
	/// <param name="mainOnCaptured">The callback to be fired at each capture, return a RenderTexture.</param>
	public static void iSetMainOnCapturedCallback(Action<RenderTexture> mainOnCaptured)
	{
		Instance.SetMainOnCapturedCallback(mainOnCaptured);
	}

	/// <summary>
	/// Capture the whole screen.
	/// </summary>
	/// <param name="onCaptured">On captured.</param>
	public static void iCaptureScreen(Action<Texture2D> onCaptured = null)
	{
		Instance.CaptureScreen(onCaptured);
	}

	/// <summary>
	/// Capture a portion of the screen at a specific screen position.
	/// </summary>
	/// <param name="screenPosition">Screen position.</param>
	/// <param name="imageSize">The maximum image size for the screenshot.</param>
	/// <param name="onCaptured">On captured action, return the captured texture2D</param>
	public static void iCapture(Vector2 screenPosition, Vector2 imageSize, Action<Texture2D> onCaptured = null)
	{
		Instance.Capture(screenPosition, imageSize, onCaptured);
	}

	/// <summary>
	/// Capture image with the view of the target camera. Return a Texture2D.
	/// </summary>
	/// <param name="camera">Target Camera.</param>
	/// <param name="onCaptured">On captured action, return the captured Texture2D</param>
	public static void iCaptureWithCamera(Camera camera, Action<Texture2D> onCaptured = null)
	{
		Instance.CaptureWithCamera(camera, onCaptured);
	}

	/// <summary>
	/// Capture image with the view of the target camera. Return a RenderTexture.
	/// </summary>
	/// <param name="camera">Target Camera.</param>
	/// <param name="onCaptured">On captured action, return the captured RenderTexture</param>
	public static void iCaptureRenderTextureWithCamera(Camera camera, Action<RenderTexture> onCaptured = null)
	{
		Instance.CaptureRenderTextureWithCamera(camera, onCaptured);
	}

	/// <summary>
	/// Attach a CameraOnRender script on the camera to capture image from camera.
	/// </summary>
	/// <param name="camera">Target Camera.</param>
	public static void iRegisterRenderCamera(Camera camera)
	{
		Instance.RegisterRenderCamera(camera);
	}

	/// <summary>
	/// Clear the instance of CameraOnRender and remove the script.
	/// </summary>
	/// <param name="camera">Target Camera.</param>
	public static void iUnRegisterRenderCamera(Camera camera)
	{
		Instance.UnRegisterRenderCamera(camera);
	}

	/// <summary>
	/// Clear the instance of CameraOnRender on all cameras, and remove the script.
	/// </summary>
	public static void iUnRegisterAllRenderCameras()
	{
		Instance.UnRegisterAllRenderCameras();
	}

	/// <summary>
	/// Clear the instance of ScreenshotHelper: 
	/// Destroy stored textures, remove callbacks, un-register cameras, remove its gameObject.
	/// </summary>
	public static void iClear()
	{
		Instance.Clear();
	}
	#endregion


	#region ----- Others -----
	public void UpdateDebugText(string text)
	{
		if(m_DebugText != null)
		{
			Debug.Log(text);
			m_DebugText.text = text;
		}
	}

	public static Sprite ToSprite(Texture2D texture)
	{
		if(texture == null) return null;
			
		Vector2 pivot = new Vector2(0.5f, 0.5f);
		float pixelPerUnit = 100;
		return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), pivot, pixelPerUnit);
	}
	#endregion
}
