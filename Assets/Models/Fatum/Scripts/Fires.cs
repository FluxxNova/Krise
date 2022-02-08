using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fires : MonoBehaviour
{

    public GameObject fuego;
    private float CurrentTime;
    public Vector3 CharacterPosition;
    public Vector3 FatumPosition;
    public float radius;
    private Quaternion rotato;
    public float Enemigos;
    private float angle;
    public float start_angle = 360;
    public float angle_increment;
        
    private float total;
    public float FatumX;
    public float FatumY;

    //     (Character.x - fire.x)^2 + (character.y - fire.y)^2 =radius^2
    // fire.y = radius/2

    void Start()
    {
        angle = start_angle;
        angle_increment = 360 / Enemigos;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime = Time.deltaTime;
        CharacterPosition = transform.position;
        
    }

    public void Spawn()
    { 
        
        for (int i = 0; i < Enemigos; i++)
        {
            double sinAngle = Math.Sin(angle);
            float float_sinAngle = (float)sinAngle;
            double cosAngle = Math.Cos(angle);
            float float_cosAngle = (float)cosAngle;
            FatumX =  radius * float_sinAngle;
            FatumY =  radius * float_cosAngle;
            angle = angle + angle_increment;
            FatumPosition.x = CharacterPosition.x + FatumX;
            FatumPosition.y = CharacterPosition.y + FatumY;
            
            Instantiate(fuego, FatumPosition, rotato);
        }
         




    }
}
