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
        protected string parentElementName; // �ڱ� �ڽ� �̸� 
        protected VisualElement parentElement; // �ڱ� �ڽ� 

        protected Dictionary<Type, List<VisualElement>> elementsDic = new Dictionary<Type, List<VisualElement>>(); // ��� �ڽ� ��ҵ� 

        // ������Ƽ 
        public VisualElement ParentElement => parentElement;

        /// <summary>
        /// parentElement ĳ�� 
        /// </summary>
        public virtual void Cashing()
        {
            if (uiDocument == null /*|| String.IsNullOrEmpty(_parentElementName)*/)
            {
                Debug.LogError("NULL���� : �ν����� Ȯ��");
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
        /// �ʱ�ȭ 
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
        /// Ŭ���� �̸����� element �������� 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        protected VisualElement GetElementClassName(string _className)
        {
            return parentElement.Q(className: _className);
        }

        /// <summary>
        /// �̸����� element �������� ĳ���ϱ� 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected VisualElement GetElement(string name)
        {
            // �̹� ĳ�̵Ǿ� ������ ��ųʸ����� ��������
            //VisualElement element = _elementsDic.Where(e => e.Key == name).Select(e => e.Value).First();
            //if(element == null) // ���ٸ� ã�ƿ��� 
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
        /// ���� Ȱ��ȭ�Ǿ� �ִ°� ( element�� null�̸� �ڽ� ) 
        /// </summary>
        /// <returns></returns>
        protected bool IsVisible(VisualElement _element = null)
        {
            VisualElement e = _element != null ? _element : parentElement;
            return e.style.display == DisplayStyle.Flex;
        }

        /// <summary>
        /// ���� ��ũ���� Ȱ��ȭ�� ��Ȱ��ȭ , ��Ȱ��ȭ�� Ȱ��ȭ 
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
            
            // ��Ȱ��ȭ�� ��ȯ �̺�Ʈ ���� �� ��Ȱ��ȭ 
            // �ٷ� Ȱ��ȭ 
        }

        /// <summary>
        /// ParentElement Opacity ���� 
        /// </summary>
        /// <param name="_isVisible"></param>
        protected void EndScreenTransition(TransitionEndEvent _evt)
        {
            if (_evt.stylePropertyNames.Contains("opacity") == false || isTargetActive == true) return;
            Debug.Log(isTargetActive); 
            ShowVisualElement(parentElement, isTargetActive);
        }
        /// <summary>
        /// elements ���ε� �ϱ� (enum���� �� ���� �־���) 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_type"></param>
        protected void Bind<T>(Type _type) where T : VisualElement
        {
            string[] names = Enum.GetNames(_type);
            List<VisualElement> elements = new VisualElement[names.Length].ToList();

            // �� �� ���ε� ������ 
            if (this.elementsDic.ContainsKey(typeof(T)))
            {
                int _count = this.elementsDic[typeof(T)].Count;

                for (int i = 0; i < names.Length; i++)
                {
                    elements[i] = GetElement(names[i]);
                    if (elements[i] == null) // names[i] �� �̸��� ��Ұ� ������ ���� 
                        Debug.LogError($"������ �̸��� ��Ұ� �����ϴ�");
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
                if (elements[i] == null) // names[i] �� �̸��� ��Ұ� ������ ���� 
                    Debug.LogError($"������ �̸��� ��Ұ� �����ϴ�");
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
        /// element �������� (idx�� enum�� ���� ) 
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
        // == �̺�Ʈ ���� == //

        //public void AddClickEvent(int _idx,Action _event)
        //{
        //    Get<VisualElement>(_idx).RegisterCallback<ClickEvent>((e) => _event?.Invoke());

        //    AddButtonEvent<ClickEvent>(1,()=> { });
        //}

        // == �̺κ� ����? 

        /// <summary>
        /// ��ư�� �̺�Ʈ �߰� 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        protected void AddButtonEvent<T>(int _idx, Action _event) where T : EventBase<T>, new() 
        {
            // Get<Ÿ��> 
            Get<Button>(_idx).RegisterCallback<T>((e) => _event?.Invoke());
        }

        /// <summary>
        /// VisualElement�� �̺�Ʈ �߰� 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        protected void AddElementEvent<T>(int _idx, Action _event) where T : EventBase<T>, new()
        {
            // Get<Ÿ��> 
            Get<VisualElement>(_idx).RegisterCallback<T>((e) => _event?.Invoke());
        }

        /// <summary>
        /// VisualElement�� �̺�Ʈ ���� 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        protected void RemoveElementEvent<T>(int _idx, Action _event) where T : EventBase<T>, new()
        {
            // Get<Ÿ��> 
            Get<VisualElement>(_idx).UnregisterCallback<T>((e) => _event?.Invoke());
        }
        //== ���� ����� �� �̺�Ʈ == 

        /// <summary>
        /// RadioButton �̺�Ʈ
        /// </summary>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        protected void AddRadioBtnChangedEvent(int _idx, Action<bool> _event)
        {
            Get<RadioButton>(_idx).RegisterValueChangedCallback((e) => _event?.Invoke(e.newValue)); 
        }

        /// <summary>
        /// �ؽ�Ʈ�ʵ� �̺�Ʈ
        /// </summary>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        protected void AddTextFieldChangedEvent(int _idx, Action<string> _event)
        {
            Get<TextField>(_idx).RegisterValueChangedCallback((e) => _event?.Invoke(e.newValue));
        }
    }
}



