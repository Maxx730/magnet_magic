using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBar_CS : MonoBehaviour {

	//PUBLIC VARIABLES
	public bool in_zone = false;
	public bool loose = true;
	public float force = 5f;

	//MATERIALS FOR DESIGNATING VALUES
	public Material loose_mat;
	public Material not_loose_mat;

	//PRIVATE VARIABLES
	private Transform trans;
	private GameObject mag_trans;
	private Rigidbody rigid;
	private float stop_space;

	void Awake(){
		trans = transform;
		rigid = trans.GetComponent<Rigidbody> ();
		stop_space = Random.Range (0.5f,5.0f);
	}

	void FixedUpdate(){
		if(in_zone){
			Vector3 direction = (mag_trans.GetComponent<Transform> ().position - trans.position) * force;
			float distance = Vector3.Distance (mag_trans.GetComponent<Transform> ().position,trans.position);

			if(distance > mag_trans.GetComponent<Magnet_CS>().stop_distance + stop_space){
				rigid.AddForce (direction);	
			}else{
				
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Magnet"){
			in_zone = true;
			mag_trans = col.gameObject;
			loose = false;
			trans.gameObject.GetComponent<MeshRenderer> ().material = not_loose_mat;
		}
	}

	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "Magnet"){
			in_zone = false;
			loose = true;
			trans.gameObject.GetComponent<MeshRenderer> ().material = loose_mat;
			rigid.velocity = new Vector3 (0, 0, 0);
		}
	}
}
