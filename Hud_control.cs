using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Hud_control : MonoBehaviour
{
    private Text seringa_count;

    public static Hud_control hud;

    public GameObject[] heart = new GameObject[3];

    void Start()
    {
        seringa_count = GameObject.FindGameObjectWithTag("UI_seringas").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        less_life();
        edit_seringa();
    }

    public void edit_seringa()
    {
        seringa_count.text = seringas.porcentagem_seringas.ToString("F") + "%";
    }

    public void less_life()
    {
        switch (player_movement.life)
        {
            case 3:
                heart[0].SetActive(true);
                heart[1].SetActive(true);
                heart[2].SetActive(true);
                break;
            case 2:
                heart[0].SetActive(true);
                heart[1].SetActive(true);
                heart[2].SetActive(false);
                break;
            case 1:
                heart[0].SetActive(true);
                heart[1].SetActive(false);
                heart[2].SetActive(false);
                break;
            default:
                heart[0].SetActive(false);
                heart[1].SetActive(false);
                heart[2].SetActive(false);
                break;
        }
    }
}
