using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovementController : MonoBehaviour {

    public float beeSpeed = 0.3f;
    public float beeRotationSpeed = 1f;
    public float beeHeight = 1f;
    public float zOffset = 0f;
    public LayerMask floorMask;

    Rigidbody rBody;
    float camRayLength = 100f;

    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveTorwardsCursor();
        RotateTorwardsCursor();
    }

    void FixedUpdate()
    {
        
    }

    void MoveTorwardsCursor()
    {
        transform.position = Vector3.SmoothDamp(transform.position, GetMouseCursorPosition(), ref velocity, beeSpeed);
    }

    void RotateTorwardsCursor()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(GetMouseCursorPosition() - transform.position), beeRotationSpeed * Time.deltaTime);
    }

    Vector3 GetMouseCursorPosition()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        Vector3 cursorPosition = new Vector3(0, 0, 0);

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            cursorPosition = floorHit.point;
        }

        cursorPosition.y = beeHeight;
        cursorPosition.z = cursorPosition.z - zOffset;

        return cursorPosition;
    }
}
