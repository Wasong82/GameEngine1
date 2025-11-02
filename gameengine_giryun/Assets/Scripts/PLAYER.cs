using UnityEngine;

public class PLAYER : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5.0f;

    [Header("점프 설정")]
    public float jumpForce = 10.0f;

    private Animator animator; // Animator 컴포넌트 가져오기
    private Rigidbody2D rb; // 캐릭터 물리(중력, 속도)
    private bool isGrounded = false; // 캐릭터가 땅 위에 서있는가?
    private int score = 0;  // 코인 먹으면 점수증가

    // 리스폰용 시작 위치
    private Vector3 startPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 게임 시작 시 위치를 저장
        startPosition = transform.position;
        Debug.Log("시작 위치 저장: " + startPosition);
 
        // 게임 시작 시 애니메이터 컴포넌트 찾아서 저장
        animator = GetComponent<Animator>();
        
        // 디버그: 제대로 찾았는지 확인
        if (animator != null)
        {
            Debug.Log("Animator 컴포넌트를 찾았습니다!");
        }
        else
        {
            Debug.LogError("Animator 컴포넌트가 없습니다!");
        }

    }

    void Update()
    {
        // 이동 벡터 계산 -> 애니메이션 적용
        float moveX = Input.GetAxisRaw("Horizontal"); // A,D 입력

        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // 방향 반전
        if (moveX != 0)
        {
            GetComponent<SpriteRenderer>().flipX = moveX < 0;
        }

        // 애니메이터에 속도 전달 (절댓값으로)
        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveX));
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    // 바닥 충돌 감지 (Collision)- 태그 Ground에 닿아있어야만 점프가능
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // 장애물 충돌 감지 - 태그 Obstacle에 닿아있으면 충돌판정
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("⚠️ 장애물 충돌! 시작 지점으로 돌아갑니다.");

            // 시작 위치로 순간이동
            transform.position = startPosition;

            // 속도 초기화 (안날아가게)
            rb.velocity = new Vector2(0, 0);

        }


    }

    //태그 Ground에 닿아있지 않으면 -> 점프 불가
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // 아이템 수집 감지 (Trigger)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            score++;  // 점수 증가
            Debug.Log("코인 획득! 현재 점수: " + score);
            Destroy(other.gameObject);  // 코인 제거
        }
    }
}