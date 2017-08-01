using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour {

    public static  Transform[] wayPoints;
	// Use this for initialization
	void Start () {
        wayPoints = new Transform[transform.childCount];
        for(int i=0;i<wayPoints.Length;i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
