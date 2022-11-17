using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new color" , menuName = "Color")]
public class Colors : ScriptableObject {

	public string colorName;
	public Color color;
	public Material material;
	public int cost;
	public int levelToUnlock;
	public Sprite colorImage;
	public bool isPurchased;
	public bool isEquipped;
	public bool isUnlocked;

}
