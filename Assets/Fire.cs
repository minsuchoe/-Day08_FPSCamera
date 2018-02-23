using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {
    float emitSpeed = 100f;    
    public GameObject shell;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

	    if (Input.GetButtonDown("Fire1"))
        {
            var a = Instantiate(shell, transform.position, Quaternion.identity);
            a.GetComponent<Rigidbody>().AddForce(new Vector3(emitSpeed, emitSpeed, 0));
            a.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * emitSpeed);
            //탄피 회전
            Destroy(a, 2f);
        }

	}
}
