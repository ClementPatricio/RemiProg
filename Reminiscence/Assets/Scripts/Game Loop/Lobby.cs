using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene("Lobby", LoadSceneMode.Single);

        GameManager.instance.getLog().Post(GameManager.instance.gameObject, (uint)AkCallbackType.AK_EndOfEvent, CallbackFunction);
        GameManager.instance.ikMotor.pointToReach.transform.position = GameManager.instance.PTRStartPos;
        /*Debug.Log(GameManager.instance.newMatrix.parameterMatrix);

        Debug.Log(GameManager.instance.chosenTranslate);
        Debug.Log(new Vector3(GameManager.instance.newMatrix.TranslatePosition(GameManager.instance.chosenTranslate).x * GameManager.instance.sensitivity.x,
                              GameManager.instance.newMatrix.TranslatePosition(GameManager.instance.chosenTranslate).y * GameManager.instance.sensitivity.y,
                              GameManager.instance.newMatrix.TranslatePosition(GameManager.instance.chosenTranslate).z * GameManager.instance.sensitivity.z));*/



    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

    public void CallbackFunction(object in_cookie, AkCallbackType in_type, object in_info)
    {
        GameManager.instance.StartLevel();
    }
}

