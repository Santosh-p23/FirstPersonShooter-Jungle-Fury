using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowScript: MonoBehaviour
{
    private Rigidbody myBody;

    public float speed = 30.0f;

    public float deactivate_Timer = 3f;

    public float damage = 15f;

     void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Invoke("DeactivateGameObject", deactivate_Timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider target)
    {
        
    }

    public void Launch(Camera mainCamera)
    {
        myBody.velocity = mainCamera.transform.forward * speed;

        transform.LookAt(transform.position + myBody.velocity);
    }
}
