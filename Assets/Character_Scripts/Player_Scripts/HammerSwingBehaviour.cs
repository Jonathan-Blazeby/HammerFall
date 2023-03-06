using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerSwingBehaviour : StateMachineBehaviour
{
    [SerializeField] private AudioClip swingAudioClip;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("WeaponActive", true);
        AudioSource source = animator.GetComponentInChildren<AudioSource>();
        if(source.clip is null) { source.clip = swingAudioClip; }
        source.Play();
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("WeaponActive", false);
    }
}
