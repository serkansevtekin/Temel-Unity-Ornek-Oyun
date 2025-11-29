using System.Collections;
using UnityEngine;

public class ChecpointTrigger : MonoBehaviour
{
    [SerializeField] GameObject checkParticle;
    ParticleSystem ps;
    ParticleSystem.MainModule psMain;
    ParticleSystem.EmissionModule psEmision;

    bool checpoinTaken = false;

    void Start()
    {

        ps = checkParticle.GetComponent<ParticleSystem>();
        psMain = ps.main;
        psEmision = ps.emission;

    }
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (!GameManager.GMinstance.AllCoinCollected()) return;
        if (checpoinTaken) return;


        print("Checkpoint Tetiklendi");

        UIManager.UIinstance.PuzzleGamePanelOpen();
        print("Checkpoint Stack Eklendi");

        //Checpoin meshin rendedrer objesi
        Renderer r = GetComponent<Renderer>();
        if (r != null)
        {
            r.material.color = Color.black;
        }
        psMain.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.yellow);
        psMain.simulationSpeed = 2f;
        psEmision.rateOverTime = 100;



    }
    void OnTriggerExit(Collider other)
    {
        if (GameManager.GMinstance.AllCoinCollected())
        {
            if (checpoinTaken) return;
            checpoinTaken = true;

            psEmision.enabled = false;

            psMain.simulationSpeed = 10f; // Partiküller yavaşlar
            StartCoroutine(DestroyAfterParticlesDie());
        }
    }

    IEnumerator DestroyAfterParticlesDie()
    {

        yield return new WaitForSeconds(psMain.startLifetime.constantMax);

        Destroy(checkParticle);
    }
}
