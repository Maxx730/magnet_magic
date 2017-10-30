using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBar_CS : MonoBehaviour {

	//PUBLIC VARIABLES
	public bool in_zone = false;
	public float force = 5f;

	//MATERIALS FOR DESIGNATING VALUES
	public Material loose_mat;
	public Material not_loose_mat;

	//PRIVATE VARIABLES
	private Transform trans;
	private GameObject mag_trans;
	private Rigidbody rigid;

	void Awake(){
		mag_trans = GameObject.Find ("Player");
		trans = transform;
		rigid = trans.GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		Transform formation = mag_trans.GetComponent<Magnet_CS> ().formation.transform;
		Transform player_pos = mag_trans.GetComponent<Transform>();
		Magnet_CS player_script = mag_trans.GetComponent<Magnet_CS> ();
		Collider col = transform.GetChild(0).gameObject.GetComponent<SphereCollider> ();

		//IF THE DISMISSED BOOLEAN IS SET TO FALSE, THEN CHECK WHAT IS INSIDE THE COLLIDER
		//AND FOLLOW THE PLAYER IF THE PLAYER IS NEAR.
		if(!player_script.dismissed){
			if(col.bounds.Contains(player_pos.position)){
				//SET MATERIAL
				trans.gameObject.GetComponent<MeshRenderer> ().material = not_loose_mat;

				//LOGIC FOR FOLLOWING THE PLAYER WHEN THE UNITS HAVE NOT BEEN
				//DISMISSED ETC.
				Quaternion _look;
				Vector3 _dir;
				float _dist;

				_dir = (mag_trans.GetComponent<Transform> ().position - trans.position).normalized;
				_look = Quaternion.LookRotation (_dir);
				_dist = Vector3.Distance (trans.position,mag_trans.GetComponent<Transform>().position);

				trans.rotation = Quaternion.Slerp(trans.rotation, _look, Time.deltaTime * 10f);

				if(_dist > 6f){
					trans.position += transform.forward * 10F * Time.deltaTime;
				}
			}
		}else{
			//SET MATERIAL
			trans.gameObject.GetComponent<MeshRenderer> ().material = loose_mat;			
		}
	}
}
