using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected float _pushForce;
    [Header("For Die")]
    [SerializeField] protected ParticleSystem _boomParticles;
    [SerializeField] protected GameObject _destroyObj;
    [SerializeField] protected AudioSource _sourseExplouseion;
    [Header("For Ragdoll")]
    [SerializeField] private bool _isRagdoll;
    [SerializeField] private List<Transform> _bones;
    public AnimationType CurrentAnimation { get; protected set; }
    protected Rigidbody _rigidbody;
    protected const string MAIN_LAYER_NAME = "MainLayer";
    protected bool _isDie;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_isRagdoll)
        {
            foreach(Transform bone in _bones)
            {
                bone.GetComponent<BoxCollider>().enabled = false;
                bone.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
    public virtual async void PlayAnimation(AnimationType animation)
    {
        if (_isDie)
        {
            return;
        }
        CurrentAnimation = animation;
        //Specific animation;
        switch (animation)
        {
            case AnimationType.Die:
                System.Random random = new System.Random();
                if (_isRagdoll)
                {
                    RagdollDie();
                    int randomValue = random.Next(0, 6);
                    _bones.ForEach((x) => x.GetComponent<Rigidbody>().AddForce(new Vector3(5 * (randomValue > 3 ? 1 : -1), 7 + randomValue, 17 + randomValue), ForceMode.Impulse));
                }
                else
                {
                    _rigidbody.constraints = RigidbodyConstraints.None;
                    _rigidbody.AddForce(new Vector3(random.Next((int)_pushForce, (int)-_pushForce), _pushForce, -_pushForce * 3), ForceMode.Impulse);
                }
                _isDie = true;
                _animator.SetInteger(MAIN_LAYER_NAME,(int)animation);
                await Task.Delay(1100);
                if (Application.isPlaying)
                {
                    _boomParticles.transform.parent = null;
                    _sourseExplouseion.transform.parent = null;
                    Destroy(_destroyObj, 0.1f);
                    if(_sourseExplouseion != null)
                    {
                        _sourseExplouseion.PlayOneShot(Level.CurrentLevel.SoundList.EnemyExplosion);
                    }
                    _boomParticles.Play();
                }
                return;
            case AnimationType.GetDamage:
                _rigidbody.AddForce(new Vector3(0, 0, -_pushForce), ForceMode.Impulse);
                 await Task.Delay(200);
                if (!_isDie)
                {
                    _rigidbody.velocity = Vector3.zero;
                }
                return;
        }
        _animator.SetInteger(MAIN_LAYER_NAME, (int)animation);
    }
    private void RagdollDie()
    {
        _animator.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        foreach (Transform bone in _bones)
        {
            bone.GetComponent<BoxCollider>().enabled = true ;
            bone.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
