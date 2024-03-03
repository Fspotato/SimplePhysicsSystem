using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomColliderBox : MonoBehaviour
{
    [SerializeField] Bounds bounds;
    [SerializeField] Vector3 centerOffset;
    [SerializeField] Color boundsColor = Color.yellow;

    public Bounds Bounds => bounds;
    public Vector3 CenterOffset => centerOffset;

    void Awake()
    {
        // 把碰撞事件委託給碰撞管理器觸發
        CollisionManager.AddColliders(this, OnCollision);
    }

    void Update()
    {
        bounds.center = transform.position + centerOffset;
    }

    public bool CollisionCheck(CustomColliderBox other)
    {
        // 碰撞檢測
        // 先取得此碰撞箱bounds距離與另一個碰撞箱中心點最近的點(可能在箱子內或箱子上)
        // 再計算這個點和另一個碰撞箱的距離 若距離為零 則判定碰撞
        return other.bounds.SqrDistance(bounds.ClosestPoint(other.bounds.center)) == 0;
    }

    void OnCollision(CustomColliderBox other)
    {
        // 碰撞後觸發
    }

    #region Editor

    void Reset()
    {
        // 根據當前物件上的Renderer獲得Bounds範圍及中心偏移值 如果沒有Renderer組件 則設為0
        centerOffset = GetComponentInChildren<Renderer>() ? GetComponentInChildren<Renderer>().bounds.center - transform.position : Vector3.zero;
        bounds = GetComponentInChildren<Renderer>() ? GetComponentInChildren<Renderer>().bounds : new Bounds();
    }

    void OnDrawGizmosSelected()
    {
        bounds.center = transform.position + centerOffset;
        /* 寫完才發現有API可以調 紀念用
        Vector3 p1 = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, bounds.extents.z);
        Vector3 p2 = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, bounds.extents.z);
        Vector3 p3 = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, bounds.extents.z);
        Vector3 p4 = bounds.center + new Vector3(-bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
        Vector3 p5 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, -bounds.extents.z);
        Vector3 p6 = bounds.center + new Vector3(bounds.extents.x, bounds.extents.y, -bounds.extents.z);
        Vector3 p7 = bounds.center + new Vector3(bounds.extents.x, -bounds.extents.y, bounds.extents.z);
        Vector3 p8 = bounds.center + new Vector3(-bounds.extents.x, bounds.extents.y, -bounds.extents.z);
        Debug.DrawLine(p1, p2, boundsColor);
        Debug.DrawLine(p1, p6, boundsColor);
        Debug.DrawLine(p1, p7, boundsColor);
        Debug.DrawLine(p2, p3, boundsColor);
        Debug.DrawLine(p2, p8, boundsColor);
        Debug.DrawLine(p3, p4, boundsColor);
        Debug.DrawLine(p3, p7, boundsColor);
        Debug.DrawLine(p4, p5, boundsColor);
        Debug.DrawLine(p4, p8, boundsColor);
        Debug.DrawLine(p5, p7, boundsColor);
        Debug.DrawLine(p5, p6, boundsColor);
        Debug.DrawLine(p6, p8, boundsColor);*/
        Handles.color = boundsColor;
        Handles.DrawWireCube(bounds.center, bounds.extents * 2);
    }

    #endregion
}
