using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class SkillsManager : MonoBehaviour {
	public List<Skills> allSkills;
	public List<Skills> purchasedSkills;
	public List<Skills> unlockedSkills;

	bool canUseSmallScale;
	bool canUseBigEnemy;
	bool canUseSlowEnemy;
	bool canUseHealth;
	Vector3 startScale;

	bool canUseSlowMotion;
	bool canUseHighSpeed;
	bool isUsingBigEnemy;


	void Awake()
	{
		foreach(Skills skill in ShopManager.instance.purchasedSkills)
		{
			allSkills.Add(skill);
		}
	}

	void Start()
	{
		startScale = this.transform.localScale;
		canUseSmallScale = true;
		canUseSlowMotion = true;
		canUseHighSpeed = true;
		canUseBigEnemy = true;
		canUseHealth = true;
		isUsingBigEnemy = false;
	}

	void Update()
	{
		if(Input.GetMouseButton(1))
		{
			UseSkill(PlayerPrefs.GetInt("equipped skill index"));
		}

		if(isUsingBigEnemy)
		{
			foreach(Enemy enemy in FindObjectsOfType<Enemy>())
			{
				enemy.transform.localScale = new Vector3(2f,2f,2f);
			}
		}
	}

	#region UseSkills
	public void UseSkill(int skillIndex)
	{
			switch(skillIndex)
			{
				case 0:
			 	UseBigEnemy();
				break;
				case 1:
				UseHighSpeed();
				break;
				case 2:
				UseSlowMotion();
				break;
				case 3:
				UseHealth();
				break;
				case 4:
				UseSmallPlayer();
				break;
			}
	}

	public void UseSmallPlayer()
	{
		StartCoroutine(SmallPlayer());
	}
	public void UseSlowMotion()
	{
		StartCoroutine(SlowMotion());
	}

	public void UseHighSpeed()
	{
		StartCoroutine(HighSpeed());
	}
	public void UseBigEnemy()
	{
		StartCoroutine(BigEnemy());
	}

	public void UseHealth()
	{
		if(canUseHealth)
		{
			GetComponent<Player>().health = GetComponent<Player>().startingHealth;
			canUseHealth = false;
		}	
	}
	#endregion UseSkills
	#region UseSkills
	IEnumerator SmallPlayer()
	{
		Vector3 startingScale = startScale;
		Vector3 smallScale = new Vector3(.6f,.6f,.6f);
		float duration = 10f;
		float recuperationTime = 20f;

		if(canUseSmallScale)
		{
			this.transform.localScale = smallScale;
			yield return new WaitForSecondsRealtime(duration);
			this.transform.localScale = startingScale;
			canUseSmallScale = false;
		}else{
			if(!canUseSmallScale)
			{
				yield return new WaitForSecondsRealtime(recuperationTime);
				canUseSmallScale = true;
			}
		}
		
	}

	IEnumerator SlowMotion()
	{
		float duration = 5f;
		float recuperationTime = 20f;

		if(canUseSlowMotion)
		{
			Time.timeScale = .5f;
			this.GetComponent<Player>().mainCam.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 8;
			this.GetComponent<Player>().fpsCam.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 8;
			yield return new WaitForSecondsRealtime(duration);
			Time.timeScale = 1f;
			this.GetComponent<Player>().mainCam.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 0;
			this.GetComponent<Player>().fpsCam.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 0;
			canUseSlowMotion = false;
		}else{
			if(!canUseSlowMotion)
			{
				yield return new WaitForSecondsRealtime(recuperationTime);
				canUseSlowMotion = true;
			}
		}
		
	}

	IEnumerator HighSpeed()
	{
		float newSpeed = 8.5f;
		float duration = 8f;
		float recuperationTime = 20f;

		if(canUseHighSpeed)
		{
			this.GetComponent<Player>().moveSpeed = newSpeed;
			yield return new WaitForSecondsRealtime(duration);
			this.GetComponent<Player>().moveSpeed = 5f;
			canUseHighSpeed = false;
		}else{
			if(!canUseHighSpeed)
			{
				yield return new WaitForSecondsRealtime(recuperationTime);
				canUseHighSpeed = true;
			}
		}
	}

	IEnumerator BigEnemy()
	{
		Vector3 startingScale = new Vector3(1f,1f,1f);
		Vector3 smallScale = new Vector3(2f,2f,2f);
		float duration = 10f;
		float recuperationTime = 20f;

		if(canUseBigEnemy)
		{
			isUsingBigEnemy = true;
			foreach(Enemy enemy in FindObjectsOfType<Enemy>())
			{
				enemy.transform.localScale = smallScale;
			}
			yield return new WaitForSecondsRealtime(duration);
			foreach(Enemy enemy in FindObjectsOfType<Enemy>())
			{
				enemy.transform.localScale = startingScale;
			}
			isUsingBigEnemy = false;
			canUseBigEnemy = false;
		}else{
			if(!canUseSmallScale)
			{
				yield return new WaitForSecondsRealtime(recuperationTime);
				canUseSmallScale = true;
			}
		}
		
	}	
	#endregion UseSkills
}