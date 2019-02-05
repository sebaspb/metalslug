using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Jugador : MonoBehaviour
{
    [Header("<VARIABLES MOVIMIENTO>")]  
    [Tooltip("Velocidad con la cual se moverá el jugador horizontalmente, valor por defecto 0.7f")]
    public float velocidadDeMovimiento = 0.7f;

    [Tooltip("Animador del torso del jugador")]
    public Animator animadorTorso;
    [Tooltip("Animador de las piernas del jugador")]
    public Animator animadorPiernas;
    [Tooltip("Capa usada para encontrar el suelo.")]
    public LayerMask capaSuelo;


    [Header("<VARIABLES PRIVADAS>")]
    [Tooltip("Rigidbody 2D del jugador.")]
    private Rigidbody2D rigidBody;
    [Tooltip("Variable que almacena la dirección del movimiento hacia la izquierda o derecha")]
    private float direccionMovimiento = 0f;
    [Tooltip("Variable que almacena si el jugador está en el suelo, o no.")]
    private bool estaEnElSuelo;
    private bool estaDisparando = false;

    public static float salud = 5f;

    public Rigidbody2D BalaA;
    public Transform spawnbala;
    public GameObject contenedorPersonaje;


    void Start()
    {
        
        rigidBody = GetComponent<Rigidbody2D> ();
        
    }

    void Update()
    {
        direccionMovimiento = Input.GetAxisRaw ("Horizontal");
        estaEnElSuelo = Physics2D.OverlapArea (new Vector2(transform.position.x - 0.5f, transform.position.y - 0.5f),
        new Vector2 (transform.position.x + 0.5f, transform.position.y + 0.5f), capaSuelo);  
        MovimientoJugador();
       

       if(salud<=0)
       {
           Destroy(contenedorPersonaje.gameObject);
       }
                
    }

    void MovimientoJugador()
    {

        
        if (direccionMovimiento > 0)
        {
            if(!estaDisparando)
            {
                animadorTorso.SetBool("Caminando",true);
            }

            animadorPiernas.SetBool("Caminando",true);
            
            
            rigidBody.velocity = new Vector2 (direccionMovimiento * velocidadDeMovimiento, rigidBody.velocity.y);
            
            contenedorPersonaje.transform.rotation = Quaternion.Euler(0, 0f, 0);
        }

        else if (direccionMovimiento < 0) 
        {
             if(!estaDisparando)
             {
                animadorTorso.SetBool("Caminando",true);
             }

            animadorPiernas.SetBool("Caminando",true);
            rigidBody.velocity = new Vector2 (direccionMovimiento * velocidadDeMovimiento, rigidBody.velocity.y);
            contenedorPersonaje.transform.rotation = Quaternion.Euler(0, 180f, 0);
        } 

        else 
        {
            animadorTorso.SetBool("Caminando",false);
            animadorPiernas.SetBool("Caminando",false);
            rigidBody.velocity = new Vector2 (0, rigidBody.velocity.y);
        }

       
        if (Input.GetButtonDown("Fire1"))
        {
            estaDisparando=true;
            animadorTorso.SetBool("Disparando",true);

            if(contenedorPersonaje.transform.rotation == Quaternion.Euler(0, 0f, 0))
            {

                Rigidbody2D bulletInstance = Instantiate(BalaA, spawnbala.position,spawnbala.rotation);
                bulletInstance.AddForce(new Vector2(120*Time.deltaTime,0), ForceMode2D.Impulse);
        
            }

            if(contenedorPersonaje.transform.rotation == Quaternion.Euler(0, 180f, 0))
           
            {
                Rigidbody2D bulletInstance = Instantiate(BalaA, spawnbala.position,spawnbala.rotation);
                bulletInstance.AddForce(new Vector2(-120*Time.deltaTime,0), ForceMode2D.Impulse);
            }

        }
        
        if (Input.GetButtonUp("Fire1"))
        {

            estaDisparando=false;
            animadorTorso.SetBool("Disparando",false);

        }

        if(estaEnElSuelo)
        {

             if (Input.GetButtonDown("Jump"))
            {
                animadorTorso.SetBool("Saltando",true);
                animadorPiernas.SetBool("Saltando",true);
                rigidBody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
                StartCoroutine(FinSalto(0.2f));
            }
        }
    }
 IEnumerator FinSalto(float time)
    {
        yield return new WaitForSeconds(time);
        animadorTorso.SetBool("Saltando",false);
        animadorPiernas.SetBool("Saltando",false);
    }

 void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.name.Contains("Cohete"))
        {
            
            Jugador.salud-=proyectiles.dañoCohete;
            Debug.Log(Jugador.salud);
            Destroy(collider.gameObject);
            
        }
        
    }
}
