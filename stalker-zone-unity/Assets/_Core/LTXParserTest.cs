using UnityEngine;

public class LTXParserTest : MonoBehaviour
{
    void Start()
    {
        string fakeWeapon =
            "[wpn_ak74]\n" +
            "damage = 45\n" +
            "fire_rate = 600\n" +
            "magazine_size = 30\n";

        LTXParser parser = new LTXParser();
        parser.Parse(fakeWeapon);

        Debug.Log("Damage: " + parser.GetFloat("wpn_ak74", "damage"));
        Debug.Log("Fire Rate: " + parser.GetFloat("wpn_ak74", "fire_rate"));
        Debug.Log("Magazine: " + parser.GetFloat("wpn_ak74", "magazine_size"));
    }
}