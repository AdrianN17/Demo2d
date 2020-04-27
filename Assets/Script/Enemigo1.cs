using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1 : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public BoxCollider2D col;
    public SpriteRenderer sprite;
    public Animator anim;

    private Timer timerPared;
    private Timer timerSuelo;

    public float anguloSuelo;
    public float anguloPared;

    public float distanciaSuelo;
    public float distanciaPared;
    public float velocidad;

    public Vector2 vecSuelo;
    public Vector2 vecPared;

    public float direccion = -1;


    void Start()
    {
        timerPared = Timer.Register(0.25f, verificarPared, isLooped: true);
        timerSuelo = Timer.Register(0.01f, verificarSuelo, isLooped: true);

        vecSuelo = MathHelpers.DegreeToVector2(anguloSuelo, 1);
        vecPared = MathHelpers.DegreeToVector2(anguloPared, 1);
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;


        rb.AddForce(new Vector2(direccion, 0) * dt * velocidad);

        Debug.DrawRay(col.bounds.center, vecSuelo, Color.red);
        Debug.DrawRay(col.bounds.center, vecPared, Color.red);
    }

    public void verificarPared()
    {
        var hit = Physics2D.Raycast(col.bounds.center, vecPared, distanciaPared, LayerMask.GetMask("Ground"));

        if (hit)
        {
            obtenerDireccion();
        }
    }

    public void verificarSuelo()
    {
        var hit = Physics2D.Raycast(col.bounds.center, vecSuelo, distanciaSuelo, LayerMask.GetMask("Ground"));

        if (!hit)
        {
            obtenerDireccion();
        }

    }

    public void obtenerDireccion()
    {
        sprite.flipX = !sprite.flipX;

        direccion = direccion * -1;

        vecSuelo.x = vecSuelo.x * -1;
        vecPared.x = vecPared.x * -1;
    }

    

    private void OnDestroy()
    {
        timerSuelo.Cancel();
        timerPared.Cancel();
    }
}
