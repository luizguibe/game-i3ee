using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Animator animator; //Animator ligado ao player.
    private Rigidbody2D rb; //Rigidbody2D associado ao player

    //Move variables
    [SerializeField] private float speed = 4f;
    private float move_input;

    //Jump variables
    [SerializeField] private float jump_force = 6f;
    private bool is_grounded;
    private float coyote_time = 0.1f;
    private float coyote_time_counter;
    private float jump_buffer_time = 0.1f;
    private float jump_buffer_time_counter;
    private float count_jump_timer;
    [SerializeField] private float jump_time = 0.3f;
    private bool is_jumping;
    public Transform feet_position; //GameObject nos péss do player para verificar o chão.
    private float check_radius = 0.2f;
    public LayerMask what_is_ground; //Layer para setar o chão.

    //Crouch variables
    public Transform check_overhead_colision; //GameObject na cabeçaa do player para verificar se tem algo sólido em cima.
    private float overhead_radius = 0.2f;
    private bool chrouch_sinalizator;
    public Collider2D not_chrouching; //Colisor normal do player.
    public Collider2D chrouching; //Colisor menor para quando o player agachar.

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //Time.timeScale = 0.3f; //Para testar as animações
    }
    void Update()
    {
        animations();
        is_grounded = Physics2D.OverlapCircle(feet_position.position, check_radius, what_is_ground);
        look_direction();
        jump();
    }
    void FixedUpdate()
    {
        crouch();
        move();
    }

    void move()
    {
        move_input = Input.GetAxisRaw("Horizontal");
        //Verificando se o player está agachado, se tiver, diminui a velocidade pela metade.
        if(!chrouch_sinalizator || is_jumping == true)
            rb.velocity = new Vector2(move_input * speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(move_input * (speed / 2), rb.velocity.y);
    }

    void jump()
    {
        //Verificando se o player esta no chao e setando o espaço como tecla pra pular.
        if (coyote_time_counter > 0f && jump_buffer_time_counter > 0f)
        {
            is_jumping = true;
            chrouch_sinalizator = false;
            count_jump_timer = jump_time;
            rb.velocity = Vector2.up * jump_force;
            jump_buffer_time_counter = 0f;
        }
        if(is_grounded)
        {
            coyote_time_counter = coyote_time;
        }
        else
        {
            coyote_time_counter -= Time.deltaTime;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump_buffer_time_counter = jump_buffer_time;
        }
        else
        {
            jump_buffer_time_counter -= Time.deltaTime;
        }
        //Verificando se o player esta presionando tecla de espaço, se tiver, aumenta a altura do pulo.
        if (Input.GetKey(KeyCode.Space) && is_jumping == true)
        {
            if (count_jump_timer > 0)
            {
                rb.velocity = Vector2.up * jump_force;
                count_jump_timer -= Time.deltaTime;
            }
            else
            {
                is_jumping = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            is_jumping = false;
            coyote_time_counter = 0f;
        }
    }

    void crouch()
    {
        //Setando S como botão para agachar e iniciando sinalizador de agachamento.
        if(Input.GetKey(KeyCode.S))
        {
            chrouch_sinalizator = true;
        }
        else
        {
            chrouch_sinalizator = false;
        }

        //Trocando o colisor normal pelo colisor menor quando o sinalizador de agachamento for positivo.
        not_chrouching.enabled = !chrouch_sinalizator;
        chrouching.enabled = chrouch_sinalizator;

        //Checando se quando o player for levantar, vai ter algo sólido em cima, se tiver, mantém o estado de agachado.
        if(chrouching.enabled == false)
        {
            if(Physics2D.OverlapCircle(check_overhead_colision.position, overhead_radius, what_is_ground)) 
            {
                not_chrouching.enabled = false;
                chrouching.enabled = true;
            }
        }
    }

    void look_direction()
    {
        //Flipando player de acordo com a direção.
        if (move_input > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (move_input < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void animations()
    {
        animator.SetFloat("horizontal_move", Mathf.Abs(move_input));
        animator.SetBool("is_grounded", is_grounded);
        animator.SetBool("Is_jumping", is_jumping);

        animator.SetFloat("vertical_move", rb.velocity.y);
    }
}