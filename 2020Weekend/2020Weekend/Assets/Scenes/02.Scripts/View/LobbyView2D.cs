using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames;
using Photon.Pun;

public class LobbyView2D : MonoBehaviour 
{
    [SerializeField]
    GameObject _characterRoot;


    public void CreateCharacter(string prefabName)
    {
        foreach(Transform transform in _characterRoot.transform)
        {
            Destroy(transform.gameObject);
        }

        GameObject loadObj = Resources.Load("Prefab/" + prefabName) as GameObject;
        GameObject gameObj = GameObject.Instantiate(loadObj);
      
        gameObj.transform.SetParent(_characterRoot.transform);
        gameObj.transform.localScale = Vector3.one;
        gameObj.transform.localPosition = Vector3.zero;
        gameObj.transform.rotation = new Quaternion(0, 0, 0, 0);
        gameObj.GetComponent<LobbyCharacter>().SetAnim(LobbyCharacter.AnimType.Dance);
    }

    private void Update()
    {
      //  if(Input.GetKeyDown("escape"))
         //   UnityEngine.SceneManagement.LoadSCe
    }
}
