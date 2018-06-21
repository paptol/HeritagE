using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationArrow : MonoBehaviour {

    public Transform Arrow;
    private Transform playerTransform;
    static public Vector3 Target;

    // Use this for initialization
    void Start () {
        playerTransform = FindObjectOfType<PlayerHeadCamera>().transform;
        //Target = new Vector3(-9999, 0, 0);
        Target = GameObject.Find("LooseLever").transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 dir = playerTransform.InverseTransformPoint(Target);
        float a = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        a += 180;
        Arrow.transform.localEulerAngles = new Vector3(0, 180, a);

        if (Target == new Vector3(-9999, 0, 0))
        {
            gameObject.GetComponent<Image>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = true;
        }
    }
}
