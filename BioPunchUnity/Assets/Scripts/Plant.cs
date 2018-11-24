
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour {

    //public class PlantType {
    //    public int value = 0;
    //}

    public System.String PlantName;

    private List<System.String> dnaChain;
    private System.String PlantCrossedName;

    // Use this for initialization
    void Start () {
        dnaChain = new List<System.String>();
        dnaChain.Add(PlantName);
        PlantCrossedName = PlantName;
    }

    void MixPollen(Plant other)
    {
        if(other.PlantName != this.PlantName && !dnaChain.Contains(other.PlantName))
        {
            
            dnaChain.Add(other.PlantName);
            PlantCrossedName += other.PlantName;
        }
        Debug.Log("Dna chain: " + DnaChainLength().ToString() + " Full plant name: " + PlantCrossedName);
    }


    private void OnTriggerEnter(Collider other)
    {
        Plant plant_ = other.GetComponent<Plant>();

        if(plant_ != null)
        {
            this.MixPollen(plant_);

        }
          
    }


    // The longer the Chain, the more POINTS! 
    int DnaChainLength()
    {
        return dnaChain.Count;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
