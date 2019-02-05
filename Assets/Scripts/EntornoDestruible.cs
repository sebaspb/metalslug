using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntornoDestruible : MonoBehaviour
{

public GameObject[] ParedDestruible1;
public GameObject[] Libreria;
public bool cambiar=false;
int controlParedDestruible1=-1;



    void Update()
    {
        
        if (gameObject.name=="Pared Destruible 1" && cambiar)
        {
            cambiar=false;
            if (controlParedDestruible1==3)
            {
                gameObject.GetComponent<BoxCollider2D>().enabled=false;
                Libreria[0].SetActive(true);
            }

        ParedDestruible1[controlParedDestruible1].SetActive(true);

            if(controlParedDestruible1>0)
            {
                ParedDestruible1[controlParedDestruible1-1].SetActive(false);
            }

        }

        if (destruibles.barril1Done)
        {
            Libreria[1].SetActive(false);
        }

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (gameObject.name=="Pared Destruible 1")
        {
            if (collider.tag=="Player" && collider.name.Contains("Bala A"))
            {
                controlParedDestruible1+=1;

                Destroy(collider.gameObject);
                cambiar=true;
            }
        }
    }
}
