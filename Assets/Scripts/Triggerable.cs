using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Triggerable : MonoBehaviour
{
    public abstract void OnTrigger(Collider inputCollider,int triggerIndex);
    public virtual void TriggerExit(Collider exitCollider, int triggerIndex) { }
}
