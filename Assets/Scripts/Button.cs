using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    /// <summary> Exit�{�^���̏���/// </summary>
    public void OnExitButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
    /// <summary> Exit�{�^���̏���/// </summary>
    public void OnRestartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
