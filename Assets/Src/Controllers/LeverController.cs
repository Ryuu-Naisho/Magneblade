using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private GuiController wGui;
    private wTags tags;
    private wHints hints;
    private bool canUse = false;
    private bool leverDown = false;
    // Start is called before the first frame update
    void Start()
    {
        tags = new wTags();
        hints = new wHints();
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


    private void TurnLever()
    {
        transform.Rotate(-45f, 0.0f, 0.0f, Space.Self);
        wGui.clearHint();
        canUse = false;
        leverDown = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        tag = other.tag;
        if (tag == tags.Player && !leverDown)
        {
            wGui.SetHint(hints.LeverHint);
            canUse = true;
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
