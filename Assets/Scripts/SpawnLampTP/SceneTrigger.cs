using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [Tooltip("Имя сцены для загрузки: ZZRU, inhouse, ingym")]
    public string sceneToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Character")) return;

        var pi = other.GetComponent<PlayerInteraction>();
        if (pi != null)
            pi.EnterZone(sceneToLoad);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Character")) return;

        var pi = other.GetComponent<PlayerInteraction>();
        if (pi != null)
            pi.ExitZone(sceneToLoad);
    }
}
