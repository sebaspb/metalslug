using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 public Transform player;       //Public variable to store a reference to the player game object
public float xMin;
public float xMax;

public float yMin;
public float yMax;
Transform transformCamara;


    void Start()
    {
        transformCamara = transform;
    }



 void LateUpdate ()
   {
    transform.position = new Vector3(player.position.x,player.position.y,transformCamara.position.z);
    float x = Mathf.Clamp(player.transform.position.x,xMin,xMax);
    float y =Mathf.Clamp(player.transform.position.y,yMin,yMax);
    transformCamara.position= new Vector3(x,y,transformCamara.position.z);
   }
}
