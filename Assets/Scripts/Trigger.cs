using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Triggerable triggerable;

    private void OnTriggerEnter(Collider other)
    {
        triggerable.OnTrigger(other);
    }
}
