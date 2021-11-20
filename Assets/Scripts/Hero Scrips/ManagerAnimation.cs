using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ManagerAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private const string MAIN_LAYER_NAME = "MainLayer";
    private const string HAND_LAYER_NAME = "HandLayer";
    private bool _palyDieAnimation;
    private bool _isDie;
    void Start()
    {

    }
    void Update()
    {
        
    }

    public async void SetMainAnimation(AnimationType type,LayerType layerType)
    {
        if (type.Equals(AnimationType.Die))
        {
            _palyDieAnimation = true;
        }
        switch (type)
        {
            case AnimationType.GetDamage:
                for(int i = 0; i < 10; i++)
                {
                    if (i < 5)
                    {
                        transform.Rotate(new Vector3(0, 5, 0));
                    }
                    else
                    {
                        transform.Rotate(new Vector3(0, -5, 0));
                    }
                    await Task.Delay(10);
                }
                transform.eulerAngles = Vector3.zero;
                return;
        }
        string layerName = layerType.Equals(LayerType.MainLayer) ? MAIN_LAYER_NAME : HAND_LAYER_NAME;
        _animator.SetInteger(layerName, (int)type);
    }

    public enum LayerType
    {
        MainLayer,
        HandLayer
    }
}
