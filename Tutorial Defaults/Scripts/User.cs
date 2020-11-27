using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class User
{
    public string userName;
    public int totalContribute;

    public User()
    {
        userName = RegistrationManager.playerName;
        totalContribute = RegistrationManager.totalCommit;
    }
}