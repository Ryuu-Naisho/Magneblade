using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobUtils : MonoBehaviour
{


    [SerializeField] private float bobbingRate;
    [SerializeField] private float idle_speed;
    [SerializeField] private float run_speed;
    [SerializeField] private PlayerController playerController;
    private float defaultPosY = 0;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
         if(!playerController.isIdle)
        {
            Bounce(run_speed);
        }
        else
        {
            Bounce(idle_speed);
        }
    }


    
    ///<summary>Slowly rest head from bouncing.</summary>
    ///<param name="speed">Walking speed of the object.</param>
    public void Idle(float speed)
    {
        timer = 0;
        transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, defaultPosY, Time.deltaTime * speed), transform.localPosition.z);
    }


    ///<summary>Bounce transform by bobbingRate and speed.</summary>
    ///<param name="speed">Speed of object.</param>
    private void Bounce(float speed)
    {
        timer += Time.deltaTime * speed;
        transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + Mathf.Sin(timer) * bobbingRate, transform.localPosition.z);
    }
}
