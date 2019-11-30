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

    public GameObject goScore, goGM;

    [Header("遊戲管理器")]
    public GameManager gm;

    [Header("音效")]
    public AudioSource aud;
    public AudioClip soundJump, soundHit, soundAdd;



    /// <summary>
    /// 小雞跳躍方法。
    /// </summary>
    private void Jump()
    {
        // 原本寫法
        // if (dead == true)
        // {
        //     return;
        // }
        
        // 如果 死亡 跳出此程式區塊
        // 判斷式只有一行敘述可以省略大括號
        if (dead) return;    // 簡寫

        // 如果 玩家 按下 左鍵
        // 輸入.按下按鍵(案件列舉.滑鼠左鍵) (手機觸控)

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            aud.PlayOneShot(soundJump, 1.5f);    // 喇叭.播放一次音效(音效,音量)
            r2d.Sleep();                         // 小雞剛體.睡著(); - 重設剛體所有資訊
            r2d.gravityScale = 1;                // 小雞剛體.重力 = 1;
            r2d.AddForce(new Vector2(0, jump));  // 小雞剛體.增加推力(二維向量(左右,上下));
            //分數 顯示
            //GM 顯示
            goScore.SetActive(true);
            goGM.SetActive(true);


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
        print("你死了");
        dead = true;
        gm.GameOver();
        aud.PlayOneShot(soundHit, 1.5f);
    }

    // 固定幀數 0.002 一幀:要控制物理請寫在此事件內
    private void Update()
    {
        Jump();
    }

    // 事件:碰撞開始 - 碰撞開始時執行一次 (Collision2D col) (碰撞類型 名稱) 存放碰到物件的資訊
    private void OnCollisionEnter2D(Collision2D col)
    {
        // 碰到物件.遊戲物件.名稱
        print(col.gameObject.name);

        if(col.gameObject.name == "地板")
        {
            Dead();
        }
    }

    // 事件:處發開始 - 物件必須勾選 IsTrigger
    private void OnTriggerEnter2D(Collider2D col)
    {
        // 如果 碰到. 物件名稱 為 上 或者 下 - 死亡
        if (col.gameObject.name == "水管 - 上" || col.gameObject.name == "水管 - 下")
        {
            Dead();
        }
    }

    // 事件:處發離開 - 物件必須勾選 IsTrigger
    private void OnTriggerExit2D(Collider2D col)
    {
        // dead == true 簡寫 dead
        // dead != true 簡寫 !dead
        if (col.name == "加分" && dead !=true)
        {
            aud.PlayOneShot(soundAdd, 1.5f);
            gm.AddScore();
        }
    }
}
