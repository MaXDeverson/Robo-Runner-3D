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
    [Header("For destroy")]
    [SerializeField] private bool _withParts;
    [SerializeField] private List<Transform> _parts;
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
                    int randomValue = random.Next(-6, 6);
                    _bones.ForEach((x) => x.GetComponent<Rigidbody>().AddForce(new Vector3(randomValue, 11 + randomValue, 19 + randomValue) * _pushForce, ForceMode.Impulse));
                }
                else
                {
                    _rigidbody.constraints = RigidbodyConstraints.None;
                    _rigidbody.AddForce(new Vector3(random.Next((int)_pushForce, (int)-_pushForce), _pushForce, -_pushForce * 3), ForceMode.Impulse);
                }
                _isDie = true;
                _animator.SetInteger(MAIN_LAYER_NAME,(int)animation);
                await Task.Delay(1100);
                if (_withParts)
                {
                    int i = 0;
                    _parts.ForEach(part =>
                    {
                        float randomX = ((float)random.Next(-25 - i ,25 + i++)) / 10;
                        int randomZ = random.Next(1, 1 + i);
                        Transform newPart = Instantiate(part, _bones[0].position + Vector3.up, new Quaternion(randomX,randomX,randomZ,randomZ));
                        newPart.parent = null;
                        newPart.GetComponent<Rigidbody>().AddForce(new Vector3(randomX * 2, randomX + 2,  1 + randomZ )/2, ForceMode.Impulse);
                       // Destroy(newPart, 4);
                    });
                }

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
