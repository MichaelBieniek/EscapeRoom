using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProps : MonoBehaviour
{
    [SerializeField] public int MagCapacity;
    [SerializeField] public float FireRate;
    [SerializeField] public ParticleSystem MuzzleFlash;
    [SerializeField] public AudioSource GunshotSfx;
    [SerializeField] public AudioSource GunClickSfx;
    [SerializeField] public AudioSource ReloadSfx;
    [SerializeField] public Animation GunshotAnim;
    [SerializeField] public Animation ReloadAnim;
    [SerializeField] public GameObject BulletOriginPoint;
    [SerializeField] public GameObject BulletPrefab;
}
