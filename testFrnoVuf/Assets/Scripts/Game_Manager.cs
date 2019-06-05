using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

	public static Game_Manager Instance{ set; get;}
	// Use this for initialization
	private void Awake () {
		Instance = this;
		ChangeScene ("Scene_1");
	}
	
	public void ChangeScene(string SceneName){

		SceneManager.LoadScene (SceneName);
	
	} 
}
