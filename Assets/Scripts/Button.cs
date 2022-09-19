using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    /// <summary> Exitボタンの処理/// </summary>
    public void OnExitButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
    /// <summary> Exitボタンの処理/// </summary>
    public void OnRestartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
