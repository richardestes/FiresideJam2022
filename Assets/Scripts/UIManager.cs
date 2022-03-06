using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Spaceship spaceship;
    public Crosshair crosshair;
    public Text ammoText;
    public Text spaceshipHealthText;

    private void Update()
    {
        spaceshipHealthText.text = spaceship.health.ToString();
        ammoText.text = crosshair.ammo.ToString();
    }
}
