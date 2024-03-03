using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CustomRigidBody : MonoBehaviour
{
    readonly float g = 9.8f;
    [SerializeField] public Vector3 velocity = Vector3.zero;
    [SerializeField] Vector3 deltaMove = Vector3.zero;
    [SerializeField] float gapValue = 1f;
    CustomColliderBox colliderBox;

    void Awake()
    {
        colliderBox = GetComponentInChildren<CustomColliderBox>();
        if (colliderBox == null) return;
        CollisionManager.AddColliders(colliderBox, OnCollsion);
    }


    void Update()
    {
        UpdateGravity(Time.deltaTime);
    }

    // 在碰撞判定後進行
    // 可以避免在碰撞體邊緣抖動的情況
    // 順序：給予剛體某個方向的力 > 碰撞判定(將力歸零或不變) > 更新剛體位置
    void FixedUpdate()
    {
        UpdateMovement(Time.deltaTime);
    }

    // 更新重力
    private void UpdateGravity(float deltatime)
    {
        velocity.y -= g * deltatime;
    }

    // 根據velocity移動
    private void UpdateMovement(float deltatime)
    {
        deltaMove = velocity * deltatime;
        transform.Translate(deltaMove, Space.World);
    }

    // 碰撞事件
    // 依碰撞點的單位向量判定是哪個方向的碰撞 由此為依據讓剛體物件無法走進/掉入碰撞箱內
    void OnCollsion(CustomColliderBox other)
    {
        float yGap = other.Bounds.center.y + other.Bounds.extents.y - (colliderBox.Bounds.center.y - colliderBox.Bounds.extents.y);

        if (yGap <= gapValue)
        {
            velocity.y = Mathf.Max(0, velocity.y);
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Max(transform.position.y, other.Bounds.center.y + other.Bounds.extents.y + colliderBox.Bounds.extents.y - colliderBox.CenterOffset.y),
                transform.position.z);
        }

        if (yGap >= gapValue && colliderBox.Bounds.center.x + colliderBox.Bounds.extents.x >= other.Bounds.center.x + other.Bounds.extents.x)
        {
            velocity.x = Mathf.Max(0, velocity.x);
        }

        if (yGap >= gapValue && colliderBox.Bounds.center.x - colliderBox.Bounds.extents.x <= other.Bounds.center.x - other.Bounds.extents.x)
        {
            velocity.x = Mathf.Min(0, velocity.x);
        }

        if (yGap >= gapValue && colliderBox.Bounds.center.z + colliderBox.Bounds.extents.z >= other.Bounds.center.z + other.Bounds.extents.z)
        {
            velocity.z = Mathf.Max(0, velocity.z);
        }

        if (yGap >= gapValue && colliderBox.Bounds.center.z - colliderBox.Bounds.extents.z <= other.Bounds.center.z - other.Bounds.extents.z)
        {
            velocity.z = Mathf.Min(0, velocity.z);
        }
    }
}
