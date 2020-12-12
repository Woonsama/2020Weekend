using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Realtime;
public class LobbyCharacter : Photon.Pun.MonoBehaviourPunCallbacks
{
    public enum AccessorieType
    {
        Hat
    }

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
    GameObject _headSlot;

    Vector3 _hatPostOffset = new Vector3(-1.13f, 0, 0f);
    Dictionary<AnimType, int> _animDic = new Dictionary<AnimType, int>();
    Dictionary<AccessorieType, string> _boneDic = new Dictionary<AccessorieType, string>();

    string _curAnimType;

    private void Awake()
    {

        foreach(var bav in _animator.GetBehaviours<LobbyCharacterAnimBehaviour>())
        {
            bav.SetExitAction(OnExitAnimState);
        }

        _boneDic.Add(AccessorieType.Hat, "Character_Head");


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
    
    public void CreateAcc(AccessorieType acType,string prefabName)
    {
        foreach (Transform transform in _headSlot.transform)
        {
            Destroy(transform.gameObject);
        }

        GameObject loadObj = Resources.Load("Prefab/Acc/" + prefabName) as GameObject;
        GameObject gameObj = GameObject.Instantiate(loadObj,_headSlot.transform);
        gameObj.transform.localPosition = new Vector3(0, 0, 0);
        gameObj.transform.localPosition += _hatPostOffset;
        gameObj.transform.localEulerAngles = new Vector3(-73f, 175, -85);

        //gameObj.transform.position = _headSlot.transform.position;

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

 

}
