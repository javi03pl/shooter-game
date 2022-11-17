using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHoverEffect : MonoBehaviour {

	public void PlayEffect()
	{
		AudioManager.instance.PlaySound2D("Hover");
	}

	public void PlayClickEffect()
	{
		AudioManager.instance.PlaySound2D("Click");
	}
}
