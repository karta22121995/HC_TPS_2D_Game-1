using UnityEngine;

public class Bird : MonoBehaviour
{
    [Header("跳躍高度"), Range(10, 2000)]
    public int jump = 100;
    [Header("旋轉角度"), Range(0, 100)]
    public float angle = 10;
    [Header("是否死亡"), Tooltip("用來判斷角色是否死亡，true 死亡，false 還沒死亡")]
    public bool dead;
    [Header("剛體")]
    public Rigidbody2D r2d;

    /// <summary>
    /// 小雞跳躍方法。
    /// </summary>
    private void Jump()
    {
        // 如果 玩家 按下 左鍵
        // 輸入.按下按鍵(案件列舉.滑鼠左鍵) (手機觸控)

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            print("玩家按下左鍵");
            r2d.Sleep();                         // 小雞剛體.睡著(); - 重設剛體所有資訊
            r2d.gravityScale = 1;                // 小雞剛體.重力 = 1;
            r2d.AddForce(new Vector2(0, jump));  // 小雞剛體.增加推力(二維向量(左右,上下));            
        }
        // Rigidbody2D.SetRotation(float) 設定角度(角度)
        // Rigidbody2D.velocity (二維向量 x, y)
        r2d.SetRotation(angle * r2d.velocity.y);

    }

    /// <summary>
    /// 小雞死亡方法。
    /// </summary>
    private void Dead()
    {

    }

    private void Update()
    // 固定幀數 0.002 一幀:要控制物理請寫在此事件內
    {
        Jump();
    }
}
