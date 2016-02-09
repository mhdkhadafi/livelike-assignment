using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	// Event Handlers for mouse hover and click
	public delegate void MouseAction(GameObject go);
	public static event MouseAction OnHover;
	public static event MouseAction OffHover;
	public static event MouseAction OnClick;

	private GameObject colHit = null;
	public bool dynamicCrosshair;
	
	// Update is called once per frame
	void Update () {
		// Make the crosshair follow the mouse location
		Cursor.visible = false;
		var mousePos = Input.mousePosition;
		mousePos.z = 15;
		Vector3 p = Camera.main.ScreenToWorldPoint(mousePos);
		transform.position = p;

		// Using raycasting to detect whether crosshair/mouse is over an object
		RaycastHit vHit = new RaycastHit();
		Ray vRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (vRay, out vHit, 1000)) {
			
			GameObject hitObject = vHit.collider.gameObject;
			if (dynamicCrosshair) transform.position = vHit.point;
			Collider coll = hitObject.GetComponent<Collider> ();
			Vector3 closestPoint = coll.ClosestPointOnBounds (Camera.main.transform.position);

			if (hitObject.name != "Crosshair") {
				// When the crosshair is dynamic, it will be in front of the object
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

		// I use this instead of OnMouseClick() because when in fixed crosshair, the crosshair will sometimes be behind object, thus wont detect onClick
		if (Input.GetMouseButtonDown(0)){ 
			if (colHit != null) {
				if (OnClick != null)
					OnClick (colHit);
			}
		}
	}
}
