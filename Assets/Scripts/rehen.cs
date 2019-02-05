using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rehen : MonoBehaviour
{
    bool liberado = false;
    int indexAnimacionRandom = 6;
    public Animator Animador;
    bool done = false;
    bool ejecutado = false;


    // Update is called once per frame
    void Update()
    {
        if(liberado && indexAnimacionRandom != 6 )
        {

            if(!ejecutado)
            {
                ejecutado=true;
                if(indexAnimacionRandom==0)
                {
                    Animador.SetInteger("Random",0);
                    if(Animador.isInitialized)
                    {
                        StartCoroutine(AnimacionRandom(2f));
                    }
              
                }

                if (indexAnimacionRandom==1)
                {
                    Animador.SetInteger("Random",1);
                    if(Animador.isInitialized)
                    {
                     StartCoroutine(AnimacionRandom(1.5f)); 
                    }
                }

                if (indexAnimacionRandom==2)
                {
                    if(Animador.isInitialized)
                    {
                        Animador.SetInteger("Random",2);
                        StartCoroutine(AnimacionRandom(5f)); 
                    }
                }

                if (indexAnimacionRandom==3)
                {
                    Animador.SetInteger("Random",3);
                }
        
                if (indexAnimacionRandom==4)
                {
                    Animador.SetInteger("Random",4);
                }
            }   
        }
    }

     void OnTriggerEnter2D(Collider2D col)
     {

        if (col.tag=="Player")
        {
            Animador.SetBool("Liberado",true);
            liberado=true;
            if (!done)
            {
                done=true;
                StartCoroutine(AnimacionRandom(2.5f)); 
            }
        }
     }

     IEnumerator AnimacionRandom(float time)
    {
        yield return new WaitForSeconds(time);
        indexAnimacionRandom  = Random.Range(0,5); 
        Debug.Log(indexAnimacionRandom);
        ejecutado=false;
    }
}
