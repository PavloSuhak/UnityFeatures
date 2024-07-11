using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Game.Scripts.Extensions
{
    public static class TMPTextExtensions
    {
        public static async UniTask FadeInAsync(this TMP_Text textMeshPro, float speed)
        {
            textMeshPro.ForceMeshUpdate();
            var textInfo = textMeshPro.textInfo;
            var characterCount = textInfo.characterCount;
        
            ResetAlphaToZero(textMeshPro, characterCount, textInfo);

            var elapsedTime = 0f;
            var durationPerLetter = 1f / speed;

            while (true)
            {
                var allCharactersFullyVisible = true;

                for (var i = 0; i < characterCount; i++)
                {
                    var charInfo = textInfo.characterInfo[i];
                    if (!charInfo.isVisible)
                        continue;

                    var materialIndex = charInfo.materialReferenceIndex;
                    var newVertexColors = textInfo.meshInfo[materialIndex].colors32;
                    var vertexIndex = charInfo.vertexIndex;

                    var letterTargetTime = i * durationPerLetter;
                    var alphaProgress = Mathf.Clamp01(elapsedTime - letterTargetTime);
                    var alphaByte = (byte)(alphaProgress * 255);

                    newVertexColors[vertexIndex + 0].a = alphaByte;
                    newVertexColors[vertexIndex + 1].a = alphaByte;
                    newVertexColors[vertexIndex + 2].a = alphaByte;
                    newVertexColors[vertexIndex + 3].a = alphaByte;

                    if (alphaByte < 255)
                    {
                        allCharactersFullyVisible = false;
                    }
                }

                textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);

                if (allCharactersFullyVisible)
                    break;

                elapsedTime += Time.deltaTime;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        private static void ResetAlphaToZero(TMP_Text textMeshPro, int characterCount, TMP_TextInfo textInfo)
        {
            for (var i = 0; i < characterCount; i++)
            {
                var charInfo = textInfo.characterInfo[i];
                if (!charInfo.isVisible)
                    continue;

                var materialIndex = charInfo.materialReferenceIndex;
                var newVertexColors = textInfo.meshInfo[materialIndex].colors32;
                var vertexIndex = charInfo.vertexIndex;

                newVertexColors[vertexIndex + 0].a = 0;
                newVertexColors[vertexIndex + 1].a = 0;
                newVertexColors[vertexIndex + 2].a = 0;
                newVertexColors[vertexIndex + 3].a = 0;
            }

            textMeshPro.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }
    }
}
