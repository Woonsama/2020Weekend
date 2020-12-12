using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : SingletonMonoBase<UserManager>
{
    public class UserData
    {
        public string setHatPrefabName = "";
        public string setCharPrefabName = "char_bear_brown";
    }

    UserData _userData;

    public UserData GetCurUserData()
    {
        if(_userData == null)
        {
            _userData = new UserData();
        }
        _userData.setCharPrefabName = PlayerPrefs.GetString("CharPrefab", "char_bear_brown");
        _userData.setHatPrefabName = PlayerPrefs.GetString("HatPrefab", "");
        return _userData;

    }

    public void SetCharData(string charPrefab)
    {
        PlayerPrefs.SetString("CharPrefab", charPrefab);
        _userData.setCharPrefabName = PlayerPrefs.GetString("CharPrefab");
    }

    public void SetHatData(string hatPrefab)
    {
        PlayerPrefs.SetString("HatPrefab", hatPrefab);
        _userData.setHatPrefabName = PlayerPrefs.GetString("HatPrefab");
    }

}
