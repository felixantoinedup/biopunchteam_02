using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePockets : MonoBehaviour {

    public Plant plantPollen;
    bool pollenIndicator = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!pollenIndicator && plantPollen != null)
        {
            var currentScale = gameObject.transform.localScale;
            currentScale.x += 2.0f;
            gameObject.transform.localScale = currentScale;
            pollenIndicator = true;
        }
        else if(pollenIndicator && plantPollen == null)
        {
            var currentScale = gameObject.transform.localScale;
            currentScale.x -= 2.0f;
            gameObject.transform.localScale = currentScale;
            pollenIndicator = false;
        }
	}
}
