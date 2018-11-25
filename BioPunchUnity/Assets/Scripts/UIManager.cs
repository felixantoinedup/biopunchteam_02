using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text ScoreText;
    public Text TimerText;
    public Text MultiplicatorText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ScoreText.text = "Score: " + GameManager.instance.currentScore;

        int seconds = (int)GameManager.instance.currentTimer % 60;
        int minutes = (int)GameManager.instance.currentTimer / 60;

        TimerText.text = minutes + ":" + seconds;

        MultiplicatorText.text = "x" + GameManager.instance.currentMultiplicator;
	}
}
