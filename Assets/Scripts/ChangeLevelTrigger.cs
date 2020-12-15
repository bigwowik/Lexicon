using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeLevelTrigger : MonoBehaviour
{
    public string nextLevelName;

    public Transform spawnPosition;



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SavePosition(other.name);
            SceneManager.LoadScene(nextLevelName);
        }

    }

    public void SavePosition(string playerName)
    {
        string keyX = playerName + " " + SceneManager.GetActiveScene().name + " " + "X";
        string keyY = playerName + " " + SceneManager.GetActiveScene().name + " " + "Y";
        string keyZ = playerName + " " + SceneManager.GetActiveScene().name + " " + "Z";



        PlayerPrefs.SetFloat(keyX, spawnPosition.position.x);
        PlayerPrefs.SetFloat(keyY, spawnPosition.position.y);
        PlayerPrefs.SetFloat(keyZ, spawnPosition.position.z);

        Debug.LogFormat("On scene {0} last position was {1} {2} {3}", SceneManager.GetActiveScene().name, spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z);



    }
}
