using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour
{
    public System.String PlantName;
    public System.Guid plantUniqueId;

    private List<System.String> dnaChain;
    private System.String PlantCrossedName;
    private float cDefaultPlantLife = 25.0f;
    private float plantLife = 0.0f;
    private float plantLifeLossOverTime = 1.0f;

    public Material[] CrossPollenMaterials;
    private Renderer rend;
    //public Mesh[] CrossPollenMeshes;
    //private MeshFilter filter;
    public GameObject[] CrossPollenPrefabs;

    public Material MaterialToUpdateForCrossPollen;

    public int flowerSection = 1;
    public int nbrShakeForPollen = 10;

    bool isFlowerActive = false;
    float pointsNextTimeStamp = 0;

    private Material defaultMaterial;

    // Use this for initialization
    void Start()
    {
        plantLife = cDefaultPlantLife;
        dnaChain = new List<System.String>();
        dnaChain.Add(PlantName);
        PlantCrossedName = PlantName;
        plantUniqueId = System.Guid.NewGuid();
        Debug.Log("PlantId:" + plantUniqueId.ToString());
        //rend = GetComponentInChildren<Renderer>();
        //rend.material = CrossPollenMaterials[0];
        //filter = GetComponentInChildren<MeshFilter>();
        //Instantiate(CrossPollenPrefabs[0]);

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        Renderer currentRender = null;
        foreach (Renderer r in renderers)
        {
            if (r.gameObject.name == "Petales")
                currentRender = r;

        }
        if(currentRender)
            defaultMaterial = currentRender.material;
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
            GameManager.instance.AddFlowerSpecies(PlantCrossedName);
        }
        else if (other.plantUniqueId != this.plantUniqueId)
        {
            Debug.Log("Mix Pollen NO CROSS");
        }
        ResetPlantLife();
    }

    void UpgradePlant()
    {
        //if(rend)
        //{ 
        //    rend.material = CrossPollenMaterials[DnaChainLength() - 1];
        //}
        ////if (filter)
        ////{
        ////    filter.mesh = CrossPollenMeshes[DnaChainLength() - 1];
        ////}



        ClearChildren();

        var pos = this.transform.position;
        //var rot = this.transform.rotation;

        //Instantiate(CrossPollenPrefabs[DnaChainLength() - 1], pos, this.transform.rotation);
        //var newprefab = Instantiate(CrossPollenPrefabs[DnaChainLength() - 1], transform.position, transform.rotation);
        GameObject newprefab = Instantiate(CrossPollenPrefabs[DnaChainLength() - 1], pos, Quaternion.identity) as GameObject;

        newprefab.transform.parent = transform;

        //newprefab.transform.localScale = this.transform.localScale;
        newprefab.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        newprefab.transform.eulerAngles = new Vector3(-90f, 0f, 0f);

        FlowerMaterialSetter matSetter = GetComponentInChildren<FlowerMaterialSetter>();
        if(dnaChain[dnaChain.Count -1] == "Rose")
        { 
            matSetter.PetalMaterial = CrossPollenMaterials[2];
        }
        else if (dnaChain[dnaChain.Count - 1] == "Bleuet")
        {
            matSetter.PetalMaterial = CrossPollenMaterials[0];
        }
        else if (dnaChain[dnaChain.Count - 1] == "Marguerite" && CrossPollenMaterials.Length > 2 )
        {
            matSetter.PetalMaterial = CrossPollenMaterials[3];
        }
        else
        {
            matSetter.PetalMaterial = CrossPollenMaterials[DnaChainLength() - 1];
        }
    }

    public void ClearChildren()
    {
        Debug.Log(transform.childCount);
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }

        Debug.Log(transform.childCount);

    }

    void DowngradePlant()
    {
        ////if (rend)
        ////{
        ////    rend.material = CrossPollenMaterials[DnaChainLength() - 1];
        ////}
        //////if (filter)
        //////{
        //////    filter.mesh = CrossPollenMeshes[DnaChainLength() - 1];
        //////}
        ////Instantiate(CrossPollenPrefabs[DnaChainLength() - 1]);

        //Instantiate(CrossPollenPrefabs[DnaChainLength() - 1], this.transform.position, this.transform.rotation);
        //var newprefab = Instantiate(CrossPollenPrefabs[DnaChainLength() - 1], transform.position, transform.rotation);
        //newprefab.transform.parent = transform;

        ClearChildren();

        var pos = this.transform.position;
        //var rot = this.transform.rotation;

        //Instantiate(CrossPollenPrefabs[DnaChainLength() - 1], pos, this.transform.rotation);
        //var newprefab = Instantiate(CrossPollenPrefabs[DnaChainLength() - 1], transform.position, transform.rotation);
        GameObject newprefab = Instantiate(CrossPollenPrefabs[DnaChainLength() - 1], pos, Quaternion.identity) as GameObject;

        newprefab.transform.parent = transform;

        //newprefab.transform.localScale = this.transform.localScale;
        newprefab.transform.localScale = new Vector3(5.0f, 5.0f, 5.0f);
        newprefab.transform.eulerAngles = new Vector3(-90f, 0f, 0f);

        FlowerMaterialSetter matSetter = GetComponentInChildren<FlowerMaterialSetter>();
        //matSetter.PetalMaterial = CrossPollenMaterials[DnaChainLength() - 1];
        matSetter.PetalMaterial = defaultMaterial;

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
                GameManager.instance.AddFlowerSpecies(PlantName);
            }
            else
            {
                return;
            }
        }

        if(pointsNextTimeStamp != GameManager.instance.pointsNextTimeStamp)

        if(isFlowerActive && GameManager.instance.isGameStarted && Time.time > pointsNextTimeStamp)
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
            GameManager.instance.RemoveFlowerSpecies(PlantCrossedName);
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
