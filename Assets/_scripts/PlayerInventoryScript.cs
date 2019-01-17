using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
	public GameObject currentGun;
	private Animator currentHandsAnimator;
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine("SpawnWeaponUponStart");
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	/*
	* Assigns Animator to the script so we can use it in other scripts of a current gun.
	*/
	void AssignHandsAnimator(GameObject _currentGun){
		//currentHandsAnimator = currentGun.GetComponent<WeaponProps>().handsAnimator;
	}

	/*
	*Waits some time then calls for a weapon spawn
	*/
	IEnumerator SpawnWeaponUponStart(){
		yield return new WaitForSeconds (0.5f);
		StartCoroutine("SpawnWeapon", 0);
	}
	void SpawnWeapon() {
		//GameObject resource = (GameObject) Resources.Load(gunsIHave[_redniBroj].ToString());
		//		currentGun = (GameObject) Instantiate(resource, transform.position, /*gameObject.transform.rotation*/Quaternion.identity);
		//		AssignHandsAnimator(currentGun);
	}
}
