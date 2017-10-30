using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControoler_CS : MonoBehaviour {

	//PUBLIC VARIABLES
	public GameObject player;
	public GameObject follower;

	//HOW FAST THE PLAYER CAN SPEED UP.
	public float player_accel = 15f;
	public float rot_speed = 5;

	//MAX SPEED THE PLAYER IS ALLOWED TO TRAVEL AT.
	public float for_speed = 10f;
	public float back_speed = 10f;

	public float max_angle = 30f;

	// Update is called once per frame
	void Update () {
		Magnet_CS player_script = player.GetComponent<Magnet_CS> ();
		Transform player_trans = player.GetComponent<Transform> ();
		Transform formation = player_script.GetComponent<Magnet_CS>().formation.transform;

		//GRAB THE PLAYER ROTATION FIRST BEFORE MAKING THE CALCULATIONS.
		Quaternion rot = player.GetComponent<Transform> ().rotation;
		float y = rot.eulerAngles.y;
		float z = rot.eulerAngles.z;

		//SET IS MOVING TO TRUE BASED ON ANY KEY DOWN EVENTS.
		//SET MOVING TO TRUE IF THE PLAYER IS GOING FORWARDS OR BACKWARDS.
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)){
			player_script.is_moving = true;
		}else{
			player_script.is_moving = false;
		}

		//CHECK IF EITHER OF THE TURNING BUTTONS ARE BEING PRESSED AND SET
		//THE LEANING BOOLEAN TO TRUE IF SO.
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
			player_script.is_leaning = true;
		}else{
			player_script.is_leaning = false;
		}


		//BACKWARDS AND FORWARDS LOGIC GOES HERE.
		if(player_script.is_moving && player.GetComponent<Rigidbody> ().velocity.x > (-1f*for_speed) && player.GetComponent<Rigidbody> ().velocity.x < for_speed && player.GetComponent<Rigidbody> ().velocity.z > (-1*back_speed) && player.GetComponent<Rigidbody> ().velocity.z < for_speed){
			if(Input.GetKey(KeyCode.W)){
				player_trans.position += player_trans.forward * player_accel * Time.deltaTime;
			}

			if(Input.GetKey(KeyCode.S)){
				player_trans.position += player_trans.forward * -player_accel * Time.deltaTime;
			}
		}

		//APPLY TURNING AND LEANING LOGIC TO THE PLAYER OBJECT IF EITHER KEY IS
		//BEING PRESSED.
		if(player_script.is_leaning){
			//ROTATION LOGIC IS APPLIED HERE.
			if(Input.GetKey(KeyCode.D)){
				y += rot_speed * Time.deltaTime;
				z = -30f;
			}

			if(Input.GetKey(KeyCode.A)){
				y += -rot_speed * Time.deltaTime;
				z = 30f;
			}					
		}else{
			z = 0f;
		}
			
		//SET THE NEW VALUES TO THE PLAYERS POSITION AND ROTATION.
		rot = Quaternion.Euler (0,y,z);
		player_trans.rotation = rot;
		Debug.Log (formation.gameObject.GetComponent<BoxCollider> ().bounds.size);
		formation.position = player_trans.position;

		//IF THEY RIGHT MOUSE BUTTON IS CLICKED THEN WE WANT TO SPAWN A FOLLOWER PREFAB WHERE IT HAS BEEN CLICKED.
		if(Input.GetMouseButtonDown(1)){
			Debug.Log ("Spawning a follower!");

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);


			if(Physics.Raycast(ray,out hit,1000.0f)){
				if(hit.collider.gameObject.tag == "PhysicalFloor"){
					var posi = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					Instantiate (follower,new Vector3(posi.x,0,posi.z),Quaternion.identity);
				}
			}
		}

		//LOGI FOR SETTING THE DISMISSAL STATUS, IF UNITS ARE DISMISSED NOTHING WILL FOLLOW THE PLAYER.
		if(Input.GetKeyDown(KeyCode.Q)){
			//MAKE THE DISMISSED VALUE OPPOSITE TO WHAT IT WAS BEFORE THE BUTTON WAS PRESSED.
			player.GetComponent<Magnet_CS> ().dismissed = !player.GetComponent<Magnet_CS> ().dismissed;

			//CHANGE PLAYER MATERIAL DEPENDING ON THE DISMISSED VALUE.
			if(player.GetComponent<Magnet_CS> ().dismissed){
				player.GetComponent<MeshRenderer> ().material = player.GetComponent<Magnet_CS> ().not_dismissed_mat;
			}else{
				player.GetComponent<MeshRenderer> ().material = player.GetComponent<Magnet_CS> ().dismissed_mat;
			}
		}

		//LOGIC FOR SHOWING AND CHANGING THE CURRENT FORMATION.
		if(Input.GetKey(KeyCode.E)){
			player.GetComponent<Magnet_CS> ().formation.SetActive (true);
		}else{
			player.GetComponent<Magnet_CS> ().formation.SetActive (false);
		}
	}
}
