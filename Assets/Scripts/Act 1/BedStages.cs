using UnityEngine;

public class BedStages : MonoBehaviour
{
    public bool DoBedSoundReset = false;

    [SerializeField] private BedMonsterAI AI;

    private int level;

    private bool playedSound1 = false;
    private bool playedSound2 = false;
    private bool playedSound3 = false;
    private bool playedSound4 = false;

    private void Update()
    {
        TryResetSounds();

        GetLevel();

        DoWarning();
    }
    private void TryResetSounds()
    {
        if(DoBedSoundReset)
        {
            playedSound1 = false;
            playedSound2 = false;
            playedSound3 = false;
            playedSound4 = false;

            DoBedSoundReset = false;
        }
    }
    private void GetLevel()
    {
        if(AI.BedProgress >= 175f)
        {
            level = 4;
            return;
        }
        if (AI.BedProgress >= 125f)
        {
            level = 3;
            return;
        }
        if (AI.BedProgress >= 55f)
        {
            level = 2;
            return;
        }
        if (AI.BedProgress >= 25f)
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
                    AudioClip clip = AudioSystem.GetSound("Crunch1", AudioType.Monsters);

                    AudioSystem.PlaySoundOnce("BedMonster", AudioType.Monsters, clip);
                }
                break;
            case 2:
                if (!playedSound2)
                {
                    AudioClip clip = AudioSystem.GetSound("Crunch2", AudioType.Monsters);

                    AudioSystem.PlaySoundOnce("BedMonster", AudioType.Monsters, clip);
                }
                break;
            case 3:
                if (!playedSound3)
                {
                    AudioClip clip = AudioSystem.GetSound("Crunch3", AudioType.Monsters);

                    AudioSystem.PlaySoundOnce("BedMonster", AudioType.Monsters, clip);
                }
                break;

            case 4:
                if (!playedSound4)
                {
                    AudioClip clip = AudioSystem.GetSound("Crunch4", AudioType.Monsters);

                    AudioSystem.PlaySoundOnce("BedMonster", AudioType.Monsters, clip);
                }
                break;


        }
    }
}
