using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";
    public GameObject casing;
    public GameObject bulletPrefab;

    [SerializeField]
    GameObject bulletOriginPoint;
    public GameObject player;

    private float nextActionTime = 0.0f;
    public float period = 0.1f;

    [SerializeField] GameObject weapon;

    bool _autoFire = false;
    bool _isFiring = false;
    float _actionCoolDown = 0.5f;
    
    float cooldown = 0f;
    float _actionTimer = 0f;
    // Start is called before the first frame update

    WeaponProps _wp;
    Animation _gunshotAnim;
    Animation _reloadAnim;
    AudioSource _gunshotSfx;
    AudioSource _gunClickSfx;
    AudioSource _reloadSfx;
    ParticleSystem _muzzleFlash;
    GameObject _bulletOriginPoint;
    GameObject _bulletPrefab;
    int _magCapacity;
    float _fireRate;
    float _fireRateNorm;    
    int ammo;
    void Start()
    {
        _wp = weapon.GetComponent<WeaponProps>();
        _fireRate = _wp.FireRate;
        _magCapacity = _wp.MagCapacity;
        _gunshotSfx = _wp.GunshotSfx;
        _gunClickSfx = _wp.GunClickSfx;
        _reloadSfx = _wp.ReloadSfx;
        _gunshotAnim = _wp.GunshotAnim;
        _reloadAnim = _wp.ReloadAnim;
        _muzzleFlash = _wp.MuzzleFlash;
        _bulletOriginPoint = _wp.BulletOriginPoint;
        _bulletPrefab = _wp.BulletPrefab;

        // calculated
        _fireRateNorm = 1/(_fireRate/60f);
        ammo = _magCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown = cooldown <= 0 ? 0 : cooldown - Time.deltaTime;
        _actionTimer = _actionTimer <= 0 ? 0 : _actionTimer - Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            _isFiring = true;                
            Fire();
            _isFiring = false;
        }
    }

    void Fire()
    {
        Debug.Log("Fired gun");
        if (cooldown > 0)
        {
            // not ready
            return;
        }
        cooldown = _fireRateNorm;
        
        // play animation & sfx
        DropCasing();
        
        if(_muzzleFlash != null) {
            _muzzleFlash.Play();
        } else {
            Debug.Log("No muzzleflash PS or bad type");
        }
        
        if(_gunshotSfx != null) {
            _gunshotSfx.Play();
        }
        Debug.Log(_gunshotAnim);
        if(_gunshotAnim != null) {
            _gunshotAnim.Play("FireAnim");
        }

        if (ammo <= 0)
        {
            // do nothing
            return;
        }
        ammo--;

        // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(_bulletPrefab, _bulletOriginPoint.transform.position, _bulletOriginPoint.transform.rotation);

        // Add velocity to the bullet
        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 0.1f;

        //Ray ray = bulletOriginCamera.ScreenPointToRay(Input.mousePosition);
    }

    void DropCasing() {
        // tbi
    }
}
