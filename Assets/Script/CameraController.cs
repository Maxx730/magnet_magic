using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject focused_character;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - focused_character.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = focused_character.transform.position + offset;
	}
}
