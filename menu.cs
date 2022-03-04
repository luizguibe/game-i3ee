using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class menu : MonoBehaviour
{
    public level_loader level_Loader;
    public string scene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start_game()
    {
        level_Loader.transition_scene(scene);
    }

    public void quit_game()
    {
        //Para rodar dentro da unity.
        UnityEditor.EditorApplication.isPlaying = false;
        
        //Para rodar quando o jogo for compilado.
        //Application.Quit();
    }
}
