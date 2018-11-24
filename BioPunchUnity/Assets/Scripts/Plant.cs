
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour {

    //public class PlantType {
    //    public int value = 0;
    //}

    public System.String PlantName;
    public System.Guid plantUniqueId;

    private List<System.String> dnaChain;
    private System.String PlantCrossedName;
    private float plantLife = 100.0f;
    private float plantLifeLossOverTime = 10.0f;

    // Use this for initialization
    void Start () {
        dnaChain = new List<System.String>();
        dnaChain.Add(PlantName);
        PlantCrossedName = PlantName;
        plantUniqueId = System.Guid.NewGuid();
        Debug.Log(plantUniqueId.ToString());
    }

    void MixPollen(Plant other)
    {
        if(other.PlantName != this.PlantName && 
            other.plantUniqueId != this.plantUniqueId && 
            !dnaChain.Contains(other.PlantName))
        {
            
            dnaChain.Add(other.PlantName);
            PlantCrossedName += other.PlantName;
            Debug.Log("Mix Pollen Dna chain: " + DnaChainLength().ToString() + " Full plant name: " + PlantCrossedName);
        }
        else if (other.plantUniqueId != this.plantUniqueId)
        {
            Debug.Log("Mix Pollen NO CROSS");
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Plant Trigger enter: " + PlantCrossedName);

        //Plant plant_ = other.GetComponent<Plant>();
        BeePockets bee = other.GetComponent<BeePockets>();

        if(bee != null)
        {
            if (bee.plantPollen != null)
            {
                this.MixPollen(bee.plantPollen);
                bee.plantPollen = null;
                Debug.Log("Bee dropped pollen");
            }
            else
            {
                bee.plantPollen = this;
                Debug.Log("Bee received: " + PlantCrossedName);
            }
        }      
    }


    // The longer the Chain, the more POINTS! 
    int DnaChainLength()
    {
        return dnaChain.Count;
    }
	
	// Update is called once per frame
	void Update () {
        plantLife -= Time.deltaTime * 1.0f/plantLifeLossOverTime; // FIXME Slow I know

    }
}
