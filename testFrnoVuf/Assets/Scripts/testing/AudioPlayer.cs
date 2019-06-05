using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	public string url;
	public AudioSource audiosource;
	// Use this for initialization
	IEnumerator Start () {
		audiosource = GetComponent<AudioSource> ();
		using(var www = new WWW(url)){
			yield return www;
			audiosource.clip = www.GetAudioClip ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!audiosource.isPlaying){ //&& audiosource.clip.loadState){
			audiosource.Play ();
		}
	}
}
