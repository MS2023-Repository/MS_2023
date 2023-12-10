using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    [SerializeField] GameObject[] managerList;

    private void Start()
    {
        foreach (GameObject managerObj in managerList)
        {
            if (GameObject.Find(managerObj.name + "(Clone)") == null)
            {
                string sceneName = SceneManager.GetActiveScene().name;
                if (sceneName == "Title" && sceneName == "StageSelect")
                {
                    if (managerObj.name != "GameManager")
                    {
                        Instantiate(managerObj);
                    }
                }
                else
                {
                    Instantiate(managerObj);
                }
            }
        }
    }
}