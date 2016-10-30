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
        // 00:00:00,000 --> 00:00:00,000 sfv format for subtitles
        Regex rgx = new Regex(@"(\d{2}:\d{2}:\d{2},\d{3}) --> (\d{2}:\d{2}:\d{2},\d{3})");
        AudioSource audioSource = audioObject.GetComponent<AudioSource>();

        bool found_duration = false;
        DateTime startTime, endTime;
        int current_index = 0;

        // Parse the SFV subtitle file assuming wellformed
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

                // Let DateTime do the heavy lifting of parsing since we will need to do some math
                startTime = DateTime.ParseExact(start, "hh:mm:ss,fff", null);

                Subtitle sub = new Subtitle();

                sub.startTime = (3600 * startTime.Hour) + (60 * startTime.Minute) + startTime.Second + (0.001 * startTime.Millisecond);
                endTime = DateTime.ParseExact(end, "hh:mm:ss,fff", null);
                sub.duration = (endTime - startTime).TotalSeconds;

                subtitles.Add(sub);
            }
        }

        audioSource.Play(); // start playing once we are done parsing the file
        StartCoroutine("DisplaySubtitles");
    }

    private IEnumerator DisplaySubtitles() {
        double deltaStart;

        for(int i = 0; i < subtitles.Count; i++) {
            deltaStart = subtitles[i].startTime;
            if (i > 0) {
                // We want to start playing at the delta between the the subtitles
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