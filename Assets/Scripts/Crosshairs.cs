using UnityEngine;
using System.Collections;

public class Crosshairs : MonoBehaviour {

	public GameObject GameUI;
	public LayerMask targetMask;
	public SpriteRenderer dot;
	public Color dotHighlightColour;
	Color originalDotColour;

	void Start() {
		originalDotColour = dot.color;
	}
	
	void Update () {
		if(GameUI.GetComponent<GameUI>().PauseMenu.activeSelf == true || GameUI.GetComponent<GameUI>().gameOverUI.activeSelf == true || GameUI.GetComponent<GameUI>().isOverPauseButton == true)
		{
			Cursor.visible = true;
		}else{
			Cursor.visible = false;
		}
		transform.Rotate (Vector3.forward * -40 * Time.deltaTime);
	}

	public void DetectTargets(Ray ray) {
		if (Physics.Raycast (ray, 100, targetMask)) {
			dot.color = dotHighlightColour;
		} else {
			dot.color = originalDotColour;
		}
	}
}
