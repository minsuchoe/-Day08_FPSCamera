using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
    public float sensitivity = 100f;
    float rotationX;
    float rotationY;

    GameObject character;


    // Use this for initialization
    void Start () {
        character = transform.parent.gameObject;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Mouse X");     //Mouse '띄어쓰고' X
        float v = Input.GetAxis("Mouse Y");

        rotationY += h * sensitivity * Time.deltaTime;
        rotationX += v * sensitivity * Time.deltaTime;

        if (rotationX > 45f)
            rotationX = 45f;

        if (rotationX < -20f)
            rotationX = -20f;

        transform.localRotation = Quaternion.AngleAxis(-rotationX, Vector3.right);
        //캐릭터도 어떤 컴포넌트의 자식이 될 수도 있으니 'local'Rotation을 사용한다.
        character.transform.localRotation = Quaternion.AngleAxis(rotationY, character.transform.up);
        //캐릭터가 좌우 방향을 바라볼 때 캐릭터도 따라 움직여야 하므로 transform 앞에 character를 넣었다.

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

    }

}
