using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float minDistance = 3.0f; // 最小距離
    [SerializeField] private float maxDistance = 10.0f; // 最大距離
    [SerializeField] private float moveThreshold = 0.5f; // 移動しないと判断する閾値
    [SerializeField] private float smoothTime = 0.2f; // スムージングの時間

    private GameObject playerObj;
    private Vector3 dir;
    private Vector3 velocity = Vector3.zero; // スムージング用の速度ベクトル
    private Vector3 lastPlayerPosition; // 前フレームのプレイヤーの位置

    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Box");

        dir = new Vector3(0, 0.2f, -0.6f);
    }

    // LateUpdateを使用してカメラの位置をプレイヤーに追従させる
    void LateUpdate()
    {
        // プレイヤーの位置の変化を計算
        float playerPositionChange = Vector3.Distance(playerObj.transform.position, lastPlayerPosition);

        // プレイヤーの位置が閾値を超えて移動した場合のみカメラを動かす
        if (playerPositionChange > moveThreshold)
        {
            // カメラとプレイヤーの距離を計算
            float currentDistance = Vector3.Distance(transform.position, playerObj.transform.position);

            // 最小距離から最大距離の範囲内に制限
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

            // ターゲットの位置を計算
            Vector3 targetPosition = playerObj.transform.position + dir * currentDistance;

            // スムージングを適用して位置を更新
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

        // プレイヤーの位置を保存
        lastPlayerPosition = playerObj.transform.position;
    }

    private void Update()
    {
        //二つのオブジェクト間のベクトルを得ます
        Vector3 _difference = (playerObj.transform.position - this.transform.position);
        //.normalizedベクトルの正規化を行う
        Vector3 _direction = _difference.normalized;
        // Ray(開始地点,進む方向)
        Ray _ray = new Ray(this.transform.position, _direction);
        
        
    }
}