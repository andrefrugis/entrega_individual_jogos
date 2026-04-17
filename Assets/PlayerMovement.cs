using UnityEngine;

public class PlayerMovement : MonoBehaviour

{
    private Rigidbody2D rb;
    public float speed;
    private AudioSource collectAudio;
    private AudioSource failureAudio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AudioSource[] audioSources = GetComponents<AudioSource>();

        if (audioSources.Length > 0)
        {
            collectAudio = audioSources[0];
        }

        if (audioSources.Length > 1)
        {
            failureAudio = audioSources[1];
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("PoisonCandy"))
        {
            if (failureAudio != null)
            {
                failureAudio.Play();
            }
            else if (collectAudio != null)
            {
                collectAudio.Play();
            }

            GameController.Fail();
            Destroy(other.gameObject);
            return;
        }

        if (other.CompareTag("coletava"))
        {
            if (collectAudio != null)
            {
                collectAudio.Play();
            }

            GameController.Collect();
            Destroy(other.gameObject);
        }
    }
}
