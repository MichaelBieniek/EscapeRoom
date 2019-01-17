using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProps : MonoBehaviour
{
    [SerializeField] public int MagCapacity;
    [SerializeField] public float FireRate;
    [SerializeField] public AudioSource GunshotSfx;
    [SerializeField] public AudioSource GunClickSfx;
    [SerializeField] public AudioSource ReloadSfx;
    [SerializeField] public Animator GunAnim;
    [SerializeField] public Transform BulletOriginPoint;
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField] public GameObject CasingPrefab;
    [SerializeField] public Transform CaseExitLocation;
    [SerializeField] public GameObject[] MuzzleFlash;
}
