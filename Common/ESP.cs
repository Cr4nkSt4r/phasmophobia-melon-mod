using UnityEngine;

namespace C4PhasMod
{
    class ESP
    {
        public static void Enable()
        {
            if (Main.initializedScene > 1) {
                if (CheatToggles.enableEspGhost == true && Main.gameController != null && Main.ghostAI != null && Main.ghostAIs.Count > 0)
                {
                    foreach (GhostAI ghostAI in Main.ghostAIs)
                    {
                        Vector3 w2s = Main.cameraMain.WorldToScreenPoint(ghostAI.transform.position);
                        Vector3 ghostPosition = Main.cameraMain.WorldToScreenPoint(ghostAI.field_Public_Transform_0.transform.position);

                        Vector3 w2s2 = ghostAI.transform.position;
                        Vector3 ghostPosition2 = ghostAI.field_Public_Transform_0.transform.position;

                        //GUI.Label(new Rect(10f, 200f, 300f, 50f), "<color=#00FF00><b>ghostPosition X:</b> " + ghostPosition2.x + "</color>");
                        //GUI.Label(new Rect(10f, 215f, 300f, 50f), "<color=#00FF00><b>ghostPosition Y:</b> " + ghostPosition2.y + "</color>");
                        //GUI.Label(new Rect(10f, 230f, 300f, 50f), "<color=#00FF00><b>ghostPosition Z:</b> " + ghostPosition2.z + "</color>");
                        //GUI.Label(new Rect(10f, 245f, 300f, 50f), "<color=#00FF00><b>w2s X:</b> " + w2s2.x + "</color>");
                        //GUI.Label(new Rect(10f, 260f, 300f, 50f), "<color=#00FF00><b>w2s Y:</b> " + w2s2.y + "</color>");
                        //GUI.Label(new Rect(10f, 275f, 300f, 50f), "<color=#00FF00><b>w2s Z:</b> " + w2s2.z + "</color>");

                        float ghostNeckMid = Screen.height - ghostPosition.y;
                        float ghostBottomMid = Screen.height - w2s.y;
                        float ghostTopMid = ghostNeckMid - (ghostBottomMid - ghostNeckMid) / 5;
                        float boxHeight = (ghostBottomMid - ghostTopMid);
                        float boxWidth = boxHeight / 1.8f;

                        if (w2s.z < 0)
                            continue;

                        Drawing.DrawBoxOutline(new Vector2(w2s.x - (boxWidth / 2f), ghostNeckMid), boxWidth, boxHeight, Color.red);
                    }
                    Vector3 w2sP = Main.cameraMain.WorldToScreenPoint(Main.myPlayer.transform.position);
                    Vector3 playerPosition = Main.cameraMain.WorldToScreenPoint(Main.myPlayer.field_Public_Transform_1.transform.position);

                    //GUI.Label(new Rect(10f, 290f, 300f, 50f), "---------------------------------------");
                    //GUI.Label(new Rect(10f, 305f, 300f, 50f), "<color=#00FF00><b>playerPosition X:</b> " + playerPosition.x + "</color>");
                    //GUI.Label(new Rect(10f, 320f, 300f, 50f), "<color=#00FF00><b>playerPosition Y:</b> " + playerPosition.y + "</color>");
                    //GUI.Label(new Rect(10f, 335f, 300f, 50f), "<color=#00FF00><b>playerPosition Z:</b> " + playerPosition.z + "</color>");
                    //GUI.Label(new Rect(10f, 350f, 300f, 50f), "<color=#00FF00><b>w2s X:</b> " + w2sP.x + "</color>");
                    //GUI.Label(new Rect(10f, 365f, 300f, 50f), "<color=#00FF00><b>w2s Y:</b> " + w2sP.y + "</color>");
                    //GUI.Label(new Rect(10f, 380f, 300f, 50f), "<color=#00FF00><b>w2s Z:</b> " + w2sP.z + "</color>");
                }

                if (CheatToggles.enableEspPlayer == true && Main.gameController != null && Main.players != null && Main.players.Count > 1)
                {
                    foreach (Player player in Main.players)
                    {
                        if (player.field_Public_PhotonView_0 != null && player.field_Public_PhotonView_0.AmOwner)
                            continue;

                        Vector3 w2s = Main.cameraMain.WorldToScreenPoint(player.transform.position);
                        Vector3 playerPosition = Main.cameraMain.WorldToScreenPoint(player.field_Public_Transform_1.transform.position);

                        float playerNeckMid = Screen.height - playerPosition.y;
                        float playerBottomMid = Screen.height - w2s.y;
                        float playerTopMid = playerNeckMid - (playerBottomMid - playerNeckMid) / 5;
                        float boxHeight = (playerBottomMid - playerTopMid);
                        float boxWidth = boxHeight / 1.8f;

                        if (w2s.z < 0)
                            continue;

                        Drawing.DrawBoxOutline(new Vector2(w2s.x - (boxWidth / 2f), playerNeckMid), boxWidth, boxHeight, Color.green);
                        GUI.Label(new Rect(new Vector2(w2s.x, Screen.height - (w2s.y + 1f)), new Vector2(100f, 100f)), player.field_Public_PhotonView_0.owner.NickName);
                    }
                }

                if (CheatToggles.enableEspBone == true && Main.gameController != null && Main.dnaEvidences != null && Main.dnaEvidences.Count > 0)
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

                if (CheatToggles.enableEspOuija == true && Main.gameController != null && Main.ouijaBoards != null && Main.ouijaBoards.Count > 0)
                {
                    foreach (OuijaBoard ouijaBoard in Main.ouijaBoards)
                    {
                        Vector3 vector2 = Main.cameraMain.WorldToScreenPoint(ouijaBoard.transform.position);
                        if (vector2.z > 0f)
                        {
                            GUI.Label(new Rect(new Vector2(vector2.x, Screen.height - (vector2.y + 1f)), new Vector2(100f, 100f)), "<color=#f6ff00><b>Ouija Board</b></color>");
                        }
                    }
                }

                if (CheatToggles.enableEspEmf == true && Main.gameController != null && Main.emf != null && Main.emf.Count > 0)
                {
                    foreach (EMF emf in Main.emf)
                    {
                        Vector3 vector = Camera.main.WorldToScreenPoint(emf.transform.position);
                        if (vector.z > 0f)
                        {
                            vector.y = Screen.height - (vector.y + 1f);
                            GUI.color = new Color32(210, 31, 255, 255);
                            string spotName = "";

                            switch ((int)emf.field_Public_EnumNPublicSealedvaGh5vGhGhGhUnique_0)
                            {
                                case 0:
                                    spotName = "EMF: Interaction";
                                    break;
                                case 1:
                                    spotName = "EMF: Thrown";
                                    break;
                                case 2:
                                    spotName = "EMF: Appeared";
                                    break;
                                case 3:
                                    spotName = "EMF: Evidence";
                                    break;
                            }
                            GUI.Label(new Rect(new Vector2(vector.x, vector.y), new Vector2(100f, 100f)), spotName);
                        }
                    }
                }

                if (CheatToggles.enableEspFuseBox == true && Main.gameController != null && Main.fuseBox != null)
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
