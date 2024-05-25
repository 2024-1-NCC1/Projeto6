using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float laneSpeed;
    private Rigidbody rb;
    private int currentLane = 0;
    private Vector3 verticalTargetPosition;
    private bool jumping = false;
    private float jumpStart;
    private Animator anim;
    public float jumpLength;
    public float jumpHeight;
    private bool sliding = false;
    private float slideStart;
    public float slideLength;
    private BoxCollider boxCollider;
    private Vector3 boxColliderSize;
    private int score = 0;
    public Track.Coletavel coletavel1;
    public GameObject somDano;
    public GameObject somVida;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = boxCollider.size;
        anim.Play("runStart");
    }

    void Update()
    {
        // Verifica os comandos de movimento lateral
        if (Input.GetKeyDown(KeyCode.A))
        {
            ChangeLane(-1); // Move duas pistas para a esquerda
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ChangeLane(1); // Move duas pistas para a direita
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Slide();
        }

        // Lógica de pulo e deslize
        UpdateJumpAndSlide();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.forward * speed;
    }

    void ChangeLane(int direction)
    {
        int targetLane = currentLane + direction;
        if (targetLane < 0 || targetLane > 2)
            return;
        currentLane = targetLane;
        verticalTargetPosition = new Vector3((currentLane - 1), 0, 0);
    }

    void Jump()
    {
        if (!jumping)
        {
            jumpStart = transform.position.z;
            anim.SetFloat("JumpSpeed", speed / jumpLength);
            anim.SetBool("Jumping", true);
            jumping = true;
        }
    }

    void Slide()
    {
        if (!jumping && !sliding)
        {
            slideStart = transform.position.z;
            anim.SetFloat("JumpSpeed", speed / slideLength);
            anim.SetBool("Sliding", true);
            Vector3 newSize = boxCollider.size;
            newSize.y = newSize.y / 2;
            boxCollider.size = newSize;
            sliding = true;
        }
    }

    void UpdateJumpAndSlide()
    {
        if (jumping)
        {
            float ratio = (transform.position.z - jumpStart) / jumpLength;
            if (ratio >= 1f)
            {
                jumping = false;
                anim.SetBool("Jumping", false);
            }
            else
            {
                verticalTargetPosition.y = Mathf.Sin(ratio * Mathf.PI) * jumpHeight;
            }
        }
        else
        {
            verticalTargetPosition.y = Mathf.MoveTowards(verticalTargetPosition.y, 0, 5 * Time.deltaTime);
        }

        if (sliding)
        {
            float ratio = (transform.position.z - slideStart) / slideLength;
            if (ratio >= 1)
            {
                sliding = false;
                anim.SetBool("Sliding", false);
                boxCollider.size = boxColliderSize;
            }
        }

        // Movimento em direção ao alvo vertical (troca de pistas)
        Vector3 targetPosition = new Vector3(verticalTargetPosition.x, verticalTargetPosition.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, laneSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("Saudavel"))
    {
        Barradevida barraDeVida = GetComponent<Barradevida>(); 
        if (barraDeVida != null)
        {
            barraDeVida.ReceberDano(-10);
            somVida.GetComponent<AudioSource>().Play();

        }
        IncreaseSpeed();
        Destroy(other.gameObject);

    }
    else if (other.CompareTag("Alimento"))
    {
        Barradevida barraDeVida = GetComponent<Barradevida>(); 
        if (barraDeVida != null)
        {
            barraDeVida.ReceberDano(10);
            

        }
        DecreaseSpeed();
        somDano.GetComponent<AudioSource>().Play();
        Destroy(other.gameObject);
    }  
    }
    

    void IncreaseSpeed()
    {
        speed *= 1.2f; // Aumenta a velocidade em 20%
    }

    void DecreaseSpeed()
    {
        speed *= 0.8f; // Diminui a velocidade em 20%
    }
}