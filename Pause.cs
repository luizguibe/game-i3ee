using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] public Transform pause_menu;
    public string scene;
    private bool is_paused;
    public level_loader level_Loader;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pause();
    }

    private void pause()
    {
         if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pause_menu.gameObject.activeSelf)
            {
                pause_menu.gameObject.SetActive(false);
                Time.timeScale = 1;
                is_paused = true;
            }
            else
            {
                pause_menu.gameObject.SetActive(true);
                Time.timeScale = 0;
                is_paused = false;
            }
        }
    }

    public void resume()
    {
        pause_menu.gameObject.SetActive(false);
        Time.timeScale = 1;
        is_paused = false;
    }
    
    public void back_to_menu()
    {
        is_paused = false;
        Time.timeScale = 1f;

        level_Loader.transition_scene(scene);
    }
}
