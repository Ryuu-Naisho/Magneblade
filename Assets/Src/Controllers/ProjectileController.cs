using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class ProjectileController : MonoBehaviour
{


    public float m_Speed = 10f;   // this is the projectile's speed
    public float m_Lifespan = 3f; // this is the projectile's lifespan (in seconds)
    private wTags tags;
 
     /// <summary>
     /// Private fields
     /// </summary>
     private Rigidbody m_Rigidbody;
 
     /// <summary>
     /// Message that is called when the script instance is being loaded
     /// </summary>
     void Awake()
     {
         m_Rigidbody = GetComponent<Rigidbody>();
     }
 
     /// <summary>
     /// Message that is called before the first frame update
     /// </summary>
     void Start()
     {
         tags = new wTags();
         m_Rigidbody.AddForce(m_Rigidbody.transform.forward * m_Speed);
         Destroy(gameObject, m_Lifespan);
     }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        string wtag = collision.gameObject.tag;
        if (wtag == tags.Enemy)
        {
            EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
            enemyController.Hit();
            Destroy(gameObject, 1f);
        }
    }
}
