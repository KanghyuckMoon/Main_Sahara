using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using DG.Tweening; 

namespace UI    
{
    public enum PosType
    {
        left, 
        mid,
        right
    }
    [Serializable]
    public abstract class AbUI_Base
    {
        protected UIDocument uiDocument; // UIDocument 
        protected VisualElement rootElement;

        [SerializeField]
        protected string parentElementName; // 자기 자신 이름 
        protected VisualElement parentElement; // 자기 자신 

        protected Dictionary<Type, List<VisualElement>> elementsDic = new Dictionary<Type, List<VisualElement>>(); // 모든 자식 요소들 

        // 프로퍼티 
        public VisualElement ParentElement => parentElement;

        /// <summary>
        /// parentElement 캐싱 
        /// </summary>
        public virtual void Cashing()
        {
            if (uiDocument == null /*|| String.IsNullOrEmpty(_parentElementName)*/)
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
        public virtual void Init()
        {
            //parentElement.RegisterCallback<TransitionEndEvent>((x) => EndScreenTransition(x));
        }

        protected VisualElement GetElementByName(string _name)
        {
            return new VisualElement();
        }

        /// <summary>
        /// 클래스 이름으로 element 가져오기 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        protected VisualElement GetElementClassName(string _className)
        {
            return parentElement.Q(className: _className);
        }

        /// <summary>
        /// 이름으로 element 가져오고 캐싱하기 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected VisualElement GetElement(string name)
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
        protected static void ShowVisualElement(VisualElement _visualElement, bool _state)
        {
            if (_visualElement == null)
                return;
            _visualElement.style.display = (_state) ? DisplayStyle.Flex : DisplayStyle.None;
        }

        /// <summary>
        /// 현재 활성화되어 있는가 ( element가 null이면 자신 ) 
        /// </summary>
        /// <returns></returns>
        protected bool IsVisible(VisualElement _element = null)
        {
            VisualElement e = _element != null ? _element : parentElement;
            return e.style.display == DisplayStyle.Flex;
        }

        /// <summary>
        /// 현재 스크린이 활성화면 비활성화 , 비활성화면 활성화 
        /// </summary>
        public bool ActiveScreen()
        {
            Debug.Log("ActiveScreen");
            float _targetV = !IsVisible() ? 1f : 0;
            isTargetActive = !IsVisible();
            if (!IsVisible() == true)
            {
                parentElement.style.opacity = _targetV;
                ShowVisualElement(parentElement, isTargetActive);
                return true;
            }
            Sequence _seq = DOTween.Sequence();
            _seq.Append(DOTween.To(() => parentElement.style.opacity.value, (x) => parentElement.style.opacity = x, _targetV, 0.125f));
            _seq.AppendCallback(() => ShowVisualElement(parentElement, false)); 
//            parentElement.style.opacity = _targetV;

            return false;
//            ShowVisualElement(parentElement, !IsVisible());
 //           bool _isVisible = IsVisible();

        }

        private bool isTargetActive; 
        public virtual void ActiveScreen(bool _isActive)
        {
            Debug.Log("ActiveScreen bool");
            float _targetV = _isActive ? 1f : 0;
            isTargetActive = _isActive; 
          //  if (_isActive == true)
           // {
            //    ShowVisualElement(parentElement, _isActive);
             //   parentElement.style.opacity = _targetV;
              //  return;
           // }
            ShowVisualElement(parentElement, _isActive);
           // Sequence _seq = DOTween.Sequence();
           // _seq.Append(DOTween.To(() => parentElement.style.opacity.value, (x) => parentElement.style.opacity = x, _targetV, 0.125f));
           // _seq.AppendCallback(() => ShowVisualElement(parentElement, false));
           // parentElement.style.opacity = _targetV;
            
            // 비활성화시 전환 이벤트 종료 후 비활성화 
            // 바로 활성화 
        }

        /// <summary>
        /// ParentElement Opacity 설정 
        /// </summary>
        /// <param name="_isVisible"></param>
        protected void EndScreenTransition(TransitionEndEvent _evt)
        {
            if (_evt.stylePropertyNames.Contains("opacity") == false || isTargetActive == true) return;
            Debug.Log(isTargetActive); 
            ShowVisualElement(parentElement, isTargetActive);
        }
        /// <summary>
        /// elements 바인딩 하기 (enum으로 쭉 쓰고 넣어줘) 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_type"></param>
        protected void Bind<T>(Type _type) where T : VisualElement
        {
            string[] names = Enum.GetNames(_type);
            List<VisualElement> elements = new VisualElement[names.Length].ToList();

            // 한 번 바인딩 했으면 
            if (this.elementsDic.ContainsKey(typeof(T)))
            {
                int _count = this.elementsDic[typeof(T)].Count;

                for (int i = 0; i < names.Length; i++)
                {
                    elements[i] = GetElement(names[i]);
                    if (elements[i] == null) // names[i] 의 이름의 요소가 없으면 에러 
                        Debug.LogError($"지정된 이름의 요소가 없습니다");
                }
                this.elementsDic[typeof(T)].AddRange(elements);

                return; 
            }

            this.elementsDic.Add(typeof(T), elements); 

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

        protected void BindVisualElements(Type type)
        {
            Bind<VisualElement>(type); 
        }
        protected void BindButtons(Type type)
        {
            Bind<Button>(type);
        }
        protected void BindProgressBars(Type type)
        {
            Bind<ProgressBar>(type);
        }
        protected void BindRadioButtons(Type type)
        {
            Bind<RadioButton>(type); 
        }
        protected void BindLabels(Type type)
        {
            Bind<Label>(type); 
        }
        protected void BindListViews(Type _type)
        {
            Bind<ListView>(_type); 
        }
        protected void BindScrollViews(Type _type)
        {
            Bind<ScrollView>(_type);
        }
        /// <summary>
        /// element 가져오기 (idx는 enum을 통해 ) 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_idx"></param>
        /// <returns></returns>
        protected T Get<T>(int _idx) where T : VisualElement
        {
            List<VisualElement> elements;
            if (this.elementsDic.TryGetValue(typeof(T), out elements) == false)
                return null;
            return elements[_idx] as T;
        }


        protected VisualElement GetVisualElement(int _idx)
        {
            return Get<VisualElement>(_idx); 
        }
        protected Button GetButton(int _idx)
        {
            return Get<Button>(_idx);
        }
        protected ProgressBar GetProgressBar(int _idx)
        {
            return Get<ProgressBar>(_idx);
        }
        protected RadioButton GetRadioButton(int _idx)
        {
            return Get<RadioButton>(_idx); 
        }
        protected Label GetLabel(int _idx)
        {
            return Get<Label>(_idx); 
        }
        protected ListView GetListView(int _idx)
        {
            return Get<ListView>(_idx); 
        }
        protected ScrollView GetScrollView(int _idx)
        {
            return Get<ScrollView>(_idx); 
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
        protected void AddButtonEvent<T>(int _idx, Action _event) where T : EventBase<T>, new() 
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
        protected void AddElementEvent<T>(int _idx, Action _event) where T : EventBase<T>, new()
        {
            // Get<타입> 
            Get<VisualElement>(_idx).RegisterCallback<T>((e) => _event?.Invoke());
        }

        /// <summary>
        /// VisualElement에 이벤트 삭제 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        protected void RemoveElementEvent<T>(int _idx, Action _event) where T : EventBase<T>, new()
        {
            // Get<타입> 
            Get<VisualElement>(_idx).UnregisterCallback<T>((e) => _event?.Invoke());
        }
        //== 값이 변경될 때 이벤트 == 

        /// <summary>
        /// RadioButton 이벤트
        /// </summary>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        protected void AddRadioBtnChangedEvent(int _idx, Action<bool> _event)
        {
            Get<RadioButton>(_idx).RegisterValueChangedCallback((e) => _event?.Invoke(e.newValue)); 
        }

        /// <summary>
        /// 텍스트필드 이벤트
        /// </summary>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        protected void AddTextFieldChangedEvent(int _idx, Action<string> _event)
        {
            Get<TextField>(_idx).RegisterValueChangedCallback((e) => _event?.Invoke(e.newValue));
        }
    }
}



