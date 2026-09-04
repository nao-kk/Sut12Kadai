using UnityEngine;

public class CursorUnlocker : MonoBehaviour
{
    void Start()
    {
        // マウスの固定を解除して、自由に動かせるようにする
        Cursor.lockState = CursorLockMode.None;

        // マウスカーソルを画面に表示する
        Cursor.visible = true;
    }
}
