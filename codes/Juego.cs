using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum EstadosDeJuego { Quieto, Jugando, Muerte, ListoIniciar };// aqui hago una lista de todos los estados de juego que hay

public class Juego : MonoBehaviour{

    [Range (0f, 1f)] //esto generara una barra en unity para seleccionar la velocidadParalax y no se debe cerrar
    public float velocidadParalax = 0.02f; //f se coloca para simbolizar la fuerza ya que son objetos 
    public RawImage fondo; //los creo para poder modificarlos
    public RawImage plataforma;
    public GameObject PantallaInicio;

    public EstadosDeJuego estadosDeJuego = EstadosDeJuego.Quieto; // defino el estado Quieto como el predeterminado

    public GameObject Personaje; //declaro al jugador como un objeto

    public GameObject GeneradorEnemigos; // declaro el generador como un objeto

    public float SubirTiempo = 4f; // declaro el tiempo en que se subire la velocidad

    public GameObject PantallaScore; // declaro como un objeto la pantalla de puntaje
    public Text PuntosPantalla; // declaro como texto los puntos en pantalla 
    private int Puntos = 0; // declaro los puntos y les otorgo un valor inicial
    public Text MejorPuntuacion; // declaro como texto la mejor puntuacion

    public GameObject ReinicioPantalla;

    // Start is called before the first frame update
    void Start()
    {   //desactivo la pantalla de score al iniciar el juego para iniciarla luego cuando el juego inicia
        PantallaScore.SetActive(false);
        //concateno el texto "Best: " con el Score mas alto guardado y lo convierto en string 
        MejorPuntuacion.text = "Best: " + GetMaxScore().ToString();

        ReinicioPantalla.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Inicio del juego pulsando los 3 botones colocados y tambien si el personaje esta quieto
        if (estadosDeJuego == EstadosDeJuego.Quieto && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")))
        {
            estadosDeJuego = EstadosDeJuego.Jugando;// cambio el estado de juego
            PantallaInicio.SetActive(false); // desaparezco la pantalla de inicio
            PantallaScore.SetActive(true); // activo la pantalla de score
            Personaje.SendMessage("CambiarEstado", "PersonajeCorriendo"); //le mando un mensaje a mi personaje para que se ponga  a correr
            GeneradorEnemigos.SendMessage("IniciarGenerador"); // recuerda que lo que esta entre "" es lo que estamos llamando eso no se pone porque si
            
            //le voy subiendo la dificultad al juego cada 5 segundos
            InvokeRepeating("JuegoSubirVelocidad", SubirTiempo, SubirTiempo);

        }

        //arranque de partida
        else if (estadosDeJuego == EstadosDeJuego.Jugando)
        {
            Paralax(); // cuando el estado sea jugando el efecto parallax se activara
        }
        else if (estadosDeJuego == EstadosDeJuego.Muerte)
        {
           ReinicioPantalla.SetActive(true);
        }

        //Final de partida
        else if (estadosDeJuego == EstadosDeJuego.ListoIniciar)
        {   // cuando muera pasara un momento para que pueda reiniciar y se reiniciara presionando 1 deestas 3 teclas
            if(Input.GetKeyDown("up") || Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
            {
                ReiniciarJuego();
            }
        }
    }

    void Paralax()
    {
        // realizo el efecto paralax
        float velocidadfinal = velocidadParalax * Time.deltaTime; // esto es para limitar la velocidad por los diferentes tipos de pc(potentes o mier....)
        fondo.uvRect = new Rect(fondo.uvRect.x + velocidadfinal, 0f, 1f, 1f); //  esto es para hacer que se mueva a una velocidad 
        plataforma.uvRect = new Rect(plataforma.uvRect.x + velocidadfinal * 2, 0f, 1f, 1f); // a este le pongo el x 2 para que la plataforma vaya mas rapido que el fondo 
    }

    public void ReiniciarJuego()
    {   // reinicio el juego  
        SceneManager.LoadScene("Principal"); // vuelvo a la escena principal la cual declare asi
    }

    void JuegoSubirVelocidad()
    {   // voy subiendo la velocidad hasta el maximo de 2.3
        if (Time.timeScale < 2.49f)
        {
            Time.timeScale += 0.1f;
        }
        if(Time.timeScale == 2.50f)
        {   // hago que ya deje de subir la velocidad cuando sea 2.3
            CancelInvoke("JuegoSubirVelocidad");
        }
        //esto es para que la velocidad se vea en consola
        Debug.Log("velocidad: "+ Time.timeScale.ToString());
    
    }

    public void ResetearTiempo()
    {
        //cuando mueres se resetea el tiempo pues eso lo hago aqui
        CancelInvoke("JuegoSubirVelocidad"); // cancelo que se siga subiendo la velocidad
        Time.timeScale = 1f; // hago que la velocidad vuelva a ser 1
        Debug.Log("reseteado: " + Time.timeScale.ToString());
    }

    public void IncrementarPuntos()
    {
        PuntosPantalla.text = (++Puntos).ToString();
        if(Puntos >= GetMaxScore())
        {
            MejorPuntuacion.text = "Best: " + Puntos.ToString();
            GuardarScore(Puntos);
        }
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("Mayor Puntaje", 0);
    }

    public void GuardarScore(int PuntajeActual)
    {
        PlayerPrefs.SetInt("Mayor Puntaje", PuntajeActual);
    }

}
