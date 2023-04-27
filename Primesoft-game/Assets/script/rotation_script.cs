using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_script : MonoBehaviour
{
    private soundManager soundManager;
    private float rotY;
    private goal_script goal_script;
    public float rotationSpeed = 100f;
    void Start()
    {
        soundManager = GameObject.Find("soundManager").GetComponent<soundManager>();
        goal_script = GameObject.Find("goal").GetComponent<goal_script>();
    }

    void Update()
    {
        rotY += Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, rotY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            soundManager.Play("key");
            goal_script.key = true;
            Destroy(gameObject);
        }
    }
    
}
