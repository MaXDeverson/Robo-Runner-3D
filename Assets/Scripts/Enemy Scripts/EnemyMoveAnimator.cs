using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveAnimator : EnemyAnimator
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public override void PlayAnimation(AnimationType animation)
    {
        _animator.SetInteger(MAIN_LAYER_NAME,(int)animation);
        base.PlayAnimation(animation);
    }
}
