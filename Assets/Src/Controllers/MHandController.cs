using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MHandController : MonoBehaviour
{
    [SerializeField] private GunController gunController;
    private wTags tags;
    private int bladeCount = 0;
    private bool coolingdown = false;



    // Start is called before the first frame update
    void Start()
    {
        tags = new wTags();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunController.Fire();
        }
    }


    public int GetBladeCount()
    {
        return this.bladeCount;
    }


    public void RemoveBlade()
    {
        bladeCount --;
    }


    public void CollectBlade()
    {
        bladeCount ++;
    }

    private BladeController GetBlade(Collider other)
    {
        BladeController blade_controller = other.gameObject.GetComponent<BladeController>();
        return blade_controller;
    }



    private void OnTriggerEnter(Collider other)
    {
        tag = other.tag;
        if (tag == tags.Blade)
        {
            BladeController blade_controller = GetBlade(other);
            blade_controller.DoAttraction(transform);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        tag = other.tag;
        if (tag == tags.Blade)
        {
            BladeController blade_controller = GetBlade(other);
            blade_controller.StopAttraction();
        }
    }


    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        onComplete();
    }
}
