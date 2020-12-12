using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacter : MonoBehaviour
{
    public enum AnimType
    {
        Angry,
        Dance,
        Frustrate,
        Idle,
        Jump,
        Knock,
        Run,
        Victory
    }
    [SerializeField]
    Animator _animator;
    //AnimType, random Anim count

    [SerializeField]
    AnimType _test;

    Dictionary<AnimType, int> _animDic = new Dictionary<AnimType, int>();

    string _curAnimType;

    private void Awake()
    {

        foreach(var bav in _animator.GetBehaviours<LobbyCharacterAnimBehaviour>())
        {
            bav.SetExitAction(OnExitAnimState);
        }

        _animDic.Add(AnimType.Angry, 2);
        _animDic.Add(AnimType.Idle, 1);
        _animDic.Add(AnimType.Jump, 2);
        _animDic.Add(AnimType.Dance, 1);
        _animDic.Add(AnimType.Run, 1);
        _animDic.Add(AnimType.Victory, 1);
        _animDic.Add(AnimType.Knock, 1);
        _animDic.Add(AnimType.Frustrate, 1);
    }

    public void SetAnim(AnimType animType)
    {
        string animName = "";
        int value = -1;
        if (_animDic.TryGetValue(animType,out value) != false)
        {
            int animNum = 0;
            if(value > 1)
            {
                animNum = UnityEngine.Random.Range(1, value+1);
            }
            
            if(animNum != 0)
            {
                animName = animType + animNum.ToString();
            }
            else
            {
                animName = animType.ToString();
            }
        }

        _animator.SetBool(animName, true);
        _curAnimType = animName;

    }

    void OnExitAnimState()
    {
        ResetCurAnimType();
        SetAnim(AnimType.Idle);

   //     _animator.Rebind();
    }

    void ResetCurAnimType()
    {
        _animator.SetBool(_curAnimType, false);
    }


    private void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            SetAnim(_test);
        }
    }
}
