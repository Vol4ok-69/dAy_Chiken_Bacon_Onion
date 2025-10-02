using UnityEngine;

public class SceneTrigger2D : MonoBehaviour
{
    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("���� � �������: " + other.name);

        if (other.CompareTag("Character"))
        {
            var player = other.GetComponent<PlayerInteraction>();
            if (player != null)
                player.EnterZone(sceneToLoad);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("����� �� ��������: " + other.name);

        if (other.CompareTag("Character"))
        {
            var player = other.GetComponent<PlayerInteraction>();
            if (player != null)
                player.ExitZone(sceneToLoad);
        }
    }
}
