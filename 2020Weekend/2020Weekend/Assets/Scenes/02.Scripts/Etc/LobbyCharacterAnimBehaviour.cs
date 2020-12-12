using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacterAnimBehaviour : StateMachineBehaviour
{
    System.Action _exitAction;

    public void SetExitAction(System.Action action)
    {
        _exitAction = action;
    }


    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _exitAction?.Invoke();
    }


}
