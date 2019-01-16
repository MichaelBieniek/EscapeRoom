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
    Animator _gunAnim;
    AudioSource _gunshotSfx;
    AudioSource _gunClickSfx;
    AudioSource _reloadSfx;
    GameObject _muzzleFlash;
    Transform _bulletOriginPoint;
    GameObject _bulletPrefab;
    GameObject _casingPrefab;
    Transform _casingExitLocation;
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
        _gunAnim = _wp.GunAnim;
        _muzzleFlash = _wp.MuzzleFlash;
        _bulletOriginPoint = _wp.BulletOriginPoint;
        _bulletPrefab = _wp.BulletPrefab;
        _casingPrefab = _wp.CasingPrefab;
        _casingExitLocation = _wp.CaseExitLocation;

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
        if (ammo <= 0)
        {           
            // do nothing
            return;
        }
        ammo--;
        
        // play animation & sfx
        CasingRelease();
        
        if(_muzzleFlash != null) {
            MuzzleFlash();
        } else {
            Debug.Log("No muzzleflash PS or bad type");
        }
        
        if(_gunshotSfx != null) {
            _gunshotSfx.Play();
        }
       
        _gunAnim.SetTrigger("shoot");  
        if(ammo <= 1) {
            _gunAnim.SetBool("empty", true);
        }       

        // Create the Bullet from the Bullet Prefab
        //GameObject bullet = Instantiate(_bulletPrefab, _bulletOriginPoint.transform.position, _bulletOriginPoint.transform.rotation);

        // Add velocity to the bullet
        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 5f;

        //Ray ray = bulletOriginCamera.ScreenPointToRay(Input.mousePosition);
    }

    void MuzzleFlash() {
        GameObject tempFlash;
        Instantiate(_bulletPrefab, _bulletOriginPoint.transform.position, _bulletOriginPoint.transform.rotation).GetComponent<Rigidbody>().AddForce(_bulletOriginPoint.forward * 1000f);
        tempFlash = Instantiate(_muzzleFlash, _bulletOriginPoint.transform.position, _bulletOriginPoint.transform.rotation);
    }
    void CasingRelease()
    {
        GameObject casing;
        casing = Instantiate(_casingPrefab, _casingExitLocation.position, _casingExitLocation.rotation) as GameObject;
        casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (_casingExitLocation.position - _casingExitLocation.right * 0.3f - _casingExitLocation.up * 0.6f), 1f);
        casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
    }
}
