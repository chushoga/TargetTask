using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	// --------------------------------
	// GAME OVER
	// --------------------------------
	public GameObject gameOverScreen;
	private CanvasGroup gameOverPanel; 

	// --------------------------------
	// FADE SCREEN
	// --------------------------------
	private float fadeSpeed = 1f;
	private Canvas fadeCanvas;
	private Transform coverImageGO; // black overlay
	private Image coverImage; // child of the black overlay
	// --------------------------------


	void Start(){

		// FADE SCREEN SETUP
		fadeCanvas = GetComponentInChildren<Canvas>();
		coverImageGO = fadeCanvas.transform.Find("Image");
		coverImage = coverImageGO.GetComponentInChildren<Image>();

		// start by enabeling the image
		// the default is false -> hidden
		coverImage.enabled = true;

		// start with fading in.
		CrossAlphaWithCallback(coverImage, 0f, fadeSpeed, delegate {
			coverImage.enabled = false;
		});

	}

	public void CrossAlphaWithCallback(Image img, float alpha, float duration, System.Action action){
		StartCoroutine(CrossFadeAlphaCOR(img, alpha, duration, action));
	}

	IEnumerator CrossFadeAlphaCOR(Image img, float alpha, float duration, System.Action action){
		img.enabled = true;

		Color currentColor = img.color;
		Color visibleColor = img.color;
		visibleColor.a = alpha;

		float counter = 0;

		while(counter < duration){
			counter += Time.deltaTime;
			img.color = Color.Lerp(currentColor, visibleColor, counter / duration);
			yield return null;
		}
		action.Invoke();
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
