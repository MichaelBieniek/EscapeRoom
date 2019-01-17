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
    Animator _gunAnim;
    AudioSource gunshotSfx;
    AudioSource gunClickSfx;
    AudioSource reloadSfx;
    GameObject[] flashes;
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
        WeaponProps wp = weapon.GetComponent<WeaponProps>();
        _fireRate = wp.FireRate;
        _magCapacity = wp.MagCapacity;
        gunshotSfx = wp.GunshotSfx;
        gunClickSfx = wp.GunClickSfx;
        reloadSfx = wp.ReloadSfx;
        _gunAnim = wp.GunAnim;
        _bulletOriginPoint = wp.BulletOriginPoint;
        _bulletPrefab = wp.BulletPrefab;
        _casingPrefab = wp.CasingPrefab;
        _casingExitLocation = wp.CaseExitLocation;
        flashes = wp.MuzzleFlash;
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
        } else if (Input.GetKeyDown("r")) {
            Reload();
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
            gunClickSfx.Play();   
            // do nothing
            return;
        }
        ammo--;
        _gunAnim.SetInteger("ammo", ammo);
        
        // play animation & sfx
        CasingRelease();
                
        MuzzleFlash();       
        
        if(gunshotSfx != null) {
            gunshotSfx.Play();
        }
       
        _gunAnim.SetTrigger("shoot");  
        if(ammo == 0) {
            _gunAnim.SetBool("empty", true);
        }       

        // Create the Bullet from the Bullet Prefab
        //GameObject bullet = Instantiate(_bulletPrefab, _bulletOriginPoint.transform.position, _bulletOriginPoint.transform.rotation);

        // Add velocity to the bullet
        //bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 5f;

        //Ray ray = bulletOriginCamera.ScreenPointToRay(Input.mousePosition);
    }

    void MuzzleFlash() {
        if(flashes == null) {
            Debug.Log("No flash prefabs");
            return;
        }
        int randomNumberForMuzzelFlash = Random.Range(0,5);
        GameObject tempFlash;
        Instantiate(_bulletPrefab, _bulletOriginPoint.transform.position, _bulletOriginPoint.transform.rotation).GetComponent<Rigidbody>().AddForce(_bulletOriginPoint.forward * 1000f);
        tempFlash = Instantiate(flashes[randomNumberForMuzzelFlash], _bulletOriginPoint.transform.position, _bulletOriginPoint.transform.rotation);
    }
    void CasingRelease()
    {
        GameObject casing;
        casing = Instantiate(_casingPrefab, _casingExitLocation.position, _casingExitLocation.rotation) as GameObject;
        casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (_casingExitLocation.position - _casingExitLocation.right * 0.3f - _casingExitLocation.up * 0.6f), 1f);
        casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
    }

    void Reload() {
        StartCoroutine("ReloadDelay");
        
         
    }

    IEnumerator ReloadDelay(){
        _gunAnim.SetTrigger("reload"); 
        reloadSfx.Play();
		yield return new WaitForSeconds (1f);
		ammo = _magCapacity;
        _gunAnim.SetInteger("ammo", ammo);
        _gunAnim.SetBool("empty", false);
	}
}
