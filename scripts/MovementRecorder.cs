using System.Collections.Generic;
using UnityEngine;

public class MovementRecorder : MonoBehaviour
{
    public bool isRecording = true;
    public bool isReplaying = false;
    public bool playBackwards = false;

    public float replaySpeed = 0.85f;

    private List<Vector3> recordedPositions = new List<Vector3>();
    private int currentIndex = 0;

    void FixedUpdate()
    {
        if (isRecording)
        {
            recordedPositions.Add(transform.position);
        }
        else if (isReplaying && recordedPositions.Count > 0)
        {
            if (currentIndex >= 0 && currentIndex < recordedPositions.Count)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    recordedPositions[currentIndex],
                    replaySpeed * Time.fixedDeltaTime
                );

                if (Vector3.Distance(transform.position, recordedPositions[currentIndex]) < 0.05f)
                {
                    currentIndex += playBackwards ? -1 : 1;
                }
            }
        }
    }

    public void StartReplay(bool backwards = false)
    {
        isRecording = false;
        isReplaying = true;
        playBackwards = backwards;
        currentIndex = backwards ? recordedPositions.Count - 1 : 0;
    }

    public void StopReplay()
    {
        isReplaying = false;
    }

    public void ResetRecording()
    {
        recordedPositions.Clear();
        currentIndex = 0;
    }
    public void LoadPath(MovementRecorder other)
{
    recordedPositions = new List<Vector3>(other.GetRecordedPositions());
}
public List<Vector3> GetRecordedPositions() => recordedPositions;
}