using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionIndicator : MonoBehaviour
{
    GameObject controller;
    ExplosionController exController;

    private void Start()
    {
        controller = transform.parent.gameObject;
        exController = controller.GetComponent<ExplosionController>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("in");
            StartCoroutine(exController.Indicate());
        }
    }
}
