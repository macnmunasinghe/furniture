using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Runtime.InteropServices;
public class ScreenshotPreview : MonoBehaviour {
	
	[SerializeField]
	GameObject canvas;
	public GameObject gl;

	public Text tx;
//	public Text tx;
	string[] files = null;
//	string[] newfiles = null;
	public List<string> newfiles; 
	int whichScreenShotIsShown= 0;
	private Sprite intiImage;
	public List<int> checklist;
	public Button[] disbuttons;
	// Use this for initialization
	void Start () {
		newfiles = new List<string> ();
		intiImage = canvas.GetComponent<Image> ().sprite;
	}

	void Update(){
		if(files!=null){
			
		}
//		tx.text = files.Length.ToString ();
	}

	void GetPictureAndShowIt()
	{
		
//		string pathToFile = files [whichScreenShotIsShown];//-------------
		string pathToFile = newfiles [whichScreenShotIsShown];
		Debug.Log (whichScreenShotIsShown);
		Texture2D texture = GetScreenshotImage (pathToFile);
		Debug.Log ("ssssss "+ pathToFile);
		Sprite sp = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height),
			new Vector2 (0.5f, 0.5f));
		canvas.GetComponent<Image> ().sprite = sp;
	}

	Texture2D GetScreenshotImage(string filePath)
	{
		Texture2D texture = null;
		byte[] fileBytes;
		if (File.Exists (filePath)) {
			fileBytes = File.ReadAllBytes (filePath);
			texture = new Texture2D (2, 2, TextureFormat.RGB24, false);
			texture.LoadImage (fileBytes);
		}
		return texture;
	}

	public void NextPicture()
	{
		if (files.Length > 0) {
			whichScreenShotIsShown += 1;
			if (whichScreenShotIsShown > newfiles.Count - 1)
				whichScreenShotIsShown = 0;
			GetPictureAndShowIt ();
		} else {
			canvas.GetComponent<Image> ().sprite = intiImage;
		}

	}

	public void PreviousPicture()
	{
//		tx.text = "hjhk";
		if (files.Length > 0) {
			whichScreenShotIsShown -= 1;
			if (whichScreenShotIsShown < 0)
				whichScreenShotIsShown = newfiles.Count - 1;
			GetPictureAndShowIt ();
		}else {
			canvas.GetComponent<Image> ().sprite = intiImage;
		}
	}

	public void galleryAct(){
		gl.SetActive (true);
//		Debug.Log (Application.persistentDataPath);
		files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");//-----------------
		//		yield return new WaitForEndOfFrame (C:\Users\chamara\Documents\myunity\testFr _noVuf);
//		files = Directory.GetFiles(@"C:/Users/chamara/Documents/myunity/testFr _noVuf/", "*.png");
		int lenght = files.Length;
		for (int i = 1; i <=lenght; i++) {
//			Debug.Log (files[]);
			newfiles.Add(files[lenght-i]);

		}
		if (files.Length > 0) {
			for(int i =0;i<disbuttons.Length;i++){
				disbuttons[i].GetComponent<Button>().interactable = true;//---------
			}
			GetPictureAndShowIt ();
		} else {
			canvas.GetComponent<Image> ().sprite = intiImage;
			for(int i =0;i<disbuttons.Length;i++){
			disbuttons[i].GetComponent<Button>().interactable = false;//---------
			}
		}
		Debug.Log (newfiles.Count);
	}

	public void DeletePic(){
		StartCoroutine ("Deletion");

	}

	public void galleryAct1(){
		checklist.Clear();
		newfiles.Clear ();
		gl.SetActive (false);

		whichScreenShotIsShown = 0;
	}
	public void empty(){
//		tx.text = "hjh";
		files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
		int lenght = files.Length - 1;
		for (int i = lenght; i >= 0; i--) {
			newfiles[lenght -i] = files[i];

		}
		if (files.Length > 0) {
			
		} else {
			canvas.GetComponent<Image> ().sprite = intiImage;
		}
//		Debug.Log (newfiles.Length);
	}


	public void Share(){
	#if UNITY_ANDROID
		AnShare();
	#endif

	#if UNITY_IOS
		iosShare();
	#endif
	}
	#if UNITY_ANDROID
	public void AnShare(){
		if (checklist.Count == files.Length) {
			
		} else {
			if (!Application.isEditor) {
				string[] checkfiles = Directory.GetFiles(Application.persistentDataPath + "/", "*.png");
				if(checkfiles.Length>0){
					string pathToFile = files [whichScreenShotIsShown];
					if(pathToFile!=null){
						tx.text = pathToFile;
						AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
						AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");

						intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));
						AndroidJavaClass uriClass = new AndroidJavaClass ("android.net.Uri");
						AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject> ("parse", "file://" + pathToFile);
						intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_STREAM"), uriObject);
						intentObject.Call<AndroidJavaObject> ("setType", "image/png");

						//					intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), "kk");

						AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
						AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");

						AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject> ("createChooser", intentObject, "Share Picture");
						currentActivity.Call ("startActivity", jChooser);
					}					
				}

			}
		}
	}
	#endif


	#if UNITY_IOS

	public void iosShare(){
	if (checklist.Count == files.Length) {

	}else{
		
	}


	}
	#endif
	IEnumerator Deletion(){
		try{
			string pathToFile = newfiles [whichScreenShotIsShown];//files [whichScreenShotIsShown]
			File.Delete (pathToFile);
			newfiles.RemoveAt(whichScreenShotIsShown);
			Debug.Log("whichScreenShotIsShown "+whichScreenShotIsShown);
			checklist.Add(1);
			Debug.Log("checklist ="+ checklist.Count + "files  ="+files.Length);
			if(checklist.Count == files.Length){
				canvas.GetComponent<Image> ().sprite = intiImage;
				for(int i =0;i<disbuttons.Length;i++){
					disbuttons[i].GetComponent<Button>().interactable = false;
				}
				checklist.Clear();
			}
		}
		catch(Exception e){
		}
		if (checklist.Count !=0) {
			Debug.Log("checklist ="+ checklist.Count + "files  ="+files.Length);
			yield return new WaitForEndOfFrame ();
			//PreviousPicture ();
			NextPicture();
		} else {
		}
	}
}
