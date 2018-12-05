using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderController : MonoBehaviour {

    public float speed = 1;

    Image fader;
    bool fadeIn = false;
    bool fadeOut = false;

	// Use this for initialization
	void Start () {
        fader = GetComponent<Image>();
        fadeIn = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(fadeIn)
        {
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, Mathf.MoveTowards(fader.color.a, 0f, speed));
        }
        else if (fadeOut)
        {
            fader.color = new Color(fader.color.r, fader.color.g, fader.color.b, Mathf.MoveTowards(fader.color.a, 255f, speed));
        }
	}

    public void FadeIn()
    {
        fadeIn = true;
        fadeOut = false;
    }

    public void FadeOut()
    {
        fadeIn = false;
        fadeOut = true;
    }
}
