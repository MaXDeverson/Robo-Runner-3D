using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private const string MAIN_LAYER_NAME = "MainLayer";
    private const string HAND_LAYER_NAME = "HandLayer";
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SetMainAnimation(AnimationType type,LayerType layerType)
    {
        string layerName = layerType.Equals(LayerType.MainLayer) ? MAIN_LAYER_NAME : HAND_LAYER_NAME;
        _animator.SetInteger(layerName, (int)type);
    }

    public enum LayerType
    {
        MainLayer,
        HandLayer
    }
}
