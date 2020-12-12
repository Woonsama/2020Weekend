using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LobbyView2D : MonoBehaviour
{
    [SerializeField]
    GameObject _characterRoot;

    private void Awake()
    {
        CreateCharacter();
    }

    public void CreateCharacter(string prefabName = "")

    { 
        var curUserData = UserManager.Instance.GetCurUserData();
        if (prefabName == "")
        {
            prefabName = curUserData.setCharPrefabName;
        }

        GameObject loadObj = Resources.Load("Prefab/" + prefabName) as GameObject;
        GameObject gameObj = GameObject.Instantiate(loadObj);

        LobbyCharacter lobbyCharacter = gameObj.GetComponent<LobbyCharacter>();
        gameObj.transform.SetParent(_characterRoot.transform);
        gameObj.transform.localScale = Vector3.one;
        gameObj.transform.localPosition = Vector3.zero;
        gameObj.transform.localEulerAngles = new Vector3(0, 180, 0);
        lobbyCharacter.SetAnim(LobbyCharacter.AnimType.Dance);

        if (curUserData.setHatPrefabName != "")
        {
            lobbyCharacter.CreateAcc(LobbyCharacter.AccessorieType.Hat, curUserData.setHatPrefabName);
        }

        UserManager.Instance.SetCharData(prefabName);
    }
}
