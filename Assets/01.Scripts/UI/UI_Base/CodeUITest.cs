using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class CodeUITest : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<CodeUITest,CodeUITestUxmlTraits> { }

        public class CodeUITestUxmlTraits : UxmlTraits
        {
            UxmlColorAttributeDescription leftColor = new UxmlColorAttributeDescription { name = "left-color", defaultValue = Color.red };
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
            }
        }

    }

}

