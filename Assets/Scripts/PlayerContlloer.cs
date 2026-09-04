using UnityEngine;

public class PlayerContlloer : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpForce = 5f; // ジャンプの強さ

    private Rigidbody rb;
    private bool isGrounded; // 地面に着地しているか

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // 【重要】プログラム側からも、物理演算で勝手にカプセルが転がらないようにロックします
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // ジャンプの入力検知は、FixedUpdateではなくUpdateで行うのがUnityの基本（入力の聞き逃し防止）
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // 上方向（Y軸）に力を加える
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // ジャンプしたので接地フラグをオフに
        }
    }

    void FixedUpdate()
    {
        float moveH = Input.GetAxisRaw("Horizontal");
        float moveV = Input.GetAxisRaw("Vertical");

        // メインカメラの向きを基準にして移動方向を決める（これでカメラの奥に進むようになる）
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = (cameraForward * moveV + cameraRight * moveH).normalized;

        if (movement.magnitude > 0)
        {
            // 現在のY軸の速度（重力分など）を維持しながら、移動速度を計算する
            rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);

            // 【回転処理】進んでいる方向をスムーズに向く
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
        else
        {
            // 止まっているときも、落下速度（Y軸）だけは維持する
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    // 地面（コライダー）に触れている間はジャンプできる
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    // 地面から離れたらジャンプできない
    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
