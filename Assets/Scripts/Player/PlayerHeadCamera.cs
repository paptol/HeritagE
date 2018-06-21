using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadCamera : MonoBehaviour {

    public GameObject bodyObject;
    private PlayerCamera playerCamera;

	// Use this for initialization
	void Start ()
    {
        playerCamera = bodyObject.GetComponent<PlayerCamera>();
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.localRotation = Quaternion.Euler(-playerCamera.GetYRotation(), 0, 0);
	}
}
