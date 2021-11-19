using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHandController : MonoBehaviour
{
    private wTags tags;



    // Start is called before the first frame update
    void Start()
    {
        tags = new wTags();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        tag = collision.gameObject.tag;
        if (tag == tags.Player)
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.Hit();
        }
    }
}
