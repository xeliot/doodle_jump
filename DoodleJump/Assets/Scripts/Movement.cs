using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour {

	private const string INPUT_AXIS = "Horizontal";

	[SerializeField]
	private float _moveSpeed = 8.0f;

	[SerializeField]
	private Vector3 _velocity;

	[SerializeField]
	private float _gravity = -19.0f;

	[SerializeField]
	private float _bounceSpeed = 15.0f;

	[SerializeField]
	private GameObject _rightBoundary;

	[SerializeField]
	private GameObject _leftBoundary;

	private int _currentHeight = 0;
	private int _maxHeight = 0;

	[System.Serializable]
	public class MaxHeightEvent : UnityEvent<float> {}
	[SerializeField]
	private MaxHeightEvent _maxHeightEvent;

	[SerializeField]
	private GameObject _mainCamera;

	// Use this for initialization
	void Start () {
		_currentHeight = 0;
	}
	
	// Update is called once per frame
	void Update () {
		_currentHeight = Mathf.FloorToInt(transform.position.y);
		float input = Input.GetAxis (INPUT_AXIS);

		Vector3 leftWallPos = _leftBoundary.gameObject.transform.position;
		_leftBoundary.gameObject.transform.position = new Vector3 (leftWallPos.x, _currentHeight, leftWallPos.z);

		Vector3 rightWallPos = _rightBoundary.gameObject.transform.position;
		_rightBoundary.gameObject.transform.position = new Vector3 (rightWallPos.x, _currentHeight, rightWallPos.z);

		float yDifference = _gravity * Time.deltaTime;

		_velocity.x = input * _moveSpeed;
		_velocity.y += yDifference;

		//Debug.Log (_velocity);

		transform.position += _velocity * Time.deltaTime;
		if (_currentHeight > _maxHeight) {
			_maxHeight = _currentHeight;
			//Camera.main.transform.position += new Vector3 (0, yDifference+1.1f, 0);
			_maxHeightEvent.Invoke (_maxHeight);
			Debug.Log (_maxHeight);
		}
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.layer == LayerMask.NameToLayer ("Platform")) {
			if (_velocity.y < 0f) {
				_velocity.y = _bounceSpeed;
			}
		} else if (other.gameObject.layer == LayerMask.NameToLayer ("Boundary")) {
			if (other.gameObject == _rightBoundary) {
				transform.position = new Vector3 (_leftBoundary.transform.position.x + 1f, transform.position.y, transform.position.z);
			} else if (other.gameObject == _leftBoundary) {
				transform.position = new Vector3 (_rightBoundary.transform.position.x - 1f, transform.position.y, transform.position.z);
			}
		}
	}
}
