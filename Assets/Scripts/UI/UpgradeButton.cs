using System.Collections;
using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    private PlayerStats playerStats;

    private void Awake()
    { 
        GetComponent<Button>().onClick.AddListener(OnClick);
        StartCoroutine(init());
    }

    private void OnClick()
    {
        playerStats.upgrade();
    }

    private IEnumerator init()
    {
        //bad solution!
        yield return new WaitForEndOfFrame(); //needed to wait for the player gameobject to load

        Player data = World.Default!.Filter.With<Player>().First().GetComponent<Player>();
        if (data.playerObject)
            playerStats = data.playerObject.GetComponent<PlayerStats>();
    }
    
}
