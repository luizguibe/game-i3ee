using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    public Animator animator; //Animator ligado ao player.
    private Rigidbody2D rb; //Rigidbody2D associado ao player.
    [SerializeField] public BoxCollider2D player_collider;
    //Move variables
    [SerializeField] private float speed = 4f;
    private float move_input;

    //Jump variables
    [SerializeField] private float jump_force = 6f;
    private float coyote_time = 0.1f;
    private float coyote_time_counter;
    private float jump_buffer_time = 0.1f;
    private float jump_buffer_time_counter;
    private float count_jump_timer;
    [SerializeField] private float jump_time = 0.3f;
    private bool is_jumping;
    public LayerMask what_is_ground; //Layer para setar o chão.

    //Crouch variables
    public Transform check_overhead_colision; //GameObject na cabeçaa do player para verificar se tem algo sólido em cima.
    private float overhead_radius = 0.2f;
    public bool is_crouching { get; private set; }
    private float crouch_hight_percent = 0.5f;
    private Vector2 standing_collider_size;
    private Vector2 standing_collider_offset;
    private Vector2 crouch_collider_size;
    private Vector2 crouch_collider_offset;

    //Atack
    public static bool is_atacking;

    Hud_control hud;

    //Seringas
    public static int coleted_seringas = 0;

    //Lifes
    [SerializeField] public static int life = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //Time.timeScale = 0.3f; //Para testar as animações

        hud = Hud_control.hud;
    }
    void Update()
    {
        animations();
        look_direction();
        jump();
        atack();
    }
    private void Awake()
    {
        standing_collider_size = player_collider.size;
        standing_collider_offset = player_collider.offset;

        crouch_collider_size = new Vector2(standing_collider_size.x, standing_collider_size.y * crouch_hight_percent);
        crouch_collider_offset = new Vector2(standing_collider_offset.x, standing_collider_offset.y * crouch_hight_percent);
    }
    void FixedUpdate()
    {
        crouch();
        move();
    }

    private bool isGrounded()
    {
        RaycastHit2D ground = Physics2D.BoxCast(player_collider.bounds.center, player_collider.bounds.size, 0, Vector2.down, 0.1f, what_is_ground);

        return ground.collider != null;
    }

    void move()
    {
        move_input = Input.GetAxisRaw("Horizontal");
        //Verificando se o player está agachado, se tiver, diminui a velocidade pela metade.
        if (!is_crouching || is_jumping == true)
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
            is_crouching = false;
            count_jump_timer = jump_time;
            rb.velocity = Vector2.up * jump_force;
            jump_buffer_time_counter = 0f;
        }
        if (isGrounded())
            coyote_time_counter = coyote_time;
        else
            coyote_time_counter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
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
                is_jumping = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            is_jumping = false;
            coyote_time_counter = 0f;
        }
    }
    void atack()
    {
        if(Input.GetMouseButtonDown(0) && isGrounded())
        {
            is_atacking = true;
        }
        else
        {
            is_atacking = false;
        }
        if(Input.GetMouseButtonUp(0))
        {
            is_atacking = false;
        }
    }

    void crouch()
    {
        //Setando S como botão para agachar e iniciando sinalizador de agachamento.
        if (Input.GetKey(KeyCode.S))
        {
            is_crouching = true;
            player_collider.size = crouch_collider_size;
            player_collider.offset = crouch_collider_offset;
        }
        else
        {
            is_crouching = false;
            player_collider.size = standing_collider_size;
            player_collider.offset = standing_collider_offset;
        }

        //Checando se quando o player for levantar, vai ter algo sólido em cima, se tiver, mantém o estado de agachado.
        if (is_crouching == false && isGrounded())
        {
            if (Physics2D.OverlapCircle(check_overhead_colision.position, overhead_radius, what_is_ground))
            {
                is_crouching = true;
                player_collider.size = crouch_collider_size;
                player_collider.offset = crouch_collider_offset;
            }
        }
    }

    void look_direction()
    {
        //Flipando player de acordo com a direção.
        if (move_input > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (move_input < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);
    }

    void animations()
    {
        animator.SetFloat("horizontal_move", Mathf.Abs(move_input));
        animator.SetBool("is_grounded", isGrounded());
        animator.SetBool("Is_jumping", is_jumping);
        animator.SetBool("is_crouching", is_crouching);
        animator.SetBool("atack", is_atacking);

        animator.SetFloat("vertical_move", rb.velocity.y);
    }
}
