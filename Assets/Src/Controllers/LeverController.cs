using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    [SerializeField] private Transform LightsOnOPrefab;
    [SerializeField] private Material PortalOnMaterial;
    [SerializeField] private AudioClip StartUPSound;
    private AudioSource audioSource;
    private wTags tags;
    private wHints hints;
    private wNames names;
    private bool canUse = false;
    private bool leverDown = false;
    private GuiController wGui;
    private PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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


        GameObject[] portalObjects = GameObject.FindGameObjectsWithTag(tags.Portal);
        foreach(GameObject portal in portalObjects)
        {
            MeshRenderer meshRenderer = portal.GetComponent<MeshRenderer>();
            meshRenderer.material = PortalOnMaterial;
            GameObject portalOnChild = portal.transform.Find(names.PortalRingOn).gameObject;
            portalOnChild.SetActive(true);
        }
        audioSource.PlayOneShot(StartUPSound);
    }

    private void TurnLever()
    {
        transform.Rotate(-45f, 0.0f, 0.0f, Space.Self);
        wGui.clearHint();
        canUse = false;
        leverDown = true;
        _playerController.UseLever();
        TurnPowerOn();
    }


    private void OnTriggerEnter(Collider other)
    {
        string wtag = other.tag;
        if (wtag == tags.Player && !leverDown)
        {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (playerController.HasAllPowercells())
            {
                _playerController = playerController;
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
        string wtag = other.tag;
        if (wtag == tags.Player)
        {
            wGui.clearHint();
            canUse = false;
        }
    }
}
