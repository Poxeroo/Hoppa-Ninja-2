using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorGenerador : MonoBehaviour
{
    //esto es para ingreasar que es lo que generare
    public GameObject[] Enemigos;
    //el tiempo de generacion de enemigos
    [Range(1f, 10f)]
    public float TiempoGeneracion = 2f;
    [Range(0f, 10f)]
    public float TiempoReaccion = 2f;

    // Start is called before the first frame update
    void Start()
    {
        //Enemigos[1].transform.position = new Vector2(9.50f, -1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy()
    {
        int opcion = Random.Range(0, 2);
        //instacion lo que voy a generar y donde lo generare 
        Instantiate(Enemigos[opcion], transform.position, Quaternion.identity); // ni puta idea de que es el quaternion
        
    }

    //inicio la invocacion
    public void IniciarGenerador()
    {

        InvokeRepeating("CreateEnemy", TiempoReaccion, TiempoGeneracion);

    }


    //termino la invocacion
    public void CancelarGenerador(bool Limpiar = false)
    {

        CancelInvoke("CreateEnemy");
        if (Limpiar)
        {
            object[] Enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
            foreach(GameObject enemigo in Enemigos)
            {
                Destroy(enemigo);
            }

        }

    }
}
