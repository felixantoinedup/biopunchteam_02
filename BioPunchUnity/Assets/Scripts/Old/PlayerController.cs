using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speedMove;
    public float speedRotation;

    bool isDabing = false;
    bool isHappy = false;
    bool isRejected = false;

    float inputV;
    float inputH;
    float pressionJoystick;

    Animator anim;
    Rigidbody rBody;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        inputV = Input.GetAxis("Vertical");
        inputH = Input.GetAxis("Horizontal");

        pressionJoystick = Mathf.Max(Mathf.Abs(inputV), Mathf.Abs(inputH));

        anim.SetFloat("Speed", Mathf.Abs(pressionJoystick));

        isDabing = false;

        if (Input.GetKey(KeyCode.Space))
        {
            isDabing = true;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isHappy = true;
            anim.SetTrigger("Happy");
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isRejected = true;
            anim.SetTrigger("Rejected");
        }

        anim.SetBool("Dab", isDabing);
    }

    void FixedUpdate()
    {
        if (!isDabing && !isHappy && !isRejected)
        {
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        Vector3 movement = transform.forward * speedMove * Time.deltaTime * pressionJoystick;
        rBody.MovePosition(rBody.position + movement);
    }

    private void Rotate()
    {
        if (Mathf.Abs(inputV) >= 0.1 || Mathf.Abs(inputH) >= 0.1)
        {
            Vector3 targetDir = new Vector3(inputH, transform.position.y, inputV);
            float step = speedRotation * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    private void StopHappy()
    {
        isHappy = false;
    }

    private void StopRejected()
    {
        isRejected = false;
    }
}
