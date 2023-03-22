using UnityEngine;

public class DoorStages : MonoBehaviour
{
    public bool DoResetDoorSounds = false;

    [SerializeField] private DoorMonsterAI AI;

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
        if (AI.Progress >= AI.ProgressLimit * 0.975f)
        {
            level = 3;
            return;
        }
        if (AI.Progress >= AI.ProgressLimit * 0.45f)
        {
            level = 2;
            return;
        }
        if (AI.Progress >= AI.ProgressLimit * 0.2f)
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

                    AudioSystem.PlaySoundOnce("Knock1", AudioType.Monsters, clip);

                    playedSound1 = true;
                }
                break;
            case 2:
                if (!playedSound2)
                {
                    AudioClip clip = AudioSystem.GetSound("Knock2", AudioType.Monsters);

                    AudioSystem.PlaySoundOnce("Knock2", AudioType.Monsters, clip);
                    playedSound2 = true;
                }
                break;
            case 3:
                if (!playedSound3)
                {
                    AudioClip clip = AudioSystem.GetSound("Knock3", AudioType.Monsters);

                    AudioSystem.PlaySoundOnce("Knock3", AudioType.Monsters, clip);
                    playedSound3 = true;
                }
                break;
        }
    }
}
