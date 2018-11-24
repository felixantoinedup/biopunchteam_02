using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    public int minimumShakesToTwerk = 3;
    public GameObject pollen;

    FlowerController currentFlower = null;
    FlowerController flowerWhoGavePollen = null;
    int currentNbrShakes = 0;

    bool isTwerking = false;
    bool isHoldingPollen = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("No");
            pollen.SetActive(false);
            flowerWhoGavePollen = null;
            SetIsHoldingPollen(false);
        }
        else
        {
            Debug.Log("Yes");
            pollen.SetActive(true);
            flowerWhoGavePollen = currentFlower;
            SetIsHoldingPollen(true);
        }

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
    }

    public bool GetIsHoldingPollen()
    {
        return isHoldingPollen;
    }

    public void SetIsHoldingPollen(bool _isHoldingPollen)
    {
        isHoldingPollen = _isHoldingPollen;
    }
}
