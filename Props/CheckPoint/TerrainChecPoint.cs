using UnityEngine;

public class TerrainChecPoint : MonoBehaviour
{
    private bool isTaken = false;

    public int checpointIndex;

    void OnTriggerEnter(Collider other)
    {
        if (isTaken) return;
        if (!other.CompareTag("Player")) return;

        if (GameManager.GMinstance.AllCoinCollected() && UIManager.UIinstance.puzzleCorrectCount >= 4)
        {

            if (GameManager.GMinstance.LastActivatedChecpointIndex() == checpointIndex - 1 || GameManager.GMinstance.LastActivatedChecpointIndex() == -1)
            {
                isTaken = true;
                GameManager.GMinstance.AddActivatedChecpoint(this.gameObject);

                //Oyun kaydediliyor
                GameManager.GMinstance.SaveGame();

                print("Checkpoint stacke eklendi ve kaydedildi: " + checpointIndex);
            }
            else
            {
                print("Önceki checkpoint alınmamış: " + checpointIndex);
            }

        }
    }


}
