using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PongBall : MonoBehaviour
{
    public float speed; // Vitesse de la balle
    public Vector3 direction; // Direction de la balle
    public TextMeshProUGUI scoreText; // Texte afficher score

    private float zMaxDistance = 15f; // Distance maximal de la balle à parcourir (si elle depasse, un point)
    private int scorePlayer = 0; // Score du jouer
    private int scoreComputer = 0; // Score du computer

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        SetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // IA gagne
        if (transform.position.z < -zMaxDistance && direction.z < 0)
        {
            scoreComputer++;
            SetDirection();
        }

        // Joueur gagne
        if (transform.position.z > zMaxDistance && direction.z > 0)
        {
            scorePlayer++;
            SetDirection();
        }
    }

    public void SetDirection()
    {
        scoreText.text = scorePlayer.ToString() + " - " + scoreComputer.ToString();
        transform.position = new Vector3(0, .5f, 0);
        direction = new Vector3(Random.Range(0.75f, 1.75f), 0, -1).normalized;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bar")
        {
            bool isPlayer = collision.gameObject.GetComponent<PongBar>().isHumanPlayer;
            if ((isPlayer && direction.z < 0) || (!isPlayer && direction.z > 0))
            {
                direction.z *= -1;
            }

            if (!isPlayer)
            {
                collision.gameObject.GetComponent<PongAi>().AddBounce();
            }
        }

        if (collision.gameObject.tag == "Side")
        {
            direction.x *= -1;
        }
    }
}
