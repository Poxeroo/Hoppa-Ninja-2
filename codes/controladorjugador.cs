using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controladorjugador : MonoBehaviour
{

    public GameObject juego;
    public GameObject generador;
    public AudioClip PersonajeSaltando;
    public AudioClip PersonajeMuriendo;
    public AudioClip PersonajeDeslizandose;

    private Animator animator;
    private AudioSource Efectos;
    private float Pisando;

    void Start()
    {
        animator = GetComponent<Animator>(); // cuando inicie el juego detectara el animador
        Efectos = GetComponent<AudioSource>();
        Pisando = transform.position.y; // como lo inicio en el start toma el valor del inicio del juego 
    }

    void Update()
    {
        bool EnPiso = transform.position.y == Pisando;// aqui comparo el valor del jugador en el momento y el valor del inicio
        bool jugando = juego.GetComponent<Juego>().estadosDeJuego == EstadosDeJuego.Jugando;
        if (EnPiso && jugando && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")))
        {
            CambiarEstado("PersonajeSaltando");
            Efectos.clip = PersonajeSaltando;
            Efectos.Play();

        }
        if(EnPiso && jugando && (Input.GetKeyDown("down") || Input.GetMouseButtonDown(1)))
        {
            CambiarEstado("PersonajeDeslizandose");
            Efectos.clip = PersonajeDeslizandose;
            Efectos.Play();

        }
    }

    public void CambiarEstado(string Estado = null)
    {   
        //Esto cambiara los estados al encenderse

        if (Estado != null)
        {
            animator.Play(Estado);
        }
    }

    void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.gameObject.tag == "Enemigo" || Enemy.gameObject.tag == "Cuervo")
        {
            CambiarEstado("PersonajeMuriendo");
            juego.GetComponent<Juego>().estadosDeJuego = EstadosDeJuego.Muerte;
            generador.SendMessage("CancelarGenerador", true);
            juego.SendMessage("ResetearTiempo");

            //aqui llamo a los efectos de sonido
            juego.GetComponent<AudioSource>().Stop();
            Efectos.clip = PersonajeMuriendo;
            Efectos.Play();

        }
        else if (Enemy.gameObject.tag == "Puntuacion") {
            Debug.Log("Suma");
            juego.SendMessage("IncrementarPuntos");

        }

    }

    public void JuegoListoIniciar()
    {
        juego.GetComponent<Juego>().estadosDeJuego = EstadosDeJuego.ListoIniciar;
    }

    
}
