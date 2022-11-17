using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

	Vector3 velocity;
	Vector3 rotation = Vector3.zero;
	Vector3 cameraRotation = Vector3.zero;
	Rigidbody myRigidbody;
	[SerializeField]
	Camera fpsCam;

	void Start () {
		myRigidbody = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

	public void LookAt(Vector3 lookPoint) {
		Vector3 heightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt(heightCorrectedPoint);
	}


	void FixedUpdate() {
		if(PlayerPrefs.GetInt("fps") == 0)
		{
			myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
		}
		else{
			if(PlayerPrefs.GetInt("fps") == 1)
			{
				PerformMovement();
				PerformRotation();
			}
		}
	}

	#region fps
	public void Rotate(Vector3 _rotation)
	{
		rotation = _rotation;
	}

	public void RotateCamera(Vector3 _cameraRotation)
	{
		cameraRotation = _cameraRotation;
	}

	public void PerformRotation()
	{
		myRigidbody.MoveRotation(myRigidbody.rotation * Quaternion.Euler(rotation));
		if(fpsCam != null)
		{
			fpsCam.transform.Rotate(-cameraRotation);
		}
	}

	public void PerformMovement()
	{
		if(velocity != Vector3.zero)
		{
			myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
		}
	}

	#endregion fps
}
