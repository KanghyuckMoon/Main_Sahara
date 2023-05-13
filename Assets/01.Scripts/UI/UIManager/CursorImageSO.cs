using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utill.SeralizableDictionary;

namespace UI
{
    public enum CursorImageType
    {
        defaultCursor,
        deleteMapMarker,
    }

    [System.Serializable]
    public class CursorImageData
    {
        public CursorImageType cursorImageType;
        public Texture2D image;
    }

    [CreateAssetMenu(menuName = "SO/UI/CurosrImageSO")]
    public class CursorImageSO : ScriptableObject
    {
        public List<CursorImageData> cursorImageList = new List<CursorImageData>();

        public Texture2D GetCursorImage(CursorImageType _type)
        {
            foreach (var _cursorImage in cursorImageList)
            {
                if (_cursorImage.cursorImageType == _type)
                    return _cursorImage.image;
            }
            Debug.LogError("존재하지 않는 커서");
            return null; 
        }
        
    }
}