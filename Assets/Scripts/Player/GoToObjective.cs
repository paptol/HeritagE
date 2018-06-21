using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToObjective : MonoBehaviour {
    Image image;

    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 temp = NavigationArrow.Target;
        temp.y += 15;
        gameObject.transform.position = temp;

        Color c = image.color;
        c.a = Mathf.Sin(10 * Time.time);
        image.color = c;
    }
}
