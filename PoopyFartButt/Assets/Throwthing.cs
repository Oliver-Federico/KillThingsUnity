using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwthing : MonoBehaviour
{
    [SerializeField] GameObject throwthingprefab;
    public GameObject throwthingPOS;
    public float throwSpeed;
    private Vector3 target;
    private Rigidbody trb;

    private Vector3 rotation;
    void Start()
    {
        trb = throwthingprefab.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rotation = new Vector3(-90f, throwthingPOS.transform.rotation.y, 0);
            var throwThing = Instantiate(throwthingprefab, throwthingPOS.transform.position, Quaternion.Euler(rotation));
            trb.AddForce(Vector3.up * throwSpeed, ForceMode.Acceleration);
        }
    }
}
