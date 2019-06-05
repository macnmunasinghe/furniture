using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
public class TakeScreenshot : MonoBehaviour {

//	[SerializeField]
//	GameObject blink;
	public Text text;
	public Text text1;
	public Text text2;

	public GameObject pl;

	public Canvas canvas;

	public Texture LogoImage;
	private Texture2D Logo;

	private string file;//file
	public string pathToSave;

	public void Start(){
		Logo = LogoImage as Texture2D;
		if(Logo==null){
			Debug.Log ("nulllll");
		}
	}

	public void TakeAShot()
	{
		StartCoroutine ("CaptureIt");
	}

	IEnumerator CaptureIt()
	{
		string timeStamp = System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
		string fileName = "Screenshot" + timeStamp + ".png";
		pathToSave = fileName;
		canvas.GetComponent<Canvas> ().worldCamera = Camera.main;
		Application.CaptureScreenshot(pathToSave);
		Debug.Log ("pathToSave  "+pathToSave);
		yield return new WaitForEndOfFrame();

		canvas.GetComponent<Canvas> ().worldCamera = null;
//		Instantiate (blink, new Vector2(0f, 0f), Quaternion.identity);
		pl.SetActive(true);
		yield return new WaitForSeconds (0.5f);
		pl.SetActive (false);
		//		file =@"C:/Users/chamara/Documents/myunity/testFr _noVuf/"+pathToSave;//editor
		//file = Application.persistentDataPath + "/"+pathToSave;//android
		//file = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
		Debug.Log ("file  "+file);
		yield return new WaitForSeconds (1f);
		AddLogo(Logo);
	}

	public void AddLogo(Texture2D logo){
		String filePath;
//		String[] file = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
		file = Application.persistentDataPath + "/"+pathToSave;
		filePath = file;
		text.text = filePath.ToString ();
		Texture2D texture = null;

		byte[] fileBytes;

//		if (File.Exists (filePath)){
			fileBytes = File.ReadAllBytes (filePath);
			text1.text = fileBytes.ToString ();
			texture = new Texture2D (2, 2, TextureFormat.RGB24, false);
			texture.LoadImage (fileBytes);
			
//		}


		int startX = (texture.width-10) - logo.width;//0;
		int startY = 0;//texture.height - logo.height;

		for (int x = startX; x < texture.width-10; x++)
		{

			for (int y = startY; y < logo.height; y++)//texture.height
			{
				Color bgColor = texture.GetPixel(x, y);
				Color wmColor = logo.GetPixel(x - startX, y - startY);

				Color final_color = Color.Lerp(bgColor, wmColor, wmColor.a / 1.0f);

				texture.SetPixel(x, y, final_color);
//				Debug.Log ("X "+x + "Y "+ y);
			}
		}

		texture.Apply();
		byte[] bytes = texture.EncodeToPNG ();
		File.WriteAllBytes (filePath,bytes);

	}

}
