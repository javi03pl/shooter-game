using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {

	public LayerMask collisionMask;
	public Color trailColour;
	float speed = 10;
	float damage = 1;

	float lifetime = 3;
	float skinWidth = .1f;
	Material bulletMaterial;
	public List<Bullets> allBullets;
	public List<Bullets> purchasedBullets;
	public List<Bullets> unlockedBullets;

	void Start() {
		Destroy (gameObject, lifetime);

		bulletMaterial = GetComponent<Renderer>().material;

		Collider[] initialCollisions = Physics.OverlapSphere (transform.position, .1f, collisionMask);
		if (initialCollisions.Length > 0) {
			OnHitObject(initialCollisions[0], transform.position);
		}

		GetComponent<TrailRenderer>().material.SetColor ("_TintColor", trailColour);

		foreach(Bullets bullet in ShopManager.instance.bulletsInShop)
		{
			allBullets.Add(bullet);
		}

		bulletMaterial.color = allBullets[PlayerPrefs.GetInt("equipped bullet index")].color;
		bulletMaterial.mainTexture = allBullets[PlayerPrefs.GetInt("equipped bullet index")].material.mainTexture;
	}

	public void SetSpeed(float newSpeed) {
		speed = newSpeed;
	}
	
	void Update () {
		float moveDistance = speed * Time.deltaTime;
		CheckCollisions (moveDistance);
		transform.Translate (Vector3.forward * moveDistance);
	}


	void CheckCollisions(float moveDistance) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide)) {
			OnHitObject(hit.collider, hit.point);
		}
	}

	void OnHitObject(Collider c, Vector3 hitPoint) {
		IDamageable damageableObject = c.GetComponent<IDamageable> ();
		if (damageableObject != null) {
			damageableObject.TakeHit(damage, hitPoint, transform.forward);
			PlayerPrefs.SetInt("total hits to enemy" , PlayerPrefs.GetInt("total hits to enemy")+1);
			PlayerPrefs.SetInt("accuracy" , (PlayerPrefs.GetInt("totalkills")*100)/PlayerPrefs.GetInt("total shots"));
		}
		GameObject.Destroy (gameObject);
	}
}
