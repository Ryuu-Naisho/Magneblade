using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioUtil))]
public class NPCHandController : MonoBehaviour
{
    [SerializeField] AudioClip[] HitClips;
    private AudioUtil audioUtil;
    private wTags tags;



    // Start is called before the first frame update
    void Start()
    {
        audioUtil = GetComponent<AudioUtil>();
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
            AudioClip clip = audioUtil.GetRandomClip(HitClips);
            audioUtil.PlayOneShot(clip);
            playerController.Hit();
        }
    }
}
