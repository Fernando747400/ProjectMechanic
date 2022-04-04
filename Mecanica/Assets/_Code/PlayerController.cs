using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float Gravity;
    [SerializeField] private LayerMask groundMask;
    private RaycastHit sphereHit;
    private RaycastHit rayHit;
    private bool Grounded = true;
    private float verticalSpeed = 0;

    void Update()
    {
        Jump();
        checkInput();
    }

    void checkInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.World);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * Time.deltaTime * Speed, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * Time.deltaTime * Speed, Space.World);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * Time.deltaTime * Speed, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            verticalSpeed = JumpHeight;
            this.transform.Translate(Vector3.up * Time.deltaTime * verticalSpeed, Space.World);
        } else if(!Grounded)
        {
            verticalSpeed -= Gravity * Time.deltaTime;
            if(verticalSpeed < 0)
            {
                Falling();
            }
            else
            {
                this.transform.Translate(Vector3.up * verticalSpeed, Space.World);
            }
        }
    }

    void Jump()
    {
       if (Physics.SphereCast(this.transform.position,0.1f,Vector3.down,out sphereHit,0.5f,groundMask.value,QueryTriggerInteraction.UseGlobal))
        {
            Grounded = true;
            verticalSpeed = 0;
            this.transform.position = new Vector3(this.transform.position.x, sphereHit.transform.position.y + sphereHit.transform.localScale.y/2 + this.transform.localScale.y + 0.1f, this.transform.position.z);
        }
        else
        {
            Grounded = false;
        }
    }

    void Falling()
    {
        if (Physics.Raycast(this.transform.position,Vector3.down,out rayHit, Mathf.Abs(verticalSpeed), groundMask.value))
        {
            this.transform.position = new Vector3(this.transform.position.x, rayHit.transform.position.y + rayHit.transform.localScale.y/2 + this.transform.localScale.y + 0.1f, this.transform.position.z);
        } else
        {
            this.transform.Translate(Vector3.up * verticalSpeed, Space.World);
        }
    }
}
