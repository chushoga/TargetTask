using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public GameObject scoreText;
	public GameObject timerText;
	private Text score;
	private Text timer;

	// Use this for initialization
	void Start () {
		score = scoreText.GetComponent<Text>();
		timer = timerText.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		score.text = "" + GameManager.SCORE + "点";
		timer.text = "" + GameManager.TIME_REMAINING + "s";
	}

}
