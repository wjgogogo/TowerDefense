using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public Animator anim;

    public void SetButtonState(string name, bool state)
    {
        anim.SetBool(name, state);
    }

    public void OnStartBtnDown()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitBtnDown()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}