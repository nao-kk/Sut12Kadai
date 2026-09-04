using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -7);
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float mouseSensitivity = 2f; // マウスの感度

    private float rotationY = 0f; // カメラの水平回転の角度を保存する変数

    void Start()
    {
        // マウスカーソルを画面中央に固定して非表示にする（ゲームでよくある設定。Escキーで解除）
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // マウスの左右の移動量を取得
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        rotationY += mouseX; // 角度を蓄積する

        // マウスの角度に基づいた回転を作成する（プレイヤーの回転は無視する！）
        Quaternion cameraRotation = Quaternion.Euler(0, rotationY, 0);

        // プレイヤーの位置から、マウスで回転させた分のオフセットを計算して位置を決める
        Vector3 desiredPosition = player.position + (cameraRotation * offset);

        // 滑らかに移動
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // 常にプレイヤーの頭あたりを見つめる
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
