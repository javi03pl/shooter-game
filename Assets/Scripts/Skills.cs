using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill" , menuName = "Skill")]
public class Skills : ScriptableObject {

	public string skillName;
	public int cost;
	public int levelToUnlock;
	[TextArea(1,3)]
	public string description;
	public Sprite skillImage;
	public bool isPurchased;
	public bool isEquipped;
	public bool isUnlocked;
}