using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controladorEnemigo : MonoBehaviour
{
    [Range(0f, 10f)] 
    public float VelocidadEnemigo = 3f;

    private Rigidbody2D rigidB2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidB2D = GetComponent<Rigidbody2D>();
        // rigidB2D.velocity = Vector2.left * VelocidadEnemigo;// el velocity es como se reconoce la velocidad de rogidbody2d 

        if (this.gameObject.tag == "Cuervo")
        {

            //this.GetComponent<Rigidbody2D>().velocity =
            this.gameObject.transform.position = new Vector3(13.50f, -0.2f,-1f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Enemigo")
        {
            transform.Translate(Vector2.left * VelocidadEnemigo * Time.deltaTime);
        }
        if (this.gameObject.tag == "Cuervo")
        {



            //this.GetComponent<Rigidbody2D>().velocity =
            //is.gameObject.transform.position = new Vector2(9.50f,-1.3f);
            transform.Translate(Vector2.left * VelocidadEnemigo * Time.deltaTime);
            
        }
    }

    void OnTriggerEnter2D(Collider2D colision)//sobre escribimos esta funcion osea que obligatoria mente se debe llamar asi
    {
        //no obligatoria mente este se debe destruir pero se hace de buena practicar para no explotar las PCs
        if (colision.gameObject.tag == "Destructor")
        {
            Destroy(gameObject);//Destruyo al enemigo al chocar con el box collider colocado atras del personaje
        }

    }



}
