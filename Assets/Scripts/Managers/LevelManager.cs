using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public GameObject gameOverScreen;
	private CanvasGroup gameOverPanel; 

	void Start(){
		
	}

	// Show the game over screen
	public void ShowGameOver(){
		gameOverPanel = gameOverScreen.GetComponent<CanvasGroup>();
		gameOverPanel.alpha = 1;
		gameOverPanel.interactable = true;
		gameOverPanel.blocksRaycasts = true;
	}

	// hide the game overscreen
	public void HideGameOver(){
		gameOverPanel = gameOverScreen.GetComponent<CanvasGroup>();
		gameOverPanel.alpha = 0;
		gameOverPanel.interactable = false;
		gameOverPanel.blocksRaycasts = false;
	}

	// restart the current level
	public void RestartLevel(){
		Scene scene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(scene.name);
	}

	// load the level with the name given
	public void LoadLevel(string levelName){
		SceneManager.LoadScene(levelName);
	}
}
