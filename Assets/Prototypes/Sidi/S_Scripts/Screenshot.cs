using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour {

	public int resWidth = 2550; 
	public int resHeight = 3300;

	private PlayerHealth playerHealth;
	private GameObject player;
	private bool takeShot = false;
	private bool shotTook = false;
	private Camera camera;
	private float timer;
	void Awake ()
	{
		camera = GetComponent<Camera> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
	}

	public static string ScreenShotName(int width, int height) {
		return string.Format("{0}/Prototypes/Sidi/Analytics/Screenshots/screen_{1}x{2}_{3}.png", 
			Application.dataPath, 
			width, height, 
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}


	void LateUpdate() {
		if (playerHealth.getCurrentHealth () <= 0) {
			timer += Time.deltaTime;
		}
		if (timer >= 20*Time.deltaTime && shotTook == false) {
			takeShot = true;
			shotTook = true;
		}

		if (takeShot) {
			camera.enabled = true;
			RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
			camera.targetTexture = rt;
			Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
			camera.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
			camera.targetTexture = null;
			RenderTexture.active = null; 
			Destroy(rt);
			byte[] bytes = screenShot.EncodeToPNG();
			string filename = ScreenShotName(resWidth, resHeight);
			System.IO.File.WriteAllBytes(filename, bytes);
			takeShot = false;
			camera.enabled = false;
		}
	}
}
