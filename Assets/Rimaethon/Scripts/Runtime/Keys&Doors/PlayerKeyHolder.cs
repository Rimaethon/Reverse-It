using System.Collections.Generic;
using UnityEngine;

namespace Rimaethon.Runtime.Keys_Doors
{
    public static class PlayerKeyHolder
    {
        private static readonly HashSet<int> keyIDs = new HashSet<int>();

        public static void AddKey(int keyID)
        {
            keyIDs.Add(keyID);
        }


        public static bool HasKey(int keyID)
        {
            return keyIDs.Contains(keyID) || keyID == -1;
        }


        public static void ClearKeyRing()
        {
            keyIDs.Clear();
        }
    }
}
