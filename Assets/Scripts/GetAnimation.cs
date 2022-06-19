using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnimation : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.Player) && !other.isTrigger)
        {
            PlayGetAnimation();
        }
    }

    private void PlayGetAnimation()
    {
        Level.CurrentLevel.AudioSourses.SourceCrystal.PlayOneShot(Level.CurrentLevel.SoundList.CrystalCrush);
        _particleSystem.transform.parent = null;
        _particleSystem.Play();
        ///Here Destroy aniamtion;
        Destroy(gameObject);
        Destroy(_particleSystem.gameObject, 1);
    }
}
