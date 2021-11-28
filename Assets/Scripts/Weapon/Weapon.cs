using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void ShootWait(bool shoot);
    public abstract void ShootOnce();
}
