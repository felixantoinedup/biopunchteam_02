using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    public System.String PlantName;
    public System.Guid plantUniqueId;

    private List<System.String> dnaChain;
    private System.String PlantCrossedName;
    private float cDefaultPlantLife = 15.0f;
    private float plantLife = 0.0f;
    private float plantLifeLossOverTime = 1.0f;


    public int flowerSection = 1;
    public int nbrShakeForPollen = 10;

    bool isFlowerActive = false;
    float pointsNextTimeStamp = 0;

    // Use this for initialization
    void Start()
    {
        plantLife = cDefaultPlantLife;
        dnaChain = new List<System.String>();
        dnaChain.Add(PlantName);
        PlantCrossedName = PlantName;
        plantUniqueId = System.Guid.NewGuid();
        Debug.Log("PlantId:" + plantUniqueId.ToString());

    }

    // The longer the Chain, the more POINTS! 
    int DnaChainLength()
    {
        return dnaChain.Count;
    }

    public void MixPollen(FlowerController other)
    {
        if (other.PlantName != this.PlantName &&
            other.plantUniqueId != this.plantUniqueId &&
            !dnaChain.Contains(other.PlantName))
        {

            dnaChain.Add(other.PlantName);
            PlantCrossedName += other.PlantName;
            Debug.Log("Mix Pollen Dna chain: " + DnaChainLength().ToString() + " Full plant name: " + PlantCrossedName);
            UpgradePlant();
        }
        else if (other.plantUniqueId != this.plantUniqueId)
        {
            Debug.Log("Mix Pollen NO CROSS");
        }
        ResetPlantLife();
    }

    void UpgradePlant()
    {
        //var currentPos = gameObject.transform.position;
        //currentPos.y += 2.0f;
        //gameObject.transform.position = currentPos;
        var currentScale = gameObject.transform.localScale;
        currentScale.x += 2.0f;
        gameObject.transform.localScale = currentScale;
    }

    void DowngradePlant()
    {
        var currentScale = gameObject.transform.localScale;
        currentScale.x = 1.0f;
        gameObject.transform.localScale = currentScale;
    }

    void ResetPlantLife()
    {
        plantLife = cDefaultPlantLife;

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

        plantLife -= Time.deltaTime * plantLifeLossOverTime; // FIXME Slow I know
        //REVERT TO NON - CROSSED PLANT GAME-RULE
        if (plantLife <= 0.0f && DnaChainLength() > 1)
        {
            //dnaChain.RemoveRange(1, dnaChain.Count - 1);
            //PlantName = dnaChain[0];
            //PlantCrossedName = dnaChain[0];
            dnaChain.RemoveAt(dnaChain.Count - 1);
            PlantCrossedName = "";
            foreach(System.String string_ in dnaChain)
            {
                PlantCrossedName += string_;
            }

            Debug.Log("Plant REVERTED " + PlantCrossedName);
            ResetPlantLife();
            DowngradePlant();
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
        GameManager.instance.AddScore(DnaChainLength());
        Debug.Log("Points Given!!!!!!!: " + DnaChainLength());
    }
}
