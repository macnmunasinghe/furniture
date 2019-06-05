/// <summary>
/// By SwanDEV 2017
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Screenshot Helper UGUI example.
/// </summary>
public class ScreenshotDemo : MonoBehaviour
{
	[Header("[ Save & Display Settings ]")]
	public FilePathName.SaveFormat saveFormat = FilePathName.SaveFormat.JPG;
	public bool fitCanvasSize = true;
	public float scaleFactor = 1f;

	[Header("[ Object References ]")]
	public CanvasScaler canvasScaler;
	public Image displayImage;
	public Text debugText;
	public InputField widthInputField;
	public InputField heightInputField;
	public MeshRenderer cubeMesh;

	public Camera camera1;
	public Camera camera2;
	public Camera camera3;

	private void Start()
	{
		ScreenshotHelper.iSetMainOnCapturedCallback((Sprite sprite)=>{
			SetImage(sprite);
			cubeMesh.material.mainTexture = (Texture)sprite.texture;

			switch(saveFormat)
			{
			case FilePathName.SaveFormat.JPG:
				SaveAsJPG(sprite.texture);
				break;
			case FilePathName.SaveFormat.PNG:
				SaveAsPNG(sprite.texture);
				break;
			case FilePathName.SaveFormat.GIF:
				//Require Pro GIF to save image(s) as GIF.
				//SaveAsGIF(sprite.texture);
				break;
			}
		});
		ScreenshotHelper.Instance.m_DebugText = debugText;

		OnInputChanges();

		//Check screen orientation for setting canvas resolution
		if(Screen.width > Screen.height)
		{
			canvasScaler.referenceResolution = new Vector2(1920, 1080);
		}
		else
		{
			canvasScaler.referenceResolution = new Vector2(1080, 1920);
		}
	}

	private PointerEventData uiPointerEventData = new PointerEventData(EventSystem.current);
	private List<RaycastResult> uiRaycastResuls = new List<RaycastResult>();
	private bool _isPointedOnUI = false;
	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			uiPointerEventData.position = Input.mousePosition;
			EventSystem.current.RaycastAll(uiPointerEventData, uiRaycastResuls);
			_isPointedOnUI = (uiRaycastResuls.Count > 0)? true:false;
		}

		if(Input.GetMouseButtonUp(0))
		{
			if(!_isPointedOnUI)
			{
				ScreenshotHelper.Instance.UpdateDebugText("Touch Position: " + Input.mousePosition);
				ScreenshotHelper.iCapture(Input.mousePosition, ScreenshotHelper.CurrentCaptureSize, null);
			}
		}
	}

	public void OnInputChanges()
	{
		int captureWidth = 512;
		int.TryParse(widthInputField.text, out captureWidth);

		int captureHeight = 512;
		int.TryParse(heightInputField.text, out captureHeight);

		ScreenshotHelper.iSetCaptureSize( new Vector2(captureWidth, captureHeight) );
	}

	public void CaptureScreen()
	{
		ScreenshotHelper.iCaptureScreen();
	}

	public void CaptureWithCamera(Camera camera)
	{
		ScreenshotHelper.iCaptureWithCamera(camera, null);
	}

	private void SetImage(Sprite sprite)
	{
		_ClearTexture();
		displayImage.sprite = sprite;

		if(!fitCanvasSize)
		{
			displayImage.SetNativeSize();
		}
		else
		{
			float newWidth = sprite.texture.width;
			float newHeight = sprite.texture.height;

			Vector2 canvasResolution = canvasScaler.GetComponent<RectTransform>().sizeDelta;
			Vector2 imageSize = new Vector2(newWidth, newHeight);

			//Set the sizeDelta for displayImage to fit the UGUI canvas resolution
			for(int i=0; i<2; i++)
			{
				if(imageSize.x < canvasResolution.x)
				{
					newWidth = canvasResolution.x;
					newHeight = imageSize.y * (canvasResolution.x / imageSize.x);
				}
				if(imageSize.y < canvasResolution.y)
				{
					newWidth = imageSize.x * (canvasResolution.y / imageSize.y);
					newHeight = canvasResolution.y;
				}

				if(imageSize.x > canvasResolution.x)
				{
					newWidth = canvasResolution.x;
					newHeight = imageSize.y * (canvasResolution.x / imageSize.x);
				}
				if(imageSize.y > canvasResolution.y)
				{
					newWidth = imageSize.x * (canvasResolution.y / imageSize.y);
					newHeight = canvasResolution.y;
				}
			}
			displayImage.rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
		}

		//Multiply the scale factor
		displayImage.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
	}

	private void _ClearTexture()
	{
		if(displayImage != null && displayImage.sprite != null && displayImage.sprite.texture != null)
		{
			Texture2D.Destroy(displayImage.sprite.texture);
			displayImage.sprite = null;
		}
	}

	public void Clear()
	{
		_ClearTexture();
		displayImage.rectTransform.sizeDelta = Vector2.zero;
	}

	private void SaveAsJPG(Texture2D tex2D)
	{
		string debugMessage = "Saved_as_JPG_to:_" + new FilePathName().SaveTextureAs(tex2D, FilePathName.SaveFormat.JPG);
		ScreenshotHelper.Instance.UpdateDebugText(debugMessage);
	}

	private void SaveAsPNG(Texture2D tex2D)
	{
		string debugMessage = "Saved_as_PNG_to:_" + new FilePathName().SaveTextureAs(tex2D, FilePathName.SaveFormat.PNG);
		ScreenshotHelper.Instance.UpdateDebugText(debugMessage);
	}

//	private void SaveAsGIF(Texture2D tex2D)
//	{
//		string debugMessage = "Saved as GIF to: " + new FilePathName().SaveTextureAs(tex2D, FilePathName.SaveFormat.GIF);
//		ScreenshotHelper.Instance.UpdateDebugText(debugMessage);
//	}

	public void ClearScreenshotHelper()
	{
		ScreenshotHelper.iClear();
	}

	public void UnRegRenderCameras()
	{
		ScreenshotHelper.iUnRegisterAllRenderCameras();
	}

	public void OurAssets()
	{
		Application.OpenURL("https://www.swanob2.com/assets");
	}
}
