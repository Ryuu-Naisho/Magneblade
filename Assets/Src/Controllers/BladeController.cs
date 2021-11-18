using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioUtil))]
public class BladeController : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private wAnimatorUtils player_hand_animator;
    [SerializeField] private float rejectionForce;
    public int speed;
    private AudioUtil audioUtil;
    private bool attract = false;
    private bool collected = false;
    private Transform player_hand_transform;
    private wTags tags;
    private int rejectionForceReached = 0;
    private bool rejected = false;

    // Start is called before the first frame update
    void Start()
    {
        audioUtil = GetComponent<AudioUtil>();
        tags = new wTags();
    }

    // Update is called once per frame
    void Update()
    {
        if (attract && !rejected)
        {
            follow();
            player_hand_animator.Attract();
        }
        else
        {
            player_hand_animator.StopAttract();
        }


        if (rejected)
        {
            ApplyRectionForce();
        }


        if (player_hand_transform != null && !collected)
        {
            if (Vector3.Distance(transform.position, player_hand_transform.position) < 1.5f)
            {
                DoCollection();
            }
        }
    }


    private AudioClip PickRandomClip()
    {
        int size = clips.Length;
        int random_index = Random.Range(0,size);
        AudioClip clip = clips[random_index];
        return clip;
    }


    public void DoAttraction(Transform _player_hand_transform)
    {
        attract = true;
        player_hand_transform = _player_hand_transform;
        if (!audioUtil.isPlaying)
        {
            AudioClip clip = PickRandomClip();
            audioUtil.Play(clip);
        }
    }


    public void Destroy()
    {
        Destroy(gameObject, 0.01f);
    }


    private void ApplyRectionForce()
    {
        if (rejectionForceReached <= rejectionForce)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - player_hand_transform.position);
            float step = (speed*speed) * Time.deltaTime;
            transform.position += Vector3.forward * Time.deltaTime * (speed*speed);
            rejectionForceReached ++;
        }
        else
        {
            rejected = false;
            collected = false;
            rejectionForceReached = 0;
        }
    }


    public void Reject()
    {
        rejected = true;
    }


    private void DoCollection()
    {
        MHandController handController = player_hand_transform.gameObject.GetComponent<MHandController>();
        if (tag == tags.Blade)
        {
            handController.CollectBlade(gameObject);
        }
        else if (tag == tags.Powercell)
        {
            handController.CollectPowercell(gameObject);
        }
        attract = false;
        collected = true;
    }

    private void follow()
    {
        transform.LookAt(player_hand_transform);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player_hand_transform.position, step);
    }


    public void StopAttraction()
    {
        attract = false;
    }
}
