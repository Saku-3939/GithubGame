using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class User
{
    public string userName;
    public int totalContribute;
    public double stddev;
    public int followers;
    public int repositoryCount;
    public string identifyName;
    public int gameScore;

    public User()
    {
        
        totalContribute = RegistrationManager.totalCommit;
        repositoryCount = RegistrationManager.totalRepository;
        stddev = RegistrationManager.standartDeviation;
        followers = RegistrationManager.followerCount;
        userName = RegistrationManager.playerName;
        identifyName = ResultManager.identifyName;
        gameScore = ResultManager.gameScore;
    }
}