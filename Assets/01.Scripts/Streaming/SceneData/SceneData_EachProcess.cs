using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Streaming.StreamingUtill;

namespace Streaming
{
	public partial class SceneData
	{
		partial void InitScene(string _sceneName)
		{
			if (sceneName.Contains("Map("))
			{
				for (int i = 0; i < 100; ++i)
				{
					ObjectData obj1 = new ObjectData();
					obj1.address = "Camera";
					obj1.lodType = LODType.On;
					obj1.lodAddress = "CameraLOD";
					obj1.key = ObjectData.totalKey++;
					obj1.position = StringToVector3(sceneName) * 100f + new Vector3(0,10,0);
					obj1.rotation = Quaternion.identity;
					obj1.scale = new Vector3(1, 1, 1) * 10f;
					objectDataList.Add(obj1);
				}
			}

			//switch (sceneName)
			//{
			//	case "Map(0,0,0)":
			//		ObjectData obj1 = new ObjectData();
			//		obj1.address = "Rock";
			//		obj1.key = 1000;
			//		obj1.position = new Vector3(0, 0, 0);
			//		obj1.rotation = new Vector3(10, 50, 180);
			//		obj1.scale = new Vector3(100, 100, 100);

			//		objectDatas.Add(obj1);
			//		break;

			//	default:
			//		break;
			//}
		}
	}

}