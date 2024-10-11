using UnityEngine;

public class CharacterDestructionHandler3 : MonoBehaviour
{
    private void OnDestroy()
    {
        // Notify the manager that this character is destroyed
        CharacterManager characterManager = FindObjectOfType<CharacterManager>();
        if (characterManager != null)
        {
            characterManager.OnCharacterDestroyed3(); // This method will handle the logic in CharacterManager
        }
    }
}
