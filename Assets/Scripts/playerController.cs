using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{

    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    private int count;
    private int livesCount;
    public Text countText;
    public Text winText;
    public Text livesText;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        livesCount = 3;
        winText.text = "";
        setCountText();
    }

    private void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 4)
        {
            winText.text = "You Win!";
        }
    }

    private void setLivesText()
    {
        livesText.text = "Lives: " + livesCount;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        } else
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            setCountText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
        if (collision.collider.tag == "Deadly")
        {
            livesCount = livesCount - 1;
            setLivesText();
            

            if (livesCount == 0)
            {
                winText.text = "You Lose. =(";
                this.gameObject.SetActive(false);
            }
        }
    }

}
