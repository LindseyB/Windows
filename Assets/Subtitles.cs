using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;

public class Subtitles : MonoBehaviour {
    public TextAsset subtitles_file;
    public GameObject audioObject;

    private List<Subtitle> subtitles;

    void Start () {
        subtitles = new List<Subtitle>();
        Regex rgx = new Regex(@"(\d{2}:\d{2}:\d{2},\d{3}) --> (\d{2}:\d{2}:\d{2},\d{3})");
        AudioSource audioSource = audioObject.GetComponent<AudioSource>();

        bool found_duration = false;
        DateTime startTime, endTime;
        int current_index = 0;

        // Parse the subtitle file assuming wellformed
        foreach (string line in subtitles_file.text.Split('\n')) {

            if (found_duration) {
                // This line is the text
                subtitles[current_index].line = line;
                current_index++;
                found_duration = false;
            }

            if (rgx.IsMatch(line)) {
                found_duration = true;
                MatchCollection matches = rgx.Matches(line);
                string start = matches[0].Groups[1].Value;
                string end = matches[0].Groups[2].Value;

                // Probably faster to do this with a regex, maybe if I end up using duration it's not
                startTime = DateTime.ParseExact(start, "hh:mm:ss,fff", null);

                Subtitle sub = new Subtitle();

                sub.startTime = (3600 * startTime.Hour) + (60 * startTime.Minute) + startTime.Second + (0.001 * startTime.Millisecond);
                endTime = DateTime.ParseExact(end, "hh:mm:ss,fff", null);
                sub.duration = (endTime - startTime).TotalSeconds;

                subtitles.Add(sub);
            }
        }

        audioSource.Play();
        StartCoroutine("DisplaySubtitles");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator DisplaySubtitles() {
        double deltaStart;

        for(int i = 0; i < subtitles.Count; i++) {
            deltaStart = subtitles[i].startTime;
            if (i > 0) {
                deltaStart = subtitles[i].startTime - (subtitles[i - 1].startTime + subtitles[i - 1].duration);
            }

            yield return new WaitForSeconds((float)deltaStart);
            GetComponent<Animator>().Play("FadeIn");
            GetComponent<UnityEngine.UI.Text>().text = subtitles[i].line;
            yield return new WaitForSeconds((float)subtitles[i].duration);
            GetComponent<Animator>().Play("FadeOut");
        }
    }
}


public class Subtitle {
    public string line;
    public double startTime;
    public double duration;  
}