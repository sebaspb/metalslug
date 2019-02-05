using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldado : MonoBehaviour
{

public float salud=3;

public Animator animador;

public GameObject jugador;

public GameObject contenedorSoldado;

float distanciaAJugador;
bool controlMuerte=false;
bool controlAtaque=false;



    // Start is called before the first frame update
    void Start()
    {

        jugador = GameObject.Find("Contenedor Jugador");

    }

    // Update is called once per frame
    void Update()
    {

        distanciaAJugador = Vector3.Distance(jugador.transform.position, contenedorSoldado.transform.position);
        RevisarMuerte();
        
        if(!controlMuerte)
        {
            RevisarDistanciaConElJugador();
            GirarEnemigo();
        } 
    }


    void RevisarMuerte()
    {
        if (salud<=0)
        {
            animador.SetBool("estaMuerto?",true);
            controlMuerte=true;
            
            StartCoroutine(EliminarSoldado(2));
        }
    }


    void RevisarDistanciaConElJugador()
    {

        if(distanciaAJugador < 3)
        {
            animador.SetBool("estaActivo?",true);
        }

        if(distanciaAJugador > 3)
        {
            animador.SetBool("estaActivo?",false);
        }

        if(distanciaAJugador < 0.5f)
        {
            animador.SetBool("puedeAtacar?",true);
            controlAtaque=true;
        }

        if(distanciaAJugador > 0.5f && animador.GetBool("estaActivo?") == true)
        {
            MoverseHaciaElJugador();

            animador.SetBool("puedeAtacar?",false);
            controlAtaque=false;
        }
    }

    void GirarEnemigo()
    {

        if (contenedorSoldado.transform.position.x < jugador.transform.position.x)
        {
            contenedorSoldado.transform.rotation = Quaternion.Euler(0, 180f, 0);
        }

        if (contenedorSoldado.transform.position.x > jugador.transform.position.x)
        {
            contenedorSoldado.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void MoverseHaciaElJugador()
    {

        contenedorSoldado.transform.position = Vector2.MoveTowards(contenedorSoldado.transform.position,
        new Vector2(jugador.transform.position.x, contenedorSoldado.transform.position.y), Time.deltaTime * 1.4f);

    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag=="Player" && collider.name.Contains("Bala A"))
        {
            salud-=1;
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.tag=="Player" && controlAtaque)
        {
            
            // Jugador.salud-=1;
            // Debug.Log(Jugador.salud);
        }
    }


    IEnumerator EliminarSoldado(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(contenedorSoldado);
    }

}