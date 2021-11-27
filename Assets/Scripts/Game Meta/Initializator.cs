using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializator : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.LoadScene(Serializator.GetLevel());
    }
}
