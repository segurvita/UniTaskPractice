using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class UniTaskTest
    {
        /// <summary>
        /// WaitDemoをテストする
        /// <para>
        /// UnityTestRunnerではasync/awaitが直接使えないので、
        /// 一旦Coroutineに変換している。
        /// </para>
        /// </summary>
        /// <returns></returns>
        [UnityTest]
        public IEnumerator WaitTest()
        {
            // 指定秒数
            double inputDuration = 1.0;

            // 経過秒数
            double outputDuration = 0.0;

            // 許容誤差
            double threshold = 0.01;

            // テスト対象関数を実行する
            yield return UniTask.ToCoroutine(async () =>
            {
                outputDuration = await WaitDemo(inputDuration);
            });

            // 経過秒数と指定秒数の誤差を計算する
            double diff = Math.Abs(outputDuration - inputDuration);

            // 誤差が許容範囲か検査する
            Assert.IsTrue(diff < threshold);
        }

        /// <summary>
        /// 指定秒数待つ
        /// </summary>
        /// <param name="duration">指定秒数</param>
        /// <returns>経過秒数</returns>
        async UniTask<double> WaitDemo(double duration)
        {
            // 開始時刻
            DateTime startTime = DateTime.Now;

            // 指定秒数待つ
            await UniTask.Delay(TimeSpan.FromSeconds(duration), ignoreTimeScale: false);

            // 終了時刻
            DateTime finishTime = DateTime.Now;

            // 経過時間計算
            TimeSpan ts = finishTime - startTime;

            // 経過秒数返却
            return ts.TotalSeconds;
        }
    }
}
