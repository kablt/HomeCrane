using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void FirstScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SecondScene()
    {
        SceneManager.LoadScene("ExitCraneScene");
    }
}
