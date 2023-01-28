using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public interface IPresenterController
    {
        public void ContructPresenters();
        public void AwakePresenters();
        public void StartPresenters(); 
    }

}
