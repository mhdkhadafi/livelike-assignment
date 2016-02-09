using UnityEngine;
using System.Collections;

public class DeflateBall : MonoBehaviour {

	private int state = 4;
	void OnEnable()
	{
		Crosshair.OnClick += Deflate;
		Crosshair.OnHover += GlowOn;
		Crosshair.OffHover += GlowOff;
	}


	void OnDisable()
	{
		Crosshair.OnClick -= Deflate;
		Crosshair.OnHover -= GlowOn;
		Crosshair.OffHover -= GlowOff;
	}


	void Deflate(GameObject go)
	{
		if (this.name == go.name) {
			if (state > 1) {
				transform.localScale = new Vector3 (transform.localScale.x * (2f / 3), transform.localScale.y * (2f / 3), transform.localScale.z * (2f / 3));
				state--;
			} else {
				transform.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
				state = 4;
			}
		}
	}

	void GlowOn(GameObject go) {
		if (this.name == go.name) {
			Behaviour halo = (Behaviour)GetComponent ("Halo");
			halo.enabled = true;
		}
	}

	void GlowOff(GameObject go) {
		Behaviour halo = (Behaviour)GetComponent("Halo");
		halo.enabled = false;
	}
}
