using UnityEngine;

public class CoinController : MonoBehaviour
{
   public AudioSource coinSound;
    void Awake()
    {
        coinSound = coinSound.GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.GMinstance.CoinIncrease();
            if (coinSound != null)
            {

                coinSound.Play();
            }

            coinSound.transform.parent = null;
            Destroy(this.gameObject);
        }
    }
}
