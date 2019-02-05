using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torreta : MonoBehaviour
{
    
    float distanciaAJugador;
    public GameObject jugador;
    public Animator Animador;

    public GameObject torretaBIntacta;
    public GameObject torretaBDestruida;

    public GameObject torretaBEscombros;

    public GameObject torretaBCañon;

    int controlTorretaB=0;
    
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Contenedor Jugador");
    }

    // Update is called once per frame
    void Update()
    {
        distanciaAJugador = Vector3.Distance(jugador.transform.position, transform.position); 
        if (distanciaAJugador<1)
        {
            Animador.SetBool("estaActivo?",true);
        }
       
        if (gameObject.name.Contains("B"))
        {
            TorretaB();
        }
    }

    void TorretaB()
    {

        if (controlTorretaB>=3)
        {

            torretaBIntacta.SetActive(false);
            torretaBDestruida.SetActive(true);

        }

        if (controlTorretaB>=5)
        {
            torretaBCañon.SetActive(false);
            torretaBIntacta.SetActive(false);
            torretaBDestruida.SetActive(false);
            torretaBEscombros.SetActive(true);
            gameObject.GetComponent<BoxCollider2D>().enabled=false;

        }
 
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (gameObject.name.Contains("B")&&collider.name.Contains("Bala"))
        {
            Destroy(collider.gameObject);
            controlTorretaB+=1;
        }   
    }
}
