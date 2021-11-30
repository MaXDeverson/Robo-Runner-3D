using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEventListener : MonoBehaviour
{
    public Action ShootAction;
    public void ShootEvent(float inputF)
    {
        ShootAction?.Invoke();
    }
}
