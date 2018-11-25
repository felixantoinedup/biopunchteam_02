using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovementController : MonoBehaviour {

    public float beeSpeed = 0.3f;
    public float beeRotationSpeed = 1f;
    public float beeHeight = 1f;
    public float zOffset = 0f;
    public float twerkDelay = 0.15f;
    public LayerMask floorMask;

    float camRayLength = 100f;
    Vector3 currentMouseCursorPosition;
    Vector3 previousMouseCursorPosition;
    float twerkNextTimeStamp = 0;

    BeeController beeController;

    bool isMovementDisable = false;

    Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        currentMouseCursorPosition = new Vector3(0, 0, 0);
        previousMouseCursorPosition = new Vector3(0, 0, 0);

        beeController = GetComponent<BeeController>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentMouseCursorPosition = GetMouseCursorPosition();

        if(Time.time > twerkNextTimeStamp)
        {
            beeController.ResetShakes();
        }

        if(!CheckIfTwerking())
        {
            if(!isMovementDisable)
            {
                MoveTorwardsCursor();
                RotateTorwardsCursor();
            }

            beeController.SetIsTwerking(false);
        }
        else
        {
            beeController.SetIsTwerking(true);
        }

        previousMouseCursorPosition = currentMouseCursorPosition;
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
        if ((GetMouseCursorPosition() - transform.position).magnitude != 0)
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

    bool CheckIfTwerking()
    {
        if (!beeController.GetCurrentFlower())
            return false;

        if(beeController.GetIsHoldingPollen())
        {
            if (!beeController.CanGivePollen())
                return false;
        }

        float currentX = currentMouseCursorPosition.x - transform.position.x;
        float previousX = previousMouseCursorPosition.x - transform.position.x;

        if (!sameSign(currentX, previousX))
        {
            beeController.IncreaseShakes();
            twerkNextTimeStamp = Time.time + twerkDelay;
        }

        if (!beeController.IsEnoughShakeToTwerk())
            return false;

        return true;
    }

    bool sameSign(float num1, float num2)
    {
        return num1 >= 0 && num2 >= 0 || num1 < 0 && num2 < 0;
    }

    public void SetDisableMovement(bool _isDisable)
    {
        isMovementDisable = _isDisable;
    }
}
