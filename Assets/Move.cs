using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public Vector3 slidingDirection;
    public bool sliding;

    public float moveSpeed = 5f;
    public float slideSpeed = 8f;

    public GameObject healEffect;
    private GameObject e;
    private bool healing = false;
    
    CharacterController con;

    Vector3 moveDirection = Vector3.zero;

    float jumpSpeed = 8f;
    float gravity = 20f;
    
    private Vector3 hitNormal;

	// Use this for initialization
	void Start () {
        con = GetComponent<CharacterController>();                
	}
	
	// Update is called once per frame
	void Update () {
		if (con.isGrounded)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            moveDirection = (new Vector3(h, 0, v)).normalized;

            //transform.LookAt(transform.position + moveDirection);
            moveDirection = transform.TransformDirection(moveDirection);
            //캐릭터가 바라보는 방향으로 이동하게끔 함. (좌표를 로컬에서 월드로 바꿈.)


            moveDirection *= moveSpeed;
            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpSpeed;
            if (sliding == true)
                moveDirection += slidingDirection;            
        }
        
        moveDirection.y -= gravity * Time.deltaTime;
        con.Move(moveDirection * Time.deltaTime);

    }
    private void OnControllerColliderHit (ControllerColliderHit hit)
    {
        hitNormal = hit.normal;

        if ((Vector3.Angle(Vector3.up, hitNormal) > con.slopeLimit))
        {
            sliding = true;
            
            Vector3 c = Vector3.Cross(Vector3.up, hitNormal);
            Vector3 u = Vector3.Cross(c, hitNormal);
            //"왼손 규칙"은 두 벡터를 교차 곱 함수에 전달해야 하는 순서를 결정하는 데 사용할 수 있습니다.
            //표면의 윗면을 내려다 볼 때(즉 노멀이 가리키며 뻗어나가는 쪽에서 평면 방향을 보면)
            //첫 번째 벡터는 시계 방향으로 두 번째 벡터로 돌아야 합니다.
            //입력 벡터의 순서가 바뀌면 결과는 정확히 반대 방향을 가리킵니다.

            slidingDirection = u * slideSpeed;
        }
        else
        {
            sliding = false;
            slidingDirection = Vector3.zero;
        }
        
        if (hit.collider.gameObject.name == "Step3")
        {
            if (!healing)
            {
                healing = true;
                e = Instantiate(healEffect, transform.Find("FXPos"));
                Invoke("RemoveHealEffect", 1.9f);
            }
        }
    }

    void RemoveHealEffect()
    {
        Destroy(e.gameObject);
        healing = false;
    }
}
