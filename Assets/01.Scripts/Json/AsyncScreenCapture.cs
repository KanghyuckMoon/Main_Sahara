// (c) longod, MIT License
using System;
using System.Collections;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

/// <summary>
/// Asynchronous Capture Screen
/// require Unity 2019.3 or later
/// </summary>
public class AsyncScreenCapture : IDisposable {

    public IEnumerator CaptureAsync(string uniquePath) {
        yield return new WaitForEndOfFrame();
        Profiler.BeginSample( "AsyncScreenCapture.CaptureAsync" );
        int width = Screen.width;
        int height = Screen.height;
        GraphicsFormat graphicsFormat = GraphicsFormat.R8G8B8A8_SRGB;
        var index = FindUsableTexture( width, height, graphicsFormat );
        if (index < 0) {
            Debug.LogError( "not enough buffer" );
        } else {
            ScreenCapture.CaptureScreenshotIntoRenderTexture( buffer[ index ].renderTexture );
            buffer[ index ].request = AsyncGPUReadback.Request( buffer[ index ].renderTexture, 0, graphicsFormat, (request) => { ReadbackCompleted( request, uniquePath, buffer[ index ].renderTexture ); } );
        }
        Profiler.EndSample();
    }

    public void Dispose() {
        for (int i = 0; i < buffer.Length; ++i) {
            if (!buffer[ i ].request.done) {
                buffer[ i ].request.WaitForCompletion(); // sync
                if (buffer[ i ].renderTexture != null) {
                    UnityEngine.Object.Destroy( buffer[ i ].renderTexture );
                    buffer[ i ].renderTexture = null;
                }
            }
        }
        buffer = null;
Debug.Log("NULL1");
    }

    private void ReadbackCompleted(AsyncGPUReadbackRequest request, string path, RenderTexture renderTexture) {
        if (flipSampler == null) {
            flipSampler = CustomSampler.Create( "AsyncScreenCapture.FlipY" );
        }
        if (encodeSampler == null) {
            encodeSampler = CustomSampler.Create( "AsyncScreenCapture.Encode" );
        }
        if (writeSampler == null) {
            writeSampler = CustomSampler.Create( "AsyncScreenCapture.Write" );
        }
        Profiler.BeginSample( "AsyncScreenCapture.ReadbackCompleted" );
        uint width = (uint)renderTexture.width;
        uint height = (uint)renderTexture.height;
        var graphicsFormat = renderTexture.graphicsFormat;
        var managed = request.GetData<byte>().ToArray();

        // 専用の単一スレッドで実行すると同時に同じパスに書き込む可能性は解消できるが、詰まりやすくなる
        Task.Run( () => {
            Profiler.BeginThreadProfiling( "Task", $"Thread {Thread.CurrentThread.ManagedThreadId}" );
            flipSampler.Begin();
            var image = new byte[ managed.Length ];
            int pitch = 4 * (int)width; // R8G8B8A8
            for (int y = 0; y < height; ++y) {
                Buffer.BlockCopy( managed, pitch * y, image, ((int)height - 1 - y) * pitch, pitch );
            }
            flipSampler.End();
            encodeSampler.Begin();
            byte[] bin = ImageConversion.EncodeArrayToPNG( image, graphicsFormat, width, height );
            encodeSampler.End();
            writeSampler.Begin();
            File.WriteAllBytes( path, bin );
            writeSampler.End();
            Profiler.EndThreadProfiling();
        } );

        Profiler.EndSample();
    }

    private int FindUsableTexture(int width, int height, GraphicsFormat graphicsFormat) {
		Debug.Log("NULL2");
        int index = -1;
        for (int i = 0; i < buffer.Length; ++i) {
            if (buffer[ i ].request.done) {
                if (index < 0) {
                    index = i;
                }
                if (buffer[ i ].renderTexture != null &&
                    buffer[ i ].renderTexture.width == width &&
                    buffer[ i ].renderTexture.height == height &&
                    buffer[ i ].renderTexture.graphicsFormat == graphicsFormat) {
                    // found reusable texture
                    return i;
                }
            }
        }
        // recreate
        if (index >= 0) {
            if (buffer[ index ].renderTexture != null) {
                UnityEngine.Object.Destroy( buffer[ index ].renderTexture );
            }
            buffer[ index ].renderTexture = new RenderTexture( width, height, 0, graphicsFormat, 0 );
        }
        return index;
    }

    struct RequestingBuffer {
        internal AsyncGPUReadbackRequest request;
        internal RenderTexture renderTexture;
    }
    RequestingBuffer[] buffer = new RequestingBuffer[ maxBufferCount ];
    // RenderTextureが要求される寿命は、CaptureScreenshotIntoRenderTextureから、
    // リードバック完了（コールバック呼び出し開始時まで）、それ以降は破棄されても問題ない。
    // ハードウェアと解像度によるが、大体リードバックは2,3フレームで完了するので、
    // 高解像度で毎フレーム1枚撮ったとしてもこれくらいあれば十分だろうというバッファ数。
    static readonly int maxBufferCount = 8;

    static CustomSampler flipSampler = null;
    static CustomSampler encodeSampler = null;
    static CustomSampler writeSampler = null;
	
}