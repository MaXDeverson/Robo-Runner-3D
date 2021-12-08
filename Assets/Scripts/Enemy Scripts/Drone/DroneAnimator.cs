using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimator : EnemyAnimator
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void PlayAnimation(AnimationType animation)
    {
        //base.PlayAnimation(animation);
        switch (animation)
        {
            case AnimationType.Die:
                _boomParticles.Play();
                _boomParticles.transform.parent = null;
                Destroy(_destroyObj);
                break;
        }
    }
}
