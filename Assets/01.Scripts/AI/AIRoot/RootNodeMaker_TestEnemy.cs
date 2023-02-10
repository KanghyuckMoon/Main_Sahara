using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utill.Addressable;
using Utill.Pattern;
using Module;
using static NodeUtill;

namespace AI
{
	public partial class RootNodeMaker
	{
		private partial INode TestEnemy()
		{
			return Selector
			(
				IgnoreAction(Reset), //����
				IgnoreAction(TargetFind),
				IgnoreAction(SuspicionGaugeSet),
				IfAction(NotDiscoveryCondition, Ignore),
				IfAction(FerCloserMoveCondition, RunMove), //�ʹ� �ָ� ������ ����
				IfSelector(AttackRangeCondition, //���� ��Ÿ� �ȿ� ��������
					IfAction(AttackCondition, Attack), //�þ߰��� �Ǹ� ����
					Action(Rotate) //�þ߰��� �� �Ǹ� ȸ��
				),
				IfAction(JumpMoveCondition, JumpAndRunMove),
				Action(CloserMove)
				//PercentRandomChoiceNode(0.1f,
				//	PercentAction(10f, Action(Jump)), //����
				//	PercentAction(90f, Action(CloserMove)) //����
				//	)
			);
		}
	}

}