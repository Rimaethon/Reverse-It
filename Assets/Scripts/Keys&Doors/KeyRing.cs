using System.Collections.Generic;

namespace Keys_Doors
{
    public static class KeyRing
    {
        private static readonly HashSet<int> KeyIDs = new HashSet<int>() { 0 };
        // The IDs of the keys held by the player

        public static void AddKey(int keyID)
        {
            KeyIDs.Add(keyID);
        }

        
        public static bool HasKey(Door door)
        {
            return KeyIDs.Contains(door.doorID);
        }

   
        public static void ClearKeyRing()
        {
            KeyIDs.Clear();
        }
    }
}
