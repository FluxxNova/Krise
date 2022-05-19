using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointShader : MonoBehaviour
{
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        rend.material.SetColor("_MaskedColor", Color.magenta);
    }
}
