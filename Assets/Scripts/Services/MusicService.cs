using FMODUnity;
using UnityEngine;

public class MusicService : MonoBehaviour
{
    static public MusicService Instance;

    [SerializeField]
    private StudioEventEmitter _studioEventEmitter;

    [SerializeField]
    private StudioGlobalParameterTrigger _bassTrigger;

    [SerializeField]
    private StudioGlobalParameterTrigger _drumsTrigger;

    [SerializeField]
    private StudioGlobalParameterTrigger _guitarTrigger;

    [SerializeField]
    private StudioGlobalParameterTrigger _saxTrigger;

    [SerializeField]
    private StudioGlobalParameterTrigger _stringsTrigger;

    private const float onValue = 1.0f;
    private const float offValue = 0.0f;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            DestroyImmediate(gameObject);
        }
    }

    public enum Instruments
    {
        Unknown,
        Bass,
        Drums,
        Guitar,
        Sax,
        Strings
    }

    public void TriggerInstrument (Instruments instrumentType, bool addToTrack)
    {
        StudioGlobalParameterTrigger fmodTrigger = null;

        switch (instrumentType)
        {
            case Instruments.Bass:
                fmodTrigger = _bassTrigger;
                break;
            case Instruments.Drums:
                fmodTrigger = _drumsTrigger; 
                break;
            case Instruments.Guitar:
                fmodTrigger = _guitarTrigger;
                break;
            case Instruments.Sax:
                fmodTrigger = _saxTrigger;
                break;
            case Instruments.Strings:
                fmodTrigger = _stringsTrigger;
                break;
        }

        if (fmodTrigger != null)
        {
            TriggerFMOD(fmodTrigger, addToTrack);
        } else
        {
            Debug.LogError("Tried to trigger Unrecognized Intrument Type");
        }
    }

    public void TriggerFMOD(StudioGlobalParameterTrigger fmodTrigger, bool addToTrack)
    {
        fmodTrigger.value = addToTrack ? onValue : offValue;
        fmodTrigger.TriggerParameters();
    }



}
