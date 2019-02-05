using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldadoCohete : MonoBehaviour
{

    public Rigidbody2D cohete;
    public Transform spawnbala;

    public float salud=3;

    public Animator animador;

    public GameObject jugador;

    public GameObject contenedorSoldado;

    float distanciaAJugador;
    bool controlMuerte=false;
    bool controlAtaque=false;

    bool puedeDisparar=true;

    string direccion = "izquierda";


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

        if(distanciaAJugador < 2f)
        {
            animador.SetBool("puedeAtacar?",true);
            if(puedeDisparar)
            {
                animador.SetBool("estaEnReposo?",true);
                controlAtaque=true;
                StartCoroutine(SpawnCohete(1f));
            
            
                puedeDisparar=false;
                StartCoroutine(Recarga(4));
            }
        }

        if(distanciaAJugador > 2f && animador.GetBool("estaActivo?") == true)
        {
            MoverseHaciaElJugador();
            animador.SetBool("estaEnReposo?",false);
            animador.SetBool("puedeAtacar?",false);
            controlAtaque=false;
        }
    }

    void GirarEnemigo()
    {

        if (contenedorSoldado.transform.position.x < jugador.transform.position.x)
        {
            contenedorSoldado.transform.rotation = Quaternion.Euler(0, 180f, 0);
            direccion="Derecha";
        }

        if (contenedorSoldado.transform.position.x > jugador.transform.position.x)
        {
            contenedorSoldado.transform.rotation = Quaternion.Euler(0, 0, 0);
            direccion="Izquierda";
        }
    }

    void MoverseHaciaElJugador()
    {
        contenedorSoldado.transform.position = Vector2.MoveTowards(contenedorSoldado.transform.position,
        new Vector2(jugador.transform.position.x, contenedorSoldado.transform.position.y), Time.deltaTime * 1.4f);
    }

   
    IEnumerator SpawnCohete(float time)
    {
            yield return new WaitForSeconds(time);

            Rigidbody2D bulletInstance = Instantiate(cohete, spawnbala.position,spawnbala.rotation);
            if (direccion=="Derecha")
            {
                bulletInstance.AddForce(new Vector2(1.5f,0), ForceMode2D.Impulse);
            }

            if (direccion=="Izquierda")
            {
                bulletInstance.AddForce(new Vector2(-1.5f,-0.2f), ForceMode2D.Impulse);
            }

           
    }
     IEnumerator Recarga(float time)
    {
        yield return new WaitForSeconds(time);
        puedeDisparar=true;
        animador.SetBool("estaEnReposo?",false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag=="Player" && collider.name.Contains("Bala A"))
        {
            salud-=1;
            Destroy(collider.gameObject);
        }
    }
    IEnumerator EliminarSoldado(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(contenedorSoldado);
    }
}
