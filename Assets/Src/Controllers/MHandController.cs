using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MHandController : MonoBehaviour
{
    [SerializeField] private GunController gunController;
    [SerializeField] private int maxBlades;
    [SerializeField] private int maxPowercells;
    private wTags tags;
    private int bladeCount = 0;
    private int powercellCount = 0;
    private PlayerController playerController;



    // Start is called before the first frame update
    void Start()
    {
        tags = new wTags();
        Transform parentTransform = transform.root.transform;
        playerController = parentTransform.gameObject.GetComponent<PlayerController>();
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

    public int GetPowerCellCount()
    {
        return this.powercellCount;
    }

    public int GetMaxPowerCells()
    {
        return this.maxPowercells;
    }


    public void RemoveBlade()
    {
        bladeCount --;
    }


    public void CollectBlade(GameObject collectableObjects)
    {
        BladeController bladeController = collectableObjects.GetComponent<BladeController>();
        if (bladeCount < maxBlades)
        {
            bladeCount ++;
            bladeController.Destroy();
        }
        else
        {
            playerController.Hit();
            bladeController.Reject();
        }
    }


    public void CollectPowercell(GameObject collectableObjects)
    {
        BladeController bladeController = collectableObjects.GetComponent<BladeController>();
        if (powercellCount < maxPowercells)
        {
            powercellCount ++;
            bladeController.Destroy();
        }
        else
        {
            playerController.Hit();
            bladeController.Reject();
        }
    }

    private BladeController GetBlade(Collider other)
    {
        BladeController blade_controller = other.gameObject.GetComponent<BladeController>();
        return blade_controller;
    }



    private void OnTriggerEnter(Collider other)
    {
        string wtag = other.tag;
        if (wtag == tags.Blade || wtag == tags.Powercell)
        {
            BladeController blade_controller = GetBlade(other);
            blade_controller.DoAttraction(transform);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        string wtag = other.tag;
        if (wtag == tags.Blade || wtag == tags.Powercell)
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
