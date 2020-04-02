using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Rigidbody controller;
    //[Range(-10f,10f)]private float lateralPos = 0f;
    private float lateralMin = -4.0f;
    private float lateralMax = 5.0f;
    private Vector3 bRot;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 globalDirection;
    PathControl modelScript;

    public AudioClip hitSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Rigidbody>();
        GameObject model = transform.parent.gameObject;
        modelScript = model.GetComponent<PathControl>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        bRot = modelScript.baseObject.transform.localEulerAngles;
        
        //Debug.Log("bRot = "+ bRot.y+ "  sin(bRot) = " +Mathf.Sin(bRot.y * Mathf.Deg2Rad) + "   addSpeed = " + PathControl.addSpeed + "   moveDirection = " + moveDirection);
        moveDirection.z = PathControl.addSpeed * Mathf.Sin(bRot.y * Mathf.Deg2Rad) * 10000;
        controller.velocity = moveDirection;
        moveDirection = Vector3.zero;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            PathControl.addSpeed = -0.0005f;
            audioSource.PlayOneShot(hitSound);
        }
    }
}
