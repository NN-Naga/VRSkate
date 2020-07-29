using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlPotable : MonoBehaviour
{
    private Rigidbody controller;
    //[Range(-10f,10f)]private float lateralPos = 0f;
    private float lateralMin = -4.0f;
    private float lateralMax = 5.0f;
    private Vector3 bRot = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 globalDirection;
    PathControlPotable modelScript;

    public AudioClip hitSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<Rigidbody>();
        GameObject model = transform.parent.gameObject;
        modelScript = model.GetComponent<PathControlPotable>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)){
            if (bRot.y > 0)
                bRot.y = -10f;
            else
                bRot.y -= 10f;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (bRot.y < 0)
                bRot.y = 10f;
            else
                bRot.y += 10f;
        }

        moveDirection.z = PathControlPotable.addSpeed * Mathf.Sin(bRot.y * Mathf.Deg2Rad) * 10000;
        controller.velocity = moveDirection;
        moveDirection = Vector3.zero;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            PathControlPotable.addSpeed = -0.0005f;
            audioSource.PlayOneShot(hitSound);
        }
    }
}
