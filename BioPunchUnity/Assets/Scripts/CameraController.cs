using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float zoomSpeed = 0.3f;

    float targetZoom;

    Camera mainCamera;

    float velocity;

    void Awake ()
    {
        mainCamera = Camera.main;
        targetZoom = mainCamera.orthographicSize;
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        //Orthographic
        if (targetZoom != mainCamera.orthographicSize)
            mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, targetZoom, ref velocity, zoomSpeed);
	}

    public void SetTargetZoom(float _targetZoom)
    {
        targetZoom = _targetZoom;
    }
}
