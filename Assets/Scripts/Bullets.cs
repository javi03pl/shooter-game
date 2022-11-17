using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new bullet material" , menuName = "Bullet Material")]
public class Bullets : ScriptableObject {

	public string bulletName;
	public Color color;
	public Material material;
	public int cost;
	public int levelToUnlock;
	public Sprite bulletImage;
	public bool isPurchased;
	public bool isEquipped;
	public bool isUnlocked;
}
