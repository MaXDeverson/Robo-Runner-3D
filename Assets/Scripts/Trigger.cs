using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private Triggerable[] triggerables;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < triggerables.Length; i++)
            if (triggerables[i] != null) triggerables[i].OnTrigger(other);
        
    }
}
