using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour {

    public float speed = 4;
    public float mouseSpeed = 20;
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouse = Input.GetAxisRaw("Mouse ScrollWheel");
        Debug.Log(mouse);
        transform.Translate(new Vector3(h*speed, mouse*mouseSpeed, v*speed)*Time.deltaTime,Space.World);

       
	}
}
