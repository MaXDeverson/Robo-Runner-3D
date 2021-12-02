using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] HeroDestroyer _destroyer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActive(bool enable)
    {
        _destroyer.IgnoreDamage(enable);
    }
}
