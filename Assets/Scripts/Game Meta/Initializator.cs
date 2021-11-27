using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializator : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(Serializator.GetData(DataName.CurrentLevel));
    }

    public void Reset()
    {
        Serializator.ResetValues();
    }
}
