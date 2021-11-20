using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{


    [SerializeField] private float cooldown_time;
    [SerializeField] private MHandController mHandController;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform muzzleTransform;
    [SerializeField] private Transform gunTransform;
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private wAnimatorUtils animator;
    private AudioUtil audioUtil;
    private int bladeCount = 0;
    private bool coolingdown = false;



    // Start is called before the first frame update
    void Start()
    {
        audioUtil = GetComponent<AudioUtil>();
    }

    // Update is called once per frame
    void Update()
    {
        bladeCount = mHandController.GetBladeCount();
    }


    private void CoolDown()
    {
        coolingdown = true;
        Action finish_cooldown = ()=>{
            coolingdown = false;
        };

        StartCoroutine(Wait(cooldown_time, finish_cooldown));

    }


    public void Fire()
    {
        if (bladeCount > 0 && !coolingdown)
        {
        animator.Fire();
        Camera camera = Camera.main;
        Vector3 mousePos = Input.mousePosition;
        Vector3 aim = camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, muzzleTransform.position.z));
        Vector3 projectile_position = new Vector3(muzzleTransform.position.x,  muzzleTransform.position.y, muzzleTransform.position.z);
        GameObject projectile_object = Instantiate(projectile, projectile_position, muzzleTransform.rotation);
        mHandController.RemoveBlade();
        DoFireSound();
        CoolDown();
        }
    }


    private AudioClip PickRandomClip()
    {
        int size = clips.Length;
        int random_index = UnityEngine.Random.Range(0,size);
        AudioClip clip = clips[random_index];
        return clip;
    }


    private void DoFireSound()
    {
        AudioClip clip = PickRandomClip();
        audioUtil.PlayOneShot(clip);
    }


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
