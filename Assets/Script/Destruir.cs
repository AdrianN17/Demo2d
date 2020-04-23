using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destruir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemigo"))
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                Timer.Register(1, reiniciarEscena);
            }


                Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemigo"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Timer.Register(1, reiniciarEscena);
            }


            Destroy(collision.gameObject);
        }
    }

    public void reiniciarEscena()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }


}
