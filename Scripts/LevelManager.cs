using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string nextSceneName;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("script executed222!");
        if (other.CompareTag("Player"))
        {
            Debug.Log("script executed!");
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void SwitchLevel()
    {
        Debug.Log("script executed!");
        SceneManager.LoadScene(nextSceneName);
    }
}