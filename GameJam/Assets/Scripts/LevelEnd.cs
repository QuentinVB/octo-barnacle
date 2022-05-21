using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    string nextLevel;

    private void OnTriggerEnter(Collider other)
    {
       SceneManager.LoadScene(nextLevel);
    }
}
