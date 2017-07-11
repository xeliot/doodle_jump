using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	private const string INPUT_AXIS = "Horizontal";

	[SerializeField]
	private float _moveSpeed = 8.0f;

	[SerializeField]
	private Vector3 _velocity;

	[SerializeField]
	private float _gravity = -20.0f;

	[SerializeField]
	private float _bounceSpeed = 15.0f;

	private int _currentHeight = 0;

	private int _maxHeight = 0;

	// Use this for initialization
	void Start () {
		_currentHeight = 0;
	}
	
	// Update is called once per frame
	void Update () {
		_currentHeight = Mathf.FloorToInt(transform.position.y);
		float input = Input.GetAxis (INPUT_AXIS);

		_velocity.x = input * _moveSpeed;
		_velocity.y += _gravity * Time.deltaTime;

		//Debug.Log (_velocity);

		transform.position += _velocity * Time.deltaTime;
		if (_currentHeight > _maxHeight) {
			_maxHeight = _currentHeight;
			Debug.Log (_maxHeight);
		}
	}

	void OnTriggerEnter(Collider other) {

		Debug.Log ("trigger1");

		if (other.gameObject.layer == LayerMask.NameToLayer ("Platform")) {
			Debug.Log ("trigger2");
			_velocity.y = _bounceSpeed;
		}
	}
}
