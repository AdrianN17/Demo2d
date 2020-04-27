using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Personaje : MonoBehaviour
{
    public float velocidad;
    public float salto;
    public bool ground;

    private bool saltar;
    private bool saltarEnemigo;

    public int score;
    public int vida;

    public Rigidbody2D rb;
    public BoxCollider2D col;
    public Animator anim;
    public SpriteRenderer sprite;
    public Text scoreText;

    private Timer timerScore;

    
    


    private enum movimiento {ninguno,derecha,izquierda };
    private movimiento movimientoPlayer =  movimiento.ninguno;

    private Vector2 dir;



    // Start is called before the first frame update
    void Start()
    {
        saltar = false;
        saltarEnemigo = false;

        timerScore = Timer.Register(0.25f,() => scoreText.text = "Score : " + score, isLooped: true);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dir = new Vector2(-1, 0);
            movimientoPlayer = movimiento.izquierda;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            dir = new Vector2(1, 0);
            movimientoPlayer = movimiento.derecha;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && ground)
        {
            saltar = true;
        }


        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) || 
            ((Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))))
        {
            dir = new Vector2(0, 0);
            movimientoPlayer = movimiento.ninguno;
        }


        
    }

    void FixedUpdate()
    {
        float dt = Time.deltaTime;

        if(saltar && vida>0)
        {

            rb.AddForce(Vector2.up * salto);

            anim.SetTrigger("Saltar");

            saltar = false;
        }

        if (movimientoPlayer != movimiento.ninguno)
        {
            rb.AddForce(dir * dt*velocidad);

            anim.SetBool("Correr", true);

            if(movimientoPlayer == movimiento.izquierda)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }    

        }
        else
        {
            anim.SetBool("Correr",false);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            score++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Vida"))
        {
            vida++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Diamante"))
        {
            score=score+10;
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            var y = collision.contacts[0].normal.y;

            if ( y>0.9 && y<1.1)
            {
                if(!saltarEnemigo)
                {
                    rb.AddForce(salto * Vector2.up);
                    saltarEnemigo = true;

                    Timer.Register(0.25f,()=> saltarEnemigo = false);
                }

                Destroy(collision.gameObject);
                    
            }
            else
            {
                float dir = Mathf.Sign(collision.contacts[0].normal.x);
                rb.AddForce(salto * new Vector2(dir, 0));

                vida--; 
                
                if(vida<1)
                {
                    col.isTrigger = true;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            var y = collision.contacts[0].normal.y;

            if (y == 1)
            {
                ground = true;
                anim.SetBool("Tierra", true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ground = false;
            anim.SetBool("Tierra", false);
        }
    }

    private void OnDestroy()
    {
        timerScore.Cancel();
    }
}
