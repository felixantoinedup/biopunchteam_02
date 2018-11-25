using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public int minimumShakesToTwerk = 3;
    public float durationStun = 1f;
    public GameObject pollen;
    public GameObject beeMesh;

    FlowerController currentFlower = null;
    FlowerController flowerWhoGavePollen = null;
    int currentNbrShakes = 0;
    float currentBeeSpeed = 0;

    bool isTwerking = false;
    bool isHoldingPollen = false;

    BeeMovementController beeMovementController;
    Rigidbody rBody;
    Animator beeAnimator;

    private void Awake()
    {
        beeMovementController = GetComponent<BeeMovementController>();
        rBody = GetComponent<Rigidbody>();
        beeAnimator = beeMesh.GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        beeAnimator.SetFloat("Speed", currentBeeSpeed/beeMovementController.distanceMaxSpeed);
        AkSoundEngine.SetRTPCValue("Fly_speed", (currentBeeSpeed / beeMovementController.distanceMaxSpeed) * 100);

        if (CheckIfEnoughForPollen())
        {
            PollenContextualAction();
        }
    }

    bool CheckIfEnoughForPollen()
    {
        if (!GetIsTwerking())
            return false;

        if (currentNbrShakes >= currentFlower.nbrShakeForPollen)
            return true;

        return false;
    }

    void PollenContextualAction()
    {
        ResetShakes();

        if (GetIsHoldingPollen())
        {
            pollen.SetActive(false);
            //flowerWhoGavePollen.MixPollen(currentFlower);
            currentFlower.MixPollen(flowerWhoGavePollen);
            currentFlower = null;
            flowerWhoGavePollen = null;
            SetIsHoldingPollen(false);
        }
        else
        {
            pollen.SetActive(true);
            flowerWhoGavePollen = currentFlower;
            SetIsHoldingPollen(true);
        }

    }

    void DropPollen()
    {
        pollen.SetActive(false);
        flowerWhoGavePollen = null;
        SetIsHoldingPollen(false);
    }

    public FlowerController GetCurrentFlower()
    {
        return currentFlower;
    }

    public void SetCurrentFlower(FlowerController _currentFlower)
    {
        currentFlower = _currentFlower;

        if (currentFlower == null)
        {
            ResetShakes();
        }
    }

    public void IncreaseShakes()
    {
        ++currentNbrShakes;
    }

    public void ResetShakes()
    {
        currentNbrShakes = 0;
    }

    public bool IsEnoughShakeToTwerk()
    {
        if (currentNbrShakes >= minimumShakesToTwerk)
            return true;

        return false;
    }

    public bool CanGivePollen()
    {
        if (!GetIsHoldingPollen())
            return false;

        if (currentFlower == flowerWhoGavePollen)
            return false;

        return true;
    }

    public bool GetIsTwerking()
    {
        return isTwerking;
    }

    public void SetIsTwerking(bool _isTwerking)
    {
        isTwerking = _isTwerking;
        beeAnimator.SetBool("IsTwerking", isTwerking);
    }

    public bool GetIsHoldingPollen()
    {
        return isHoldingPollen;
    }

    public void SetIsHoldingPollen(bool _isHoldingPollen)
    {
        isHoldingPollen = _isHoldingPollen;
    }

    public void SetCurrentSpeed(float _speed)
    {
        currentBeeSpeed = _speed;
    }

    public void PushBee(Vector3 _pushPosition, float _force)
    {
        _pushPosition.y = beeMovementController.beeHeight;
        DropPollen();
        rBody.AddForce((transform.position - _pushPosition).normalized * _force, ForceMode.Impulse);
        StartCoroutine(Stun());
    }

    IEnumerator Stun()
    {
        beeMovementController.SetDisableMovement(true);
        yield return new WaitForSeconds(durationStun);
        rBody.velocity = new Vector3(0, 0, 0);
        beeMovementController.SetDisableMovement(false);
    }
}
