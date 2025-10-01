using UnityEngine;

[CreateAssetMenu(menuName = "Project/Skill", fileName = "NewSkill")]
public class SkillSO : ScriptableObject
{
    public string id;
    public string skillName;
    [TextArea] public string description;
    public int pointDelta;
    public Sprite icon;

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id))
            id = skillName;
    }
}
