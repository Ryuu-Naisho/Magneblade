using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    private wTags tags;
    private wHints hints;
    private GuiController wGui;
    private wNames names;
    private SceneController sceneController;
    private bool canUse = false;
    // Start is called before the first frame update
    void Start()
    {
        sceneController = GetComponent<SceneController>();
        tags = new wTags();
        hints = new wHints();
        names = new wNames();
        GameObject GUIObject = GameObject.Find(names.GUI);
        wGui = GUIObject.GetComponent<GuiController>();
    }

    // Update is called once per frame
    void Update()
    {
         if (canUse)
        {
            if (Input.GetKeyDown("e"))
            {
                Cursor.lockState = CursorLockMode.None;
                sceneController.CompleteScene();
            }
        }   
    }


    private void OnTriggerEnter(Collider other)
    {
        string wtag = other.tag;
        if (wtag == tags.Player)
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.UsedLever())
            {
                wGui.SetHint(hints.PortalInstructions);
                canUse = true;
            }
            else
            {
                wGui.SetHint(hints.PortalRequirements);
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        string wtag = other.tag;
        if (wtag == tags.Player)
        {
            wGui.clearHint();
            canUse = false;
        }
    }
}
