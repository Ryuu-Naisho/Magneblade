using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private Transform LightsOnOPrefab;
    private wTags tags;
    private wHints hints;
    private wNames names;
    private bool canUse = false;
    private bool leverDown = false;
    private GuiController wGui;
    // Start is called before the first frame update
    void Start()
    {
        tags = new wTags();
        hints = new wHints();
        names = new wNames();
        GameObject GUIObject = GameObject.Find(names.GUI);
        wGui = GUIObject.GetComponent<GuiController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canUse && !leverDown)
        {
            if (Input.GetKeyDown("e"))
            {
                TurnLever();
            }
        }
    }


    private void TurnPowerOn()
    {
        GameObject[] toggleableLights = GameObject.FindGameObjectsWithTag(tags.ToggleableLights);
        foreach(GameObject light in toggleableLights)
        {
            Vector3 current_position = light.transform.position;
            var current_rotation = light.transform.rotation;
            Destroy(light);
            Instantiate(LightsOnOPrefab, current_position, current_rotation);
        }
    }

    private void TurnLever()
    {
        transform.Rotate(-45f, 0.0f, 0.0f, Space.Self);
        wGui.clearHint();
        canUse = false;
        leverDown = true;
        TurnPowerOn();
    }


    private void OnTriggerEnter(Collider other)
    {
        tag = other.tag;
        if (tag == tags.Player && !leverDown)
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.HasAllPowercells())
            {
                wGui.SetHint(hints.LeverHint);
                canUse = true;
            }
            else
            {
                wGui.SetHint(hints.NeedPowercells);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        tag = other.tag;
        if (tag == tags.Player)
        {
            wGui.clearHint();
            canUse = false;
        }
    }
}
