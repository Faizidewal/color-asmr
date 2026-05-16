using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DataSO", menuName = "GameDataSO/DataSO")]
public class DataSO : ScriptableObject
{
    public static DataSO instance;
    public static DataSO Instance
    {
        get
        {
            if (instance == null)
                instance = Resources.Load("DataSO") as DataSO;
            return instance;
        }
    }
}
