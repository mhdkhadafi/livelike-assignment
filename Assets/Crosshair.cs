using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	public delegate void MouseAction(GameObject go);
	public static event MouseAction OnHover;
	public static event MouseAction OffHover;
	public static event MouseAction OnClick;
	private GameObject colHit = null;
	public bool dynamicCrosshair;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Cursor.visible = false;
		var mousePos = Input.mousePosition;
		mousePos.z = 15;
		Vector3 p = Camera.main.ScreenToWorldPoint(mousePos);
		transform.position = p;

		RaycastHit vHit = new RaycastHit();
		Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (vRay, out vHit, 1000)) {
			
			GameObject hitObject = vHit.collider.gameObject;
			if (dynamicCrosshair) transform.position = vHit.point;
			Collider coll = hitObject.GetComponent<Collider> ();
			Vector3 closestPoint = coll.ClosestPointOnBounds (Camera.main.transform.position);

			if (hitObject.name != "Crosshair") {
				

				if (dynamicCrosshair){
					while (transform.position.z > closestPoint.z) {
						transform.position = Vector3.MoveTowards (transform.position, Camera.main.transform.position, (float)0.01);
					}
				}
				colHit = hitObject;
				if (OnHover != null)
					OnHover (hitObject);
			} else {
				colHit = null;
				if (OffHover != null)
					OffHover (hitObject);
			}
		} 

		if (Input.GetMouseButtonDown(0)){ 
			if (colHit != null) {
				if (OnClick != null)
					OnClick (colHit);
			}
		}
	}
}
