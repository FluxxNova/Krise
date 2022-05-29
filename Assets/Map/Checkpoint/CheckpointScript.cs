using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    
    public Material Lightmat;
    public Collider colisionador;
    // Start is called before the first frame update
    void Start()
    {
        Lightmat.SetColor("_EmissionColor", Color.blue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Lightmat.SetColor("_EmissionColor", Color.red);
    }
}
