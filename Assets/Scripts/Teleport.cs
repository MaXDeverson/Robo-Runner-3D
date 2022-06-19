using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform _endPoint;

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(Level.CurrentLevel.SoundList.Teleport);
        other.gameObject.transform.position = _endPoint.position;
    }
}
