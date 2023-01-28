using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill;

namespace Utill.ExtraStruct
{
	public class PriorityDelegate<T>
	{
		public delegate void priorityDel(ref T _item1);
		private List<Tuple<int, priorityDel>> delList = new List<Tuple<int, priorityDel>>();
		private priorityDel del;

		/// <summary>
		/// Invoke 후 실행값 가져오기
		/// </summary>
		/// <param name="_value"></param>
		/// <returns></returns>
		public T ReturnValue(ref T _value)
		{
			del.Invoke(ref _value);
			return _value;
		}

		/// <summary>
		/// 이벤트 추가
		/// </summary>
		/// <param name="addEvent"></param>
		public void AddEvent(Tuple<int, priorityDel> _tuple)
		{
			foreach (var remove in delList)
			{
				del -= remove.Item2;
			}

			delList.Add(_tuple);
			delList = delList.OrderBy(a => a.Item1).ToList();

			foreach (var add in delList)
			{
				del += add.Item2;
			}
		}

		/// <summary>
		/// 이벤트 제거
		/// </summary>
		/// <param name="_removeEvent"></param>
		public void RemoveEvent(priorityDel _removeEvent)
		{
			delList.RemoveAt(delList.FindIndex(x => x.Item2 == _removeEvent));
			del -= _removeEvent;
		}
	}
}
