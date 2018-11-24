using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testSoundLauncher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space"))
        {
            AkSoundEngine.PostEvent("Music", gameObject);
            Debug.Log("Test");
        }
	}
}
