using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInTitle : MonoBehaviour
{

    public TMP_Text _text;
    public float FadeInPerCharacterSeconds;
    public float DelayBetweenCharactersSeconds;
    public float DelayedStartSeconds;
    public float ButtonFadeInDelaySeconds;
    public FadeInButton startButtonToFadeIn;
    private float _startTime;
    private TMP_TextInfo _textInfo;
    private Color _color;
    private int _visibleCharacters;
    private Color32[] _newVertexColors;

    void Awake()
    {
        _color = _text.color;
        _color.a = 0.0f;
        _text.color = _color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _textInfo = _text.textInfo;
        StartCoroutine(FadeInText());
    }


    private IEnumerator FadeInText(){
        _text.ForceMeshUpdate();

        yield return new WaitForSeconds(DelayedStartSeconds);

        for(var character = 0; character < _textInfo.characterCount; character++){
            if(!_textInfo.characterInfo[character].isVisible){
                continue;
            }
            var meshIndex = _textInfo.characterInfo[character].materialReferenceIndex;
            _newVertexColors = _textInfo.meshInfo[meshIndex].colors32;
            var vertexIndex = _textInfo.characterInfo[character].vertexIndex;
            yield return StartCoroutine(FadeInLetter(vertexIndex));

        }

        yield return new WaitForSeconds(ButtonFadeInDelaySeconds);

        startButtonToFadeIn.StartFadingIn();
    }

    private IEnumerator FadeInLetter(int vertexIndex) {
        var startTime = Time.time;
        var endTime = startTime + FadeInPerCharacterSeconds;

        while(Time.time < endTime){
            var elapsedTime = Time.time - startTime;
            var newAlpha = (byte)Mathf.Lerp(0, 255, elapsedTime/FadeInPerCharacterSeconds);
            _newVertexColors[vertexIndex].a = newAlpha;
            _newVertexColors[vertexIndex+1].a = newAlpha;
            _newVertexColors[vertexIndex+2].a = newAlpha;
            _newVertexColors[vertexIndex+3].a = newAlpha;


            _text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
            yield return new WaitForFixedUpdate();

        }

        yield return new WaitForSeconds(DelayBetweenCharactersSeconds);

    } 
}
