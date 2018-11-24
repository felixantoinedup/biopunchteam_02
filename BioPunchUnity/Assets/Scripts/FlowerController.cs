using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    public int flowerSection = 1;
    public int nbrShakeForPollen = 10;

    bool isFlowerActive = false;
    float pointsNextTimeStamp = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isFlowerActive)
        {
            if(GameManager.instance.currentSection >= flowerSection)
            {
                isFlowerActive = true;
                pointsNextTimeStamp = GameManager.instance.pointsNextTimeStamp;
            }
            else
            {
                return;
            }
        }

        if(pointsNextTimeStamp != GameManager.instance.pointsNextTimeStamp)

        if(GameManager.instance.isGameStarted && Time.time > pointsNextTimeStamp)
        {
            pointsNextTimeStamp = pointsNextTimeStamp + GameManager.instance.delayPoints;
            GivePoints();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bee")
        {
            other.gameObject.GetComponent<BeeController>().SetCurrentFlower(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bee")
        {
            other.gameObject.GetComponent<BeeController>().SetCurrentFlower(null);
        }
    }

    void GivePoints()
    {
        GameManager.instance.AddScore(1);
    }
}
