using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cria : MonoBehaviour
{//velocidade do rato : 13 , velocidade do esqueleto : 8.5
    public float speed;
    public float distance;
    bool IsRight = true;

    public Transform GroundCheck;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D ground = Physics2D.Raycast(GroundCheck.position, Vector2.down, distance);

        if(ground.collider == false){
            if(IsRight == true){
                transform.eulerAngles = new Vector3(0, 0, 0);
                IsRight = false;

            }
            else{
                transform.eulerAngles = new Vector3(0, 180, 0);
                IsRight = true;


            }
            

        }



    }
}
