using UnityEngine;
using System.Collections;

public class PerlinShake : MonoBehaviour {
	
	public float duration = 1f;
	public float speed = 5.0f;
	public float magnitude = 0.1f;
	
	public bool test = false;
	
	// -------------------------------------------------------------------------
	public void PlayShake() {
		
		StopAllCoroutines();
		StartCoroutine("Shake");
	}
	
	// -------------------------------------------------------------------------
	void Update() {
		if (test) {
			test = false;
			PlayShake();
		}
	}
	
	// -------------------------------------------------------------------------
	IEnumerator Shake() {

		//Time.timeScale = 0.7f;

		float elapsed = 0.0f;
		
//		Vector3 originalCamPos = new Vector3(0, 4f, -7.5f);
		float randomStart = Random.Range(-1000.0f, 1000.0f);
		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;			
			
			float percentComplete = elapsed / duration;			
			
			// We want to reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);
			
			// Calculate the noise parameter starting randomly and going as fast as speed allows
			float alpha = randomStart + speed * percentComplete;
			
			// map noise to [-1, 1]
			float x = Util.Noise.GetNoise(alpha, 0.0f, 0.0f) * 2.0f - 1.0f;
			float y = Util.Noise.GetNoise(0.0f, alpha, 0.0f) * 2.0f - 1.0f;
			
			x *= magnitude * damper;
			y *= magnitude * damper;

			//Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);
			//Camera.main.transform.position = new Vector3(x + originalCamPos.x, y + originalCamPos.y, originalCamPos.z);
			Camera.main.transform.localPosition = new Vector3(x , y , 0f);

			yield return null;
		}
		//Camera.main.transform.localPosition = new Vector3(0, 0, 0f);
		//Time.timeScale = 1f;
	}
}
