using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tile Color" , menuName = "Tile Material")]
public class TileMaterials : ScriptableObject {

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
