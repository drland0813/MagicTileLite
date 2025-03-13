using System;
using System.Collections.Generic;
using System.IO;
using Drland.MagicTileLite;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class BeatMapEditor : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Button _addNoteButton;
    [SerializeField] private TMP_Dropdown _noteTypeDropdown;
    [SerializeField] private TextMeshProUGUI _timestampText;
    [SerializeField] private TextMeshProUGUI _timestampEndText;
    [SerializeField] private TextMeshProUGUI _pauseText;
    private float _seekStep = 1.0f;
    private SpawnType _noteType;
    private string _songName;
    
    private List<Note> _notes = new();
    private float _songLength;
    private bool _isDragging;

    void Start()
    {
        if (_audioSource.clip != null)
        {
            _songName = _audioSource.clip.name;
            _songLength = _audioSource.clip.length;
            _timeSlider.maxValue = _songLength;
        }
        
        _addNoteButton.onClick.AddListener(AddNote);
        _timestampEndText.text = FormatTime(_timeSlider.maxValue);
        
        _pauseText.text = "Is Playing";
    }

    void Update()
    {
        if (_audioSource.isPlaying && !_isDragging)
        {
            _timeSlider.value = _audioSource.time;
            _timestampText.text = FormatTime(_timeSlider.value);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayPause();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            SeekTime(_seekStep);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            SeekTime(-_seekStep);
        }
    }
    

    public void UpdateNoteType(int type)
    {
        _noteType = (SpawnType)type;
    }

    public void PlayPause()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Pause();
            _pauseText.text = "Is Pausing";
        }
        else
        {
            _audioSource.Play();
            _pauseText.text = "Is Playing";
        }
    }
    
    public void AddNote()
    {
        int selectedType = (int)_noteType;
        float roundedTime = (float)System.Math.Round(_timeSlider.value, 2, System.MidpointRounding.AwayFromZero);
        
        _notes.Add(new Note { SpawnType = selectedType, SpawnTime = roundedTime });
    }


    
    public void SaveToJson()
    {
        BeatMap beatMap = new BeatMap { Notes = _notes };
        Song song = new Song { ID = 1, Name = _songName, BeatMap = beatMap };
        string json = JsonUtility.ToJson(song, true);
        File.WriteAllText(Application.persistentDataPath + $"/{_songName}.json", json);
        Debug.Log("Saved to: " + Application.persistentDataPath + $"/{_songName}.json");
    }
    public void OnSliderDragStart()
    {
        _isDragging = true;
    }
    public void OnSliderDragEnd()
    {
        _audioSource.time = _timeSlider.value;
        _isDragging = false;
    }
    
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }
    
    private void SeekTime(float step)
    {
        float newTime = Mathf.Clamp(_audioSource.time + step, 0, _songLength);
        _audioSource.time = newTime;
        _timeSlider.value = newTime;
    }

    private void OnApplicationQuit()
    {
        SaveToJson();
    }
}
