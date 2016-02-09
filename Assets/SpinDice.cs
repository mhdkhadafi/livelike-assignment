using UnityEngine;
using System.Collections;

public class SpinDice : MonoBehaviour {

	private bool spinning = false;
	void OnEnable()
	{
		Crosshair.OnClick += Spin;
		Crosshair.OnHover += GlowOn;
		Crosshair.OffHover += GlowOff;
	}


	void OnDisable()
	{
		Crosshair.OnClick -= Spin;
		Crosshair.OnHover -= GlowOn;
		Crosshair.OffHover -= GlowOff;
	}

	// I use different behavior (vs DeflateBall) here to show the robustness of using event
	void Spin(GameObject go)
	{
		if (this.name == go.name) {
			spinning = !spinning;
			Behaviour halo = (Behaviour)GetComponent("Halo");
			halo.enabled = false;
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

	void Update() {
		if (spinning) transform.rotation = Random.rotation;
	}
}
