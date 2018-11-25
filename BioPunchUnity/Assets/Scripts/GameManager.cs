using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public float delayPoints = 3f;
    public float[] SectionsZoomValues;
    public float[] NextSectionScoreValues;

    public int currentSection = 0;
    public int currentScore = 0;
    public float currentTimer = 0;
    public int currentMultiplicator = 1;

    public bool isGameStarted = false;
    public float pointsNextTimeStamp = 0;

    CameraController cameraController;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        cameraController = Camera.main.GetComponent<CameraController>();
    }

    // Use this for initialization
    void Start () {
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isGameStarted)
            return;

        if(currentSection <= NextSectionScoreValues.Length && currentSection > 0)
        {
            if(currentScore >= NextSectionScoreValues[currentSection - 1])
            {
                ChangeSection();
            }
        }

        currentTimer -= Time.deltaTime;

        if(currentTimer <= 0)


        if(Time.time >= pointsNextTimeStamp)
        {
            pointsNextTimeStamp = pointsNextTimeStamp + delayPoints;
        }
	}

    void ChangeSection ()
    {
        ++currentSection;
        currentTimer = 120f;
        cameraController.SetTargetZoom(GetTargetZoom());

        if(currentSection == 2)
        {
            AkSoundEngine.PostEvent("MUS_Layer2", gameObject);
        }
    }

    float GetTargetZoom()
    {
        int section = currentSection;
        section = Mathf.Min(section, SectionsZoomValues.Length);

        int index = section - 1;

        return SectionsZoomValues[index];
    }

    void StartGame()
    {
        isGameStarted = true;
        pointsNextTimeStamp = Time.time + delayPoints;
        ChangeSection();
        AkSoundEngine.PostEvent("Music_Start", gameObject);
        AkSoundEngine.PostEvent("AMB_Level_Start", gameObject);
        AkSoundEngine.PostEvent("SFX_Bee_Buzz", gameObject);
    }

    void EndGame()
    {

    }

    public void AddScore(int _score)
    {
        currentScore += _score;
    }
}
