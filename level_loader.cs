using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class level_loader : MonoBehaviour
{
    public Animator transition_anim;

    public void transition_scene(string scene_name)
    {
        StartCoroutine(load_scene(scene_name));
    }

    IEnumerator load_scene(string scene_name)
    {
        transition_anim.SetTrigger("in");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(scene_name);
    }
}
