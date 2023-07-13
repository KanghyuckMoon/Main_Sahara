using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Talk
{

    public class TalkCheckEditor : EditorWindow
    {
        [MenuItem("MoonTool/TalkCheckEditor")]
        static void Open ()
        {
            if (exampleWindow == null)
            {
                exampleWindow = CreateInstance<TalkCheckEditor> ();
            }

            exampleWindow.Show();
        }
        
        
        void OnGUI ()
        {
            
            
        }
        
    }
    
    
}

