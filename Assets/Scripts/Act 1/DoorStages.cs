using UnityEngine;

public class DoorStages : MonoBehaviour
{
    public bool DoResetDoorSounds = false;

    [SerializeField] private DoorMonsterAI AI;

    [SerializeField] private AudioSourceID DoorSoundID;

    private int level;

    private bool playedSound1 = false;
    private bool playedSound2 = false;
    private bool playedSound3 = false;

    private void Update()
    {
        TryResetSounds();

        GetLevel();

        DoWarning();
    }
    private void TryResetSounds()
    {
        if(DoResetDoorSounds)
        {
            playedSound1 = false;
            playedSound2 = false;
            playedSound3 = false;

            DoResetDoorSounds = false;  
        }
    }    
    private void GetLevel()
    {
        if (AI.DoorProgress >= 85f)
        {
            level = 3;
            return;
        }
        if (AI.DoorProgress >= 45f)
        {
            level = 2;
            return;
        }
        if (AI.DoorProgress >= 20f)
        {
            level = 1;
            return;
        }

        level = 0;
    }
    private void DoWarning()
    {
        switch (level)
        {
            case 1:
                if(!playedSound1)
                {
                    AudioClip clip = AudioSystem.GetSound("Knock1", AudioType.Monsters);

                    AudioSystem.PlaySoundOnce(DoorSoundID.SourceName, DoorSoundID.Type, clip);

                    playedSound1 = true;
                }
                break;
            case 2:
                if (!playedSound2)
                {
                    AudioClip clip = AudioSystem.GetSound("Knock2", AudioType.Monsters);

                    AudioSystem.PlaySoundOnce(DoorSoundID.SourceName, DoorSoundID.Type, clip);

                    playedSound2 = true;
                }
                break;
            case 3:
                if (!playedSound3)
                {
                    AudioClip clip = AudioSystem.GetSound("Knock3", AudioType.Monsters);

                    AudioSystem.PlaySoundOnce(DoorSoundID.SourceName, DoorSoundID.Type, clip);

                    playedSound3 = true;
                }
                break;
        }
    }
}
