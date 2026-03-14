using System.Collections.Generic;
using UnityEngine;

public class LTXParser
{
    private Dictionary<string, Dictionary<string, string>> _sections
        = new Dictionary<string, Dictionary<string, string>>();

    public void Parse(string ltxText)
    {
        string currentSection = null;

        foreach (string rawLine in ltxText.Split('\n'))
        {
            string line = rawLine.Trim();

            if (line.StartsWith(";") || line.Length == 0) continue;

            if (line.StartsWith("["))
            {
                int closeBracket = line.IndexOf(']');
                currentSection = line.Substring(1, closeBracket - 1);
                _sections[currentSection] = new Dictionary<string, string>();
            }
            else if (currentSection != null && line.Contains('='))
            {
                int eqIdx = line.IndexOf('=');
                string key = line.Substring(0, eqIdx).Trim();
                string val = line.Substring(eqIdx + 1).Trim();
                _sections[currentSection][key] = val;
            }
        }
    }

    public string Get(string section, string key, string defaultVal = "")
    {
        if (_sections.TryGetValue(section, out var s))
            if (s.TryGetValue(key, out var v)) return v;
        return defaultVal;
    }

    public float GetFloat(string section, string key, float def = 0f)
    {
        var v = Get(section, key);
        return float.TryParse(v, out float result) ? result : def;
    }
}