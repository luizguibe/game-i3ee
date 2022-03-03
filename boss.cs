using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inifollow : MonoBehaviour
{

    public float speed;
    private Transform Target; //alvo
     public float StoppingDistance; // nao coloque uma distancia muito alta.
      private Transform GroundCheck;
     private BoxCollider2D colisor;
     private float JumpForce;
     private RigidBody2D rig;

   




    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //coloque dentro das aspas o  player que o inimigo quer seguir.Eh so ir em inspector ,tag  e configurar como player.
        rig = GetComponent<RigidBody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, Target.position) > StoppingDistance){
            transform.position = Vector2.MoveTowards(transform.position, Target.position, speed*Time.deltaTime);
        }
        if(Grounded()){
            rig.velocity = Vector2.Up*JumpForce;

        }

    }
      bool Grounded()
    {
        RaycastHit2D ground = Physics2D.Raycast(colisor.bounds.center, colisor.bounds.size, 0 ,Vector2.down, 0.1f);
        return ground.collider != null;
    }
}
