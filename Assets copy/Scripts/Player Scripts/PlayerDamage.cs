using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour {

	private Text lifeText;
	private int lifeScoreCount;

	private bool canDamage;

	
	void Start() {
		Time.timeScale = 1f;
	}

    public void DealDamage()
    {
        if (canDamage)
        {
            GameManager.instance.PlayerFellInWater(transform.position);  // Call your GameManager
            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage() {
		yield return new WaitForSeconds (2f);
		canDamage = true;
	}

	IEnumerator RestartGame() {
		yield return new WaitForSecondsRealtime(2f);
		SceneManager.LoadScene ("Gameplay");
	}

} // class
