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
		trans = transform;
		rigid = trans.GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		//IF THE FOLLOWER IS WITHIN RANGE OF THE PLAYER AND THE PLAYER HAS NOT DIMISSED
		//FOLLOWERS THEN SET THE UNITS MATERIAL TO FOLLOWING AND HAVE THE UNIT FOLLOW 
		//THE PLAYER.
		if(in_zone && !mag_trans.GetComponent<Magnet_CS>().dismissed){
			//SET MATERIAL
			trans.gameObject.GetComponent<MeshRenderer> ().material = not_loose_mat;

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
		}else{
			trans.gameObject.GetComponent<MeshRenderer> ().material = loose_mat;
		}
	}

	//WHEN THERE IS A HITBOX TRIGGER CHECK IF THE HIT IS THE USER, IF SO THEN SET 
	//IN ZONE TO TRUE AND 
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Magnet"){
			in_zone = true;
			mag_trans = col.gameObject;

			//CHANGE THE UNIT MATERIAL AND ADD TO THE LIST OF FOLLOWERS.
			trans.gameObject.GetComponent<MeshRenderer> ().material = not_loose_mat;
			mag_trans.gameObject.GetComponent<Magnet_CS> ().followers.Add (trans.gameObject);
		}
	}

	//IF THE UNIT GETS TOO FAR AWAY IT WILL NO LONGER BE FOLLOWING THE PLAYER.
	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "Magnet"){
			in_zone = false;

			//SET THE MATERIAL TO A NOT FOCUSED VALUE.
			trans.gameObject.GetComponent<MeshRenderer> ().material = loose_mat;
			//STOP THE UNIT FROM MOVING ANY MORE.
			rigid.velocity = new Vector3 (0, 0, 0);
		}
	}
}
