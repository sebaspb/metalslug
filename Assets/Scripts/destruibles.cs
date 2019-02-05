using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruibles : MonoBehaviour
{

    public float controlBlink=0;
    public float controlHit=0;
    public float controlHitBar=0;

    public GameObject Explosion;
    public GameObject ReemplazoSprite;

    public Animator[] Debris;

    public static bool barril1Done;


    // Update is called once per frame
    void Update()
    {
        if (controlBlink==6)
        {
            controlBlink=0;
            StopAllCoroutines();
        }

        if(controlHit>=4)
        {   
            StartCoroutine(Destruir(2f));

 
            Explosion.GetComponent<Animator>().SetBool("puedeExplotar?",true); 
            gameObject.GetComponent<SpriteRenderer>().enabled=false;
            gameObject.GetComponent<BoxCollider2D>().enabled=false;

            if (gameObject.name=="barril Blink")
            {
                barril1Done=true;
            }
      
        }  
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (gameObject.name.Contains("Blink") &&  collider.name.Contains("Bala"))
        {
            Destroy(collider.gameObject);
            controlBlink=0;
            controlHit+=1;

            Hit();
        }

        if (!gameObject.name.Contains("Blink") &&  collider.name.Contains("Bala"))
        {
            Destroy(collider.gameObject);

            if (ReemplazoSprite!=null)
            {
                controlHitBar+=1;

                if(controlHitBar>3)
                {
                    for(int i = 0; i < Debris.Length; i++)
                    {
                        if (i%2==0)
                        {
                            Debris[i].SetInteger("Variacion",0);     
                        }

                        if (i%2==1)
                        {
                            Debris[i].SetInteger("Variacion",1); 
                        }    
                    }

                gameObject.GetComponent<SpriteRenderer>().enabled=false;
                ReemplazoSprite.GetComponent<SpriteRenderer>().enabled=true;
                gameObject.GetComponent<BoxCollider2D>().enabled=false;

                }
            }
            
        }
    }


    void Hit()
    {
        if(controlBlink<6)
        {    
            StartCoroutine(ParpadeoBlanco(0.3f*Time.deltaTime));
        }  
    }

    IEnumerator ParpadeoBlanco(float time)
    {
        yield return new WaitForSeconds(time);
        controlBlink+=1;
        gameObject.GetComponent<SpriteRenderer>().material.shader = Shader.Find("GUI/Text Shader");
        StartCoroutine(ParpadeoColor(0.3f*Time.deltaTime));
    }

    
    IEnumerator ParpadeoColor(float time)
    {
        yield return new WaitForSeconds(time);
        controlBlink+=1;
        gameObject.GetComponent<SpriteRenderer>().material.shader = Shader.Find("Sprites/Default");
        StartCoroutine(ParpadeoBlanco(0.3f*Time.deltaTime));
    }

    IEnumerator Destruir(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
