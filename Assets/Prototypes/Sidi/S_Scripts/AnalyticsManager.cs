using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsManager : MonoBehaviour {
	public GameObject Inventory;

	WeaponSelection WeaponSelection;
	List<Weapon> data;
	string weaponName;
	int weaponId;
	bool found; 

	void Start () {
		WeaponSelection = Inventory.GetComponent<WeaponSelection> ();
		data = new List<Weapon> ();
		found = false;
		weaponName = "";
		weaponId = 0;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		weaponName = WeaponSelection.GetWeaponName () ;
		weaponId = WeaponSelection.GetWeaponID ();
		if( weaponId != -1){
			for (int i = 0; i < data.Count; i++) {
				if (data [i].getId () == weaponId) {
					data [i].UpdateTimer (Time.deltaTime);
					found = true;
				} else {
					found = false;
				}
			}
			if (found == false) {
				Weapon w = new Weapon (weaponId, weaponName, 0.0f);
				data.Add (w);
			}
		}

		for(int i = 0; i < data.Count; i++){
			
			Debug.Log (data [i].getName ());
		}
	}
}
