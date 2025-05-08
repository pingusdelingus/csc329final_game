using System.Collections;
using UnityEngine;

public class InversionController : MonoBehaviour
{
    public MovementRecorder playerRecorder;
    public MovementRecorder aiRecorder;

    public GameObject ghostPlayerPrefab;
    public GameObject ghostAIPrefab;

    public Transform player;
    public Transform ai;

    public Transform startPoint;
    public Transform finishPoint;


    private bool hasInverted = false;

    public IEnumerator TriggerInversion()
    {
        if (hasInverted) yield break;
        hasInverted = true;

        // Disable finish line inversion, enable start line for second phase

        // Stop forward recording
        playerRecorder.isRecording = false;
        aiRecorder.isRecording = false;

        // Spawn ghosts
        Vector3 offset = new Vector3(5f, 2f, 0f);
        var ghostP = Instantiate(ghostPlayerPrefab, startPoint.position + offset, Quaternion.identity);
        var ghostA = Instantiate(ghostAIPrefab, startPoint.position - offset, Quaternion.identity);

        var ghostPRec = ghostP.GetComponent<MovementRecorder>();
        var ghostARec = ghostA.GetComponent<MovementRecorder>();
         if (ghostPRec == null || ghostARec == null)
    {
        Debug.LogError("One of the ghost prefabs is missing MovementRecorder!");
        yield break;
    }

        ghostPRec.LoadPath(playerRecorder);
        ghostARec.LoadPath(aiRecorder);

        ghostPRec.StartReplay(false);
        ghostARec.StartReplay(false);

        // Move live Player/AI to finish
        player.position = finishPoint.position + new Vector3(-7f, 4f, 0f);

        ai.position = finishPoint.position + new Vector3(2f, 4f, 0f);

        // Reassign AI destination to Start
        var aiController = ai.GetComponent<EnemyController>();
        if (aiController != null)
            aiController.finishPoint = startPoint;

        // Start reverse playback
        playerRecorder.StartReplay(true);
        aiRecorder.StartReplay(true);

        yield return null;
    }
}