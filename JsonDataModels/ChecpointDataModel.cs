using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChecpointDataModel 
{
   public int totalCoin;
   public List<CheckPointData> checkpoints;

   public ChecpointDataModel()
    {
        checkpoints = new List<CheckPointData>();
    }
}

public class CheckPointData
{
    public int index;
    public float x,y,z;
     public CheckPointData(Vector3 pos , int idx)
    {
        index = idx;
        x = pos.x;
        y = pos.y;
        z = pos.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x,y,z);
    }
}