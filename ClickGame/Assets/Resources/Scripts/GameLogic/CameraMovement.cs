using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	
	Vector3 initialMousePosition;
	bool movingMouse = false;
	bool movingMouseMobile = false;
	public float cameraMoveSpeed;
	public float cameraMoveSpeedMobile;
	public float cameraAccelerationSpeedConstant;
	public float timeUntilCameraCanMove;
	Vector3 initialPosition;
	
	public float speedResetTime;
	float cameraTimer;
	float speed;
	Vector2 delta;
	
	Rigidbody2D cameraRigid;
	Transform cameraTrans;
	
	
	// Use this for initialization
	void Start () {
		cameraRigid = Camera.main.gameObject.GetComponent<Rigidbody2D>();
		cameraTrans = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		cameraTimer += Time.deltaTime;
	
		if(Input.GetAxis("Mouse ScrollWheel") != 0.0)
		{
			if(Input.GetAxis("Mouse ScrollWheel") > 0.0)
			{
				
				if(Camera.main.orthographicSize >= 2.0F)
				{
					Camera.main.orthographicSize -= 0.5F;
				}
				
			}
			else
			{
				if(Camera.main.orthographicSize <= 10.0F)
				{
					Camera.main.orthographicSize += 0.5F;
				}
			}
		}
		
		
		if(Input.GetMouseButtonDown(1))
		{
			movingMouse = true;
			initialMousePosition = Input.mousePosition;
		}
		else if(Input.GetMouseButtonUp(1))
		{
			movingMouse = false;
			
		}
		
		
		
		
		
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) 
		{
			initialPosition = Input.GetTouch(0).position;
			speed = 0;
			cameraRigid.velocity = Vector2.zero;
		}
		else if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved)
		{
		
			
		
			if(Vector3.Distance(initialPosition,Input.GetTouch(0).position) > 100)
			{
				initialPosition = Input.GetTouch(0).position;
				
			}
			else
			{
				
			}
		
			Vector3 initialCalc = Camera.main.ScreenToWorldPoint(initialPosition);
			Vector3 newCalc = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			
			
			
			
			 
			delta = -Input.GetTouch (0).deltaPosition;
			
			float distance = Vector3.Distance(initialCalc,newCalc);
			
			float tempSpeed = (distance / Time.deltaTime);
			
			if(tempSpeed > speed)
			{
				speed = tempSpeed;
			}
			else if(cameraTimer >= speedResetTime)
			{
				speed = 0;
				cameraTimer = 0;
			}
			
			
			
			
		
			cameraTrans.Translate((initialCalc.x - newCalc.x) * cameraMoveSpeedMobile, (initialCalc.y - newCalc.y) * cameraMoveSpeedMobile, 0);
			
			initialPosition = Input.GetTouch(0).position;
		}
		else if(Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Ended)
		{
		
		
			
			
			//Vector3 initialCalc = Camera.main.ScreenToWorldPoint(initialPosition);
			//Vector3 newCalc = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			//Debug.Log (speed);
			//Debug.Log (Input.GetTouch(0).deltaPosition.x);
			//Vector2 force = new Vector2((initialCalc.x - newCalc.x) * speed,(initialCalc.y - newCalc.y) * speed);
			delta.Normalize();
			
			speed *= cameraAccelerationSpeedConstant;
			
			
			Vector2 force = new Vector2(delta.x * speed,delta.y * speed);
			//Debug.Log ("Delta = " + delta);
			//Debug.Log ("Speed = " + speed);
			//Debug.Log (force);
			
			cameraRigid.AddForce(force);
			
		}
		
		
//		else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
//		{
//			movingMouseMobile = false;
//		}
		
		
		
		
		
		
		if(movingMouse)
		{
			
			Camera.main.transform.position -= (Input.mousePosition - initialMousePosition) / cameraMoveSpeed;
			initialMousePosition = Input.mousePosition;
		}
//		else if(movingMouseMobile)
//		{
//			cameraTimer += Time.deltaTime;
//			
//			if(cameraTimer >= timeUntilCameraCanMove)
//			{
//				Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
//				Camera.main.transform.Translate(-touchDeltaPosition.x * cameraMoveSpeedMobile, -touchDeltaPosition.y * cameraMoveSpeedMobile, 0);
//			}
//		}
		
	}
}





