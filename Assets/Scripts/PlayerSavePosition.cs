using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSavePosition : MonoBehaviour
{
    
    //первый запуск
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoadRuntimeMethod()
    {
        Debug.Log("All player prefs was deleted.");
        PlayerPrefs.DeleteAll();
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadPosition();
    }
    
    
    public void LoadPosition()
    {
        string keyX = name + " " + SceneManager.GetActiveScene().name + " " + "X";
        string keyY = name + " " + SceneManager.GetActiveScene().name + " " + "Y";
        string keyZ = name + " " + SceneManager.GetActiveScene().name + " " + "Z";


        if (PlayerPrefs.HasKey(keyX))
        {

            Vector3 newPosition = new Vector3(
                PlayerPrefs.GetFloat(keyX, transform.position.x),   //добавление к позициии пути который пройдет игрок за 1 секуднку в направлении движения во время сохр
                PlayerPrefs.GetFloat(keyY, transform.position.y),
                PlayerPrefs.GetFloat(keyZ, transform.position.z)
                );

            transform.position = newPosition;
            Debug.LogFormat("Position was loaded with key {0} and {1} .", newPosition.x, newPosition.y);
        }
        else
        {
            Debug.LogFormat("Position with key {0} was not found.", keyX);
            transform.position = FindNearSpawnPoint();


        }
    }

    Vector3 FindNearSpawnPoint()
    {
        var spawns = GameObject.FindGameObjectsWithTag("SpawnPoint");

        if (spawns.Length > 0)
        {
            Debug.Log("spawn is " + spawns.Length);
            var nearPoint = spawns[0];
            foreach(GameObject point in spawns)
            {
                if(Vector3.Distance(transform.position, point.transform.position) < Vector3.Distance(transform.position, nearPoint.transform.position)) //если новая точка ближе
                {
                    nearPoint = point;
                }
            }

            return nearPoint.transform.position;
        }
        else
        {
            return transform.position;
        }

    }

}
