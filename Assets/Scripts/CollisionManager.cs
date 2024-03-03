using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // 碰撞事件委託 以CustomColliderBox為觸發索引 所以要觸發碰撞一定要掛載CustomColliderBox
    public static Dictionary<CustomColliderBox, Action<CustomColliderBox>> OnCollision = new Dictionary<CustomColliderBox, Action<CustomColliderBox>>();

    CustomColliderBox[] collidableObjects;

    // 供外部調用 用於掛載碰撞事件委託的API
    public static void AddColliders(CustomColliderBox collider, Action<CustomColliderBox> action)
    {
        if (OnCollision.ContainsKey(collider)) OnCollision[collider] += action;
        else OnCollision.Add(collider, action);
    }

    void Start()
    {
        collidableObjects = FindObjectsOfType<CustomColliderBox>();
    }

    void FixedUpdate()
    {
        CollisionCheck();
    }

    // 碰撞檢測 待優化(八叉樹)
    void CollisionCheck()
    {
        for (int i = 0; i < collidableObjects.Length; i++)
            for (int j = 0; j < collidableObjects.Length; j++)
            {
                if (i == j) continue;
                if (collidableObjects[i].CollisionCheck(collidableObjects[j]))
                {
                    OnCollision[collidableObjects[i]](collidableObjects[j]);
                }
            }
    }
}
