using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    [Header("Level souds")]
    [SerializeField] public AudioClip CrystalCrush;
    [SerializeField] public AudioClip MineExplosion;
    [SerializeField] public AudioClip BulletHit;
    [SerializeField] public AudioClip Notification;
    [SerializeField] public AudioClip Button;
    [SerializeField] public AudioClip Teleport;
    [Header("Hero sounds")]
    [SerializeField] public AudioClip HeroShoot;
    [SerializeField] public AudioClip HeroJump;
    [SerializeField] public AudioClip HeroFall;
    [SerializeField] public AudioClip HeroRun;
    [SerializeField] public AudioClip AppearanceShield;
    [SerializeField] public AudioClip DisappearanceShield;
    [Header("Enemy")]
    [SerializeField] public AudioClip EnemyExplosion;
    [SerializeField] public AudioClip EnemyShoot;
}
