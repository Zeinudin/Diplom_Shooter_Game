using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{

    public CharacterController controller;
    public Animator animator;
    public float speedMove = 3f;
    public float speedRatation = 180f;
    public Gun gun;
    public float minY = -20f;
    public float maxY = 20f;
    private float currentY;

    // Start is called before the first frame update
    void Start()
    {
        currentY = Camera.main.transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            float vertical = Input.GetAxis("Vertical");
            float mx = Input.GetAxis("Mouse X");
            float my = Input.GetAxis("Mouse Y");
            if(vertical != 0)
            {
                controller.Move(transform.forward * vertical * speedMove * Time.deltaTime);
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
            if(mx != 0)
            {
                transform.Rotate(transform.up * mx * speedRatation * Time.deltaTime);
            }

            if(my != 0)
            {
                currentY = Mathf.Clamp(currentY - my * speedRatation * Time.deltaTime, minY, maxY);
                Vector3 camRotation = Camera.main.transform.rotation.eulerAngles;
                Camera.main.transform.rotation = Quaternion.Euler(currentY, camRotation.y, camRotation.z);
            }

            if(Input.GetMouseButton(0))
            {
                gun.shoot();
                animator.SetBool("Shoot", true);
            }
            else
            {
                animator.SetBool("Shoot", false);
            }

        }
        controller.Move(Physics.gravity * Time.deltaTime);
    }


    public void damage()
    {
        GameManager.instance.deadUnity(gameObject);
    }
}
