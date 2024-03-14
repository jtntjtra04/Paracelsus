using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    public float speed;

    Transform player;
    Rigidbody2D body;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // find object with player tag
        body = animator.GetComponent<Rigidbody2D>(); // find rigidbody component from animator component in slime
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, body.position.y); // to find the target which is player
        Vector2 new_position = Vector2.MoveTowards(body.position, target, speed * Time.fixedDeltaTime); // Update new position
        body.MovePosition(new_position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
