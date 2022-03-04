using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaeDano : MonoBehaviour
{
    public int Vida;
    public int VidaPlayer;

    // Start is called before the first frame update
    void Start()
    {
        VidaPlayer = gameObject.Find("Player").GetComponent<player_movement>().life;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collider ){
        if(collider.gameObject.tag == "DanoPersonagem")
        {
            Vida--;
        }
        if(collider.gameObject.tag == "Player")
        {
            VidaPlayer--;

        }
    }
    void Morte(){
        if(Vida == 0)
        {
            Object.Destroy(this.gameObject)
        }
    }
}
