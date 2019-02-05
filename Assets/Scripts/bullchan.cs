using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullchan : MonoBehaviour
{
    float distanciaAJugador;
    public GameObject jugador;
    public Animator Animador;

    Transform transformTanque;
    // Start is called before the first frame update
    void Start()
    {
        Animador=gameObject.GetComponent<Animator>();
        transformTanque=transform;
    }

    // Update is called once per frame
    void Update()
    {
       distanciaAJugador = Vector3.Distance(jugador.transform.position, transform.position); 

       if (distanciaAJugador<1)
       {
            Animador.SetBool("estaActivo?",true);
            transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(transformTanque.transform.position.x, 0.6f ), Time.deltaTime * 0.2f);
       }
    }
}
