using UnityEngine;

public class PongAi : MonoBehaviour
{

    public Transform pongBall;
    public float latency = 4f;
    public float viewDistance = 20;
    public int nbShots = 0;


    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, pongBall.position) < viewDistance)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(pongBall.transform.position.x, .5f, 14),
                latency * Time.deltaTime
                );
        }
    }

    // Cette fonction permet de rendre l'IA moins fort quand il tape 10 fois la fois pour permettre au joueur de le battre
    public void AddBounce()
    {
        nbShots++;
        
        if (nbShots >= 10)
        {
            nbShots = 0;
            latency -= 0.25f;
        }
    }
}
