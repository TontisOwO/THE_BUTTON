using System.Numerics;
using UnityEditor.SearchService;

namespace THE_BUTTON.runtime
{
    [System.Serializable]
    public sealed class SaveProfiles<T> where T : SaveProfileData
    {
        public string name;
        public T saveData;
        public int savedScene = 1;

        private SaveProfiles()
        {

        }

        public SaveProfiles(string name, T saveData)
        {
            this.name = name;
            this.saveData = saveData;
        }
    }

    public abstract record SaveProfileData
    {

    }

    public record PlayerSaveData : SaveProfileData
    {
        public Vector2 position;
        
    }
}
