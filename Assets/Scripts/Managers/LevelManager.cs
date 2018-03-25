using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	private GameObject GameOverScreen;

	void Start(){
		GameOverScreen = GameObject.Find("GameOver");
	}

	public static void ShowGameOver(){
		GameOverScreen.SetActive(true);
	}

	public void RestartLevel(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	public void LoadLevel(string levelName){
		SceneManager.LoadScene(levelName);
	}
}
