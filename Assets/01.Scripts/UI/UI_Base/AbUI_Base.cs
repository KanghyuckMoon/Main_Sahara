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
        protected string parentElementName; // �ڱ� �ڽ� �̸� 
        protected VisualElement parentElement; // �ڱ� �ڽ� 

        protected Dictionary<string, VisualElement> elementsDic = new Dictionary<string, VisualElement>(); // ��� �ڽ� ��ҵ� 
        protected Dictionary<Type, VisualElement[]> elements = new Dictionary<Type, VisualElement[]>(); // ��� �ڽ� ��ҵ� 

        /// <summary>
        /// Element ĳ�� 
        /// </summary>
        public virtual void Cashing()
        {
            if(uiDocument == null /*|| String.IsNullOrEmpty(_parentElementName)*/)
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
        public virtual void Init() { }

        public VisualElement GetElementByName(string _name)
        {
            return new VisualElement(); 
        }

        /// <summary>
        /// Ŭ���� �̸����� element �������� 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public VisualElement GetElementClassName(string _className)
        {
            return parentElement.Q(className: _className);
        }
        
        /// <summary>
        /// �̸����� element �������� ĳ���ϱ� 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public VisualElement GetElement(string name)
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
        public static void ShowVisualElement(VisualElement _visualElement, bool _state)
        {
            if (_visualElement == null)
                return;
            _visualElement.style.display = (_state) ? DisplayStyle.Flex : DisplayStyle.None;
        }

        /// <summary>
        /// ���� Ȱ��ȭ�Ǿ� �ִ°� ( element�� null�̸� �ڽ� ) 
        /// </summary>
        /// <returns></returns>
        public bool IsVisible(VisualElement _element = null)
        {
            VisualElement e = _element != null ? _element : parentElement; 
            return e.style.display == DisplayStyle.Flex; 
        }

        /// <summary>
        /// ���� ��ũ���� Ȱ��ȭ�� ��Ȱ��ȭ , ��Ȱ��ȭ�� Ȱ��ȭ 
        /// </summary>
        public void ActiveScreen()
        {
            ShowVisualElement(parentElement, !IsVisible());
        }

        /// <summary>
        /// elements ���ε� �ϱ� (enum���� �� ���� �־���) 
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
                if (elements[i] == null) // names[i] �� �̸��� ��Ұ� ������ ���� 
                    Debug.LogError($"������ �̸��� ��Ұ� �����ϴ�");
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
        /// element �������� (idx�� enum�� ���� ) 
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
        public void AddButtonEvent<T>(int _idx, Action _event) where T : EventBase<T>, new() 
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
        public void AddVisualElementEvent<T>(int _idx, Action _event) where T : EventBase<T>, new()
        {
            // Get<Ÿ��> 
            Get<VisualElement>(_idx).RegisterCallback<T>((e) => _event?.Invoke());
        }

        //== ���� ����� �� �̺�Ʈ == 

        /// <summary>
        /// RadioButton �̺�Ʈ
        /// </summary>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        public void AddRadioBtnChangedEvent(int _idx, Action<bool> _event)
        {
            Get<RadioButton>(_idx).RegisterValueChangedCallback((e) => _event?.Invoke(e.newValue)); 
        }

        /// <summary>
        /// �ؽ�Ʈ�ʵ� �̺�Ʈ
        /// </summary>
        /// <param name="_idx"></param>
        /// <param name="_event"></param>
        public void AddTextFieldChangedEvent(int _idx, Action<string> _event)
        {
            Get<TextField>(_idx).RegisterValueChangedCallback((e) => _event?.Invoke(e.newValue));
        }
    }
}



