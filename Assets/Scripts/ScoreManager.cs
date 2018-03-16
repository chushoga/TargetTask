using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public GameObject scoreText;
	private Text score;

	// Use this for initialization
	void Start () {
		score = scoreText.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		score.text = "" + GameManager.SCORE;
	}

}
