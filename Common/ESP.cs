using UnityEngine;

namespace PhasmoMelonMod
{
    class ESP
    {
        public static void Enable()
        {
            if (Main.gameController != null && Main.ghostAI != null)
            {
                foreach (GhostAI ghostAI in Main.ghostAIs)
                {
                    Vector3 w2s = Main.cameraMain.WorldToScreenPoint(ghostAI.transform.position);
                    Vector3 ghostPosition = Main.cameraMain.WorldToScreenPoint(ghostAI.field_Public_Transform_0.transform.position);

                    float ghostNeckMid = Screen.height - ghostPosition.y;
                    float ghostBottomMid = Screen.height - w2s.y;
                    float ghostTopMid = ghostNeckMid - (ghostBottomMid - ghostNeckMid) / 5;
                    float boxHeight = (ghostBottomMid - ghostTopMid);
                    float boxWidth = boxHeight / 1.8f;

                    if (w2s.z < 0)
                        continue;

                    Drawing.DrawBoxOutline(new Vector2(w2s.x - (boxWidth / 2f), ghostNeckMid), boxWidth, boxHeight, Color.cyan);
                }
                if (Main.dnaEvidences != null)
                {
                    foreach (DNAEvidence dnaEvidence in Main.dnaEvidences)
                    {
                        Vector3 vector3 = Main.cameraMain.WorldToScreenPoint(dnaEvidence.transform.position);
                        if (vector3.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector3.x, Screen.height - (vector3.y + 1f)), new Vector2(100f, 100f)), "<color=#FFFFFF><b>Bone</b></color>");
                        }
                    }
                }
                if (Main.ouijaBoards != null)
                {
                    foreach (OuijaBoard ouijaBoard in Main.ouijaBoards)
                    {
                        Vector3 vector2 = Main.cameraMain.WorldToScreenPoint(ouijaBoard.transform.position);
                        if (vector2.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector2.x, Screen.height - (vector2.y + 1f)), new Vector2(100f, 100f)), "<color=#D11500><b>Ouija Board</b></color>");
                        }
                    }
                }
                if (Main.fuseBox != null)
                {
                    Vector3 vector3 = Main.cameraMain.WorldToScreenPoint(Main.fuseBox.transform.position);
                    if (vector3.z > 0f)
                    {
                        GUI.Label(new Rect(new Vector2(vector3.x, Screen.height - (vector3.y + 1f)), new Vector2(100f, 100f)), "<color=#EBC634><b>FuseBox</b></color>");
                    }
                }
            }
        }
    }
}
