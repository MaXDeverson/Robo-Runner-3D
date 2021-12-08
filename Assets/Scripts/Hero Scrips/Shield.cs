using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private HeroDestroyer _destroyer;
    [SerializeField] private GameObject _shieldObj;
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
        _shieldObj.SetActive(enable);
    }
}
