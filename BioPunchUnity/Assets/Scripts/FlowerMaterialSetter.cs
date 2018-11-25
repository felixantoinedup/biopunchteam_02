using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerMaterialSetter : MonoBehaviour {

    //public Material MaterialToChangeOnPollen;
    public Material PetalMaterial;
    private Renderer rend;

    private void Awake()
    {

    }

    public void SetMaterial(Material material_)
    {
        rend.material = material_;

    }

	// Use this for initialization
	void Start () {

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            if (r.gameObject.name == "Petales")
                rend = r;

        }

        if (PetalMaterial != null)
            SetMaterial(PetalMaterial);
    }
	


	// Update is called once per frame
	void Update () {
		
	}
}
