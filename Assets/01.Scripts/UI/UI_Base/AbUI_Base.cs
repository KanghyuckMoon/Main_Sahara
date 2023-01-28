using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq; 

namespace UI    
{
    [Serializable]
    public abstract class AbUI_Base
    {
        [SerializeField]
        protected UIDocument uiDocument; // UIDocument 
        protected VisualElement rootElement;

        [SerializeField]
        protected string parentElementName; // 자기 자신 이름 
        protected VisualElement parentElement; // 자기 자신 

        protected Dictionary<string, VisualElement> elementsDic = new Dictionary<string, VisualElement>(); // 모든 자식 요소들 
        protected Dictionary<Type, VisualElement[]> elements = new Dictionary<Type, VisualElement[]>(); // 모든 자식 요소들 

        /// <summary>
        /// Element 캐싱 
        /// </summary>
        public virtual void Cashing()
        {
            if(uiDocument == null /*|| String.IsNullOrEmpty(_parentElementName)*/)
            {
                Debug.LogError("NULL에러 : 인스펙터 확인");
            }
            rootElement = uiDocument.rootVisualElement;
            parentElement = rootElement.Q<VisualElement>(parentElementName);
        }
        public void InitUIDocument(UIDocument uiDoc)
        {
            this.uiDocument = uiDoc; 
        }
        public void InitUIParent(VisualElement v)
        {
            this.parentElement = v;
        }
        /// <summary>
        /// 초기화 
        /// </summary>
        public virtual void Init() { }

        public VisualElement GetElementByName(string _name)
        {
            return new VisualElement(); 
        }

        /// <summary>
        /// 클래스 이름으로 element 가져오기 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public VisualElement GetElementClassName(string _className)
        {
            return parentElement.Q(className: _className);
        }
        
        /// <summary>
        /// 이름으로 element 가져오고 캐싱하기 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public VisualElement GetElement(string name)
        {
            // 이미 캐싱되어 있으면 딕셔너리에서 가져오기
            //VisualElement element = _elementsDic.Where(e => e.Key == name).Select(e => e.Value).First();
            //if(element == null) // 없다면 찾아오기 
            //{
            //    element = _parentElement.Q(name);
            //    _elementsDic.Add(name, element);
            //}
            //return element; 
        
            return parentElement.Q(name: name);
        }



        /// <summary>
        /// element show or hide
        /// </summary>
        /// <param name="_visualElement"></param>
        /// <param name="_state"></param>
        public static void ShowVisualElement(VisualElement _visualElement, bool _state)
        {
            if (_visualElement == null)
                return;
            _visualElement.style.display = (_state) ? DisplayStyle.Flex : DisplayStyle.None;
        }

        /// <summary>
        /// 현재 활성화되어 있는가 ( element가 null이면 자신 ) 
        /// </summary>
        /// <returns></returns>
        public bool IsVisible(VisualElement _element = null)
        {
            VisualElement e = _element != null ? _element : parentElement; 
            return e.style.display == DisplayStyle.Flex; 
        }

        /// <summary>
        /// 현재 스크린이 활성화면 비활성화 , 비활성화면 활성화 
        /// </summary>
        public void ActiveScreen()
        {
            ShowVisualElement(parentElement, !IsVisible());
        }

        /// <summary>
        /// elements 바인딩 하기 (enum으로 쭉 쓰고 넣어줘) 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_type"></param>
        public void Bind<T>(Type _type) where T : VisualElement
        {
            string[] names = Enum.GetNames(_type);
            VisualElement[] elements = new VisualElement[names.Length];
            this.elements.Add(typeof(T), elements); 

            for(int i =0;i < names.Length; i++)
            {
//                if(typeof(T) == typeof(VisualElement))
//                {
                    elements[i] = GetElement(names[i]);
//                }
                if (elements[i] == null) // names[i] 의 이름의 요소가 없으면 에러 
                    Debug.LogError($"지정된 이름의 요소가 없습니다");
            }
        }

        public void BindVisualElements(Type type)
        {
            Bind<VisualElement>(type); 
        }
        public void BindButtons(Type type)
        {
            Bind<Button>(type);
        }
        public void BindProgressBars(Type type)
        {
            Bind<ProgressBar>(type);
        }
        public void BindRadioButtons(Type type)
        {
            Bind<RadioButton>(type); 
        }

        /// <summary>
        /// element 가져오기 (idx는 enum을 통해 ) 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_idx"></param>
        /// <returns></returns>
        public T Get<T>(int _idx) where T : VisualElement
        {
            VisualElement[] elements;
            if (this.elements.TryGetValue(typeof(T), out elements) == false)
                return null;
            return elements[_idx] as T;
        }


        public VisualElement GetVisualElement(int _idx)
        {
            return Get<VisualElement>(_idx); 
        }
        public Button GetButton(int _idx)
        {
            return Get<Button>(_idx);
        }
        public ProgressBar GetProgressBar(int _idx)
        {
            return Get<ProgressBar>(_idx);
        }
        public RadioButton GetRadioButton(int _idx)
        {
            return Get<RadioButton>(_idx); 
        }
        // == 이벤트 관련 == //

        //public void AddClickEvent(int _idx,Action _event)
        //{
        //    Get<VisualElement>(_idx).RegisterCallback<ClickEvent>((e) => _event?.Invoke());

        //    AddButtonEvent<ClickEvent>(1,()=> { });
        //}

        // == 이부분 수정? 

        /// <summary>
        /// 버튼에 이벤트 추가 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        public void AddButtonEvent<T>(int _idx, Action _event) where T : EventBase<T>, new() 
        {
            // Get<타입> 
            Get<Button>(_idx).RegisterCallback<T>((e) => _event?.Invoke());
        }
    
        /// <summary>
        /// VisualElement에 이벤트 추가 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        public void AddVisualElementEvent<T>(int _idx, Action _event) where T : EventBase<T>, new()
        {
            // Get<타입> 
            Get<VisualElement>(_idx).RegisterCallback<T>((e) => _event?.Invoke());
        }

        //== 값이 변경될 때 이벤트 == 

        /// <summary>
        /// RadioButton 이벤트
        /// </summary>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        public void AddRadioBtnChangedEvent(int _idx, Action<bool> _event)
        {
            Get<RadioButton>(_idx).RegisterValueChangedCallback((e) => _event?.Invoke(e.newValue)); 
        }

        /// <summary>
        /// 텍스트필드 이벤트
        /// </summary>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        public void AddTextFieldChangedEvent(int _idx, Action<string> _event)
        {
            Get<TextField>(_idx).RegisterValueChangedCallback((e) => _event?.Invoke(e.newValue));
        }
    }
}



