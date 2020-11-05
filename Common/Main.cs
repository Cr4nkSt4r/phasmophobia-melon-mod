using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Il2CppSystem;
using MelonLoader;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;
using Console = System.Console;
using String = System.String;
using Object = UnityEngine.Object;

namespace PhasmoMelonMod
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            Console.Title = string.Format("Phasmophobia");
            MelonLogger.Log("[+] Set console title to: Phasmophobia");
        }
        public override void OnLevelWasLoaded(int level)
        {
            DisableAll();
        }
        public override void OnLevelWasInitialized(int level)
        {
            if (level == 1 && canRun)
            {
                canRun = false;
                new Thread(() =>
                {
                    while (isRunning)
                    {
                        coRoutine = MelonCoroutines.Start(CollectGameObjects());
                        Thread.Sleep(5000);
                    }
                }).Start();
            }
            initializedScene = level;
            Debug.Out("initializedScene: " + initializedScene);
        }
        public override void OnUpdate()
        {
            Keyboard keyboard = Keyboard.current;

            if (keyboard.leftArrowKey.wasPressedThisFrame)
            {
                CheatToggles.enableBasicInformations = !CheatToggles.enableBasicInformations;
                MelonLogger.Log("[+] Basic informations: Toggled " + (CheatToggles.enableBasicInformations ? "On" : "Off"));
            }

            if (keyboard.upArrowKey.wasPressedThisFrame)
            {
                CheatToggles.enableEsp = !CheatToggles.enableEsp;
                MelonLogger.Log("[+] ESP: Toggled " + (CheatToggles.enableEsp ? "On" : "Off"));
            }

            if (keyboard.downArrowKey.wasPressedThisFrame)
            {
                CheatToggles.enableFullbright = !CheatToggles.enableFullbright;
                MelonLogger.Log("[+] Fullbright: Toggled " + (CheatToggles.enableFullbright ? "On" : "Off"));
                if (CheatToggles.enableFullbright == true)
                {
                    Fullbright.Enable();
                }
                else
                {
                    Fullbright.Disable();
                }
            }

            if (keyboard.insertKey.wasPressedThisFrame)
            {
                CheatToggles.guiEnabled = !CheatToggles.guiEnabled;
                MelonLogger.Log("[+] GUI: Toggled " + (CheatToggles.guiEnabled ? "On" : "Off"));

                if(CheatToggles.guiEnabled)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    if (myPlayer != null)
                        myPlayer.field_Public_FirstPersonController_0.enabled = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    if (myPlayer != null)
                        myPlayer.field_Public_FirstPersonController_0.enabled = true;
                }
            }

            if (keyboard.deleteKey.wasPressedThisFrame)
            {
                CheatToggles.enableDebug = !CheatToggles.enableDebug;
                MelonLogger.Log("[+] Debug: Toggled " + (CheatToggles.enableDebug ? "On" : "Off"));
            }

            if (keyboard.xKey.wasPressedThisFrame && !CheatToggles.guiEnabled)
            {
                Trolling.FuseBox();
            }

            if (keyboard.hKey.wasPressedThisFrame && !CheatToggles.guiEnabled)
            {
                Trolling.Hunt();
            }

            if (keyboard.iKey.wasPressedThisFrame && !CheatToggles.guiEnabled)
            {
                Trolling.Interact();
            }

            if (keyboard.kKey.wasPressedThisFrame && !CheatToggles.guiEnabled)
            {
                Trolling.RandomEvent();
            }

            if (keyboard.oKey.wasPressedThisFrame && !CheatToggles.guiEnabled)
            {
                Trolling.Appear();
            }

            if (keyboard.pKey.wasPressedThisFrame && !CheatToggles.guiEnabled)
            {
                Trolling.Idle();
            }
            if (keyboard.uKey.wasPressedThisFrame && !CheatToggles.guiEnabled)
            {
                Trolling.LockDoors(3);
            }
            if (keyboard.lKey.wasPressedThisFrame && !CheatToggles.guiEnabled)
            {
                Trolling.LockDoors(1);
            }
        }
        public override void OnGUI()
        {
            GUI.Label(new Rect(10f, 160f, 100f, 50f), "<b><color=#A302B5>ESP:</color> " + (CheatToggles.enableEsp ? "<color=#00C403>On" : "<color=#C40000>Off") + " </color></b>");
            GUI.Label(new Rect(10f, 175f, 100f, 50f), "<b><color=#A302B5>Fullbright:</color> " + (CheatToggles.enableFullbright ? "<color=#00C403>On" : "<color=#C40000>Off") + " </color></b>");
            GUI.Label(new Rect(10f, 190f, 100f, 50f), "<b><color=#A302B5>Basic Info:</color> " + (CheatToggles.enableBasicInformations ? "<color=#00C403>On" : "<color=#C40000>Off") + " </color></b>");

            if (CheatToggles.enableBasicInformations)
            {
                BasicInformations.Enable();

                GUI.Label(new Rect(0f, 0f, 500f, 160f), "", "box");
                GUI.Label(new Rect(10f, 2f, 300f, 50f), "<color=#00FF00><b>Ghost Name:</b> " + (ghostNameAge ?? "N/A") + "</color>");
                GUI.Label(new Rect(10f, 17f, 300f, 50f), "<color=#00FF00><b>Ghost Type:</b> " + (ghostType ?? "N/A") + "</color>");
                GUI.Label(new Rect(10f, 47f, 400f, 50f), "<color=#00FF00><b>Evidence:</b> " + (evidence ?? "N/A") + "</color>");
                GUI.Label(new Rect(10f, 32f, 300f, 50f), "<color=#00FF00><b>Ghost State:</b> " + (ghostState ?? "N/A") + "</color>");
                GUI.Label(new Rect(10f, 62f, 400f, 50f), "<color=#00FF00><b>Responds to:</b> " + (ghostIsShy ?? "N/A") + "</color>");
                GUI.Label(new Rect(10f, 77f, 300f, 50f), "<color=#00FF00><b>My Sanity:</b> " + (myPlayerSanity ?? "N/A") + "</color>");
            }

            if (CheatToggles.enableEsp)
                ESP.Enable();

            if(CheatToggles.guiEnabled)
            {
                if (initializedScene > 1)
                {
                    if (GUI.Button(new Rect(500f, 2f, 150f, 20f), "Hunt") && levelController != null)
                    {
                        Trolling.Hunt();
                    }
                    if (GUI.Button(new Rect(500f, 22f, 150f, 20f), "Idle") && levelController != null)
                    {
                        Trolling.Idle();
                    }
                    if (GUI.Button(new Rect(500f, 42f, 150f, 20f), "Appear") && levelController != null)
                    {
                        Trolling.Appear();
                    }
                    if (GUI.Button(new Rect(500f, 62f, 150f, 20f), "Unappear") && levelController != null)
                    {
                        Trolling.UnAppear();
                    }
                    if (GUI.Button(new Rect(500f, 82f, 150f, 20f), "FuseBox") && levelController != null)
                    {
                        Trolling.FuseBox();
                    }
                    if (GUI.Button(new Rect(650f, 2f, 150f, 20f), "Lock Exit Doors") && levelController != null)
                    {
                        Trolling.LockDoors(1);
                    }
                    if (GUI.Button(new Rect(650f, 22f, 150f, 20f), "Lock All Doors") && levelController != null)
                    {
                        Trolling.LockDoors(2);
                    }
                    if (GUI.Button(new Rect(650f, 42f, 150f, 20f), "Unlock Exit Doors") && levelController != null)
                    {
                        Trolling.LockDoors(3);
                    }
                    if (GUI.Button(new Rect(650f, 62f, 150f, 20f), "Unlock All Doors") && levelController != null)
                    {
                        Trolling.LockDoors(4);
                    }
                    if (GUI.Button(new Rect(800f, 2f, 150f, 20f), "Default Speed") && levelController != null)
                    {
                        player.field_Public_FirstPersonController_0.m_WalkSpeed = 1.6f;
                        player.field_Public_FirstPersonController_0.m_RunSpeed = 2.6f;
                    }
                    GUI.SetNextControlName("changeSpeed");
                    walkSpeedModifierString = GUI.TextArea(new Rect(800f, 22f, 150f, 20f), walkSpeedModifierString);
                    if (GUI.Button(new Rect(800f, 42f, 150f, 20f), "Set Speed") && levelController != null)
                    {
                        GUI.FocusControl("changeSpeed");
                        walkSpeedModifierInt = Int32.Parse(walkSpeedModifierString);
                        if (walkSpeedModifierInt > 0 && walkSpeedModifierInt < 99)
                        {
                            player.field_Public_FirstPersonController_0.m_WalkSpeed = 3.2f * walkSpeedModifierInt;
                            player.field_Public_FirstPersonController_0.m_RunSpeed = 5.2f * walkSpeedModifierInt;

                            MelonLogger.Log("[+] Set walk/run speed x " + walkSpeedModifierInt);
                        }
                        else
                        {
                            walkSpeedModifierInt = 1;
                            MelonLogger.Log("Speed modifier not between 0-99");
                        }
                    }
                }
                else
                {
                    if (initializedScene == 1)
                    {
                        if (GUI.Button(new Rect(500f, 2f, 150f, 20f), "+ 1.000$") && levelController == null)
                        {
                            FileBasedPrefs.SetInt("PlayersMoney", FileBasedPrefs.GetInt("PlayersMoney", 0) + 1000);
                        }
                        if (GUI.Button(new Rect(500f, 22f, 150f, 20f), "+ 100XP") && levelController == null)
                        {
                            FileBasedPrefs.SetInt("myTotalExp", FileBasedPrefs.GetInt("myTotalExp", 0) + 100);
                        }
                        if (GUI.Button(new Rect(500f, 42f, 150f, 20f), "- 1.000$") && levelController == null)
                        {
                            FileBasedPrefs.SetInt("PlayersMoney", FileBasedPrefs.GetInt("PlayersMoney", 0) - 1000);
                        }
                        if (GUI.Button(new Rect(500f, 62f, 150f, 20f), "- 1.000XP") && levelController == null)
                        {
                            FileBasedPrefs.SetInt("myTotalExp", FileBasedPrefs.GetInt("myTotalExp", 0) - 1000);
                        }
                        GUI.SetNextControlName("changeName");
                        playerName = GUI.TextArea(new Rect(650f, 2f, 150f, 20f), playerName);
                        if (GUI.Button(new Rect(650f, 22f, 150f, 20f), "Change Name"))
                        {
                            GUI.FocusControl("changeName");
                            PhotonNetwork.NickName = playerName;
                            MelonLogger.Log("[+] Set name: " + playerName);
                        }
                    }
                }
            }
        }
        public override void OnModSettingsApplied()
        {
        }

        private void DisableAll()
        {
            CheatToggles.enableBasicInformations = false;
            CheatToggles.guiEnabled = false;
            CheatToggles.enableEsp = false;
            CheatToggles.enableFullbright = false;
            BasicInformations.Reset();
        }

        private Player GetLocalPlayer()
        {
            if (players == null || players.Count == 0)
            {
                return null;
            }
            if (players.Count == 1)
            {
                return players[0];
            }
            foreach (Player player in players)
            {
                if (player != null && player.field_Public_PhotonView_0 != null && player.field_Public_PhotonView_0.AmOwner)
                {
                    return player;
                }
            }
            return null;
        }

        IEnumerator CollectGameObjects()
        {
            cameraMain = Camera.main;
            yield return new WaitForSeconds(0.15f);
            Debug.Out("cameraMain");

            dnaEvidences = Object.FindObjectsOfType<DNAEvidence>().ToList<DNAEvidence>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("dnaEvidences");

            doors = Object.FindObjectsOfType<Door>().ToList<Door>() ?? null;
            yield return new WaitForSeconds(0.15f);
            Debug.Out("doors");

            fuseBox = Object.FindObjectOfType<FuseBox>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("fuseBox");

            gameController = Object.FindObjectOfType<GameController>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("gameController");

            ghostAI = Object.FindObjectOfType<GhostAI>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("ghostAI");

            ghostAIs = Object.FindObjectsOfType<GhostAI>().ToList<GhostAI>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("ghostAIs"); 
            
            ghostActivity = Object.FindObjectOfType<GhostActivity>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("ghostActivity");

            ghostInfo = Object.FindObjectOfType<GhostInfo>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("ghostInfo");

            levelController = Object.FindObjectOfType<LevelController>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("levelController");

            ouijaBoards = Object.FindObjectsOfType<OuijaBoard>().ToList<OuijaBoard>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("ouijaBoards");

            if (Object.FindObjectOfType<Player>() != null)
            {
                player = Object.FindObjectOfType<Player>() ?? null;
                yield return new WaitForSeconds(0.15f);
                Debug.Out("player");

                players = Object.FindObjectsOfType<Player>().ToList<Player>() ?? null;
                yield return new WaitForSeconds(0.15f);
                Debug.Out("players");

                myPlayer = GetLocalPlayer() ?? player;
                Debug.Out("myPlayer");
                yield return new WaitForSeconds(0.15f);

                playerAnim = myPlayer.field_Public_Animator_0 ?? null;
                yield return new WaitForSeconds(0.15f);
                Debug.Out("playerAnim");

                if (playerAnim != null)
                {
                    boneTransform = playerAnim.GetBoneTransform(HumanBodyBones.Head) ?? null;
                    yield return new WaitForSeconds(0.15f);
                    Debug.Out("boneTransform");
                    if (boneTransform != null)
                    {
                        light = boneTransform.GetComponent<Light>() ?? null;
                        yield return new WaitForSeconds(0.15f);
                        Debug.Out("light");
                    }
                }
            }

            if (levelController != null)
            {
                photonView = ghostAI.field_Public_PhotonView_0 ?? null;
                yield return new WaitForSeconds(0.15f);
                Debug.Out("photonView");
            }

            serverManager = Object.FindObjectOfType<ServerManager>();
            yield return new WaitForSeconds(0.15f);
            Debug.Out("playerAnim");

            Debug.Out("-----------------------------");
            yield return null;
        }

        public static Transform boneTransform;
        public static Camera cameraMain;
        public static List<DNAEvidence> dnaEvidences;
        public static List<Door> doors;
        public static GameController gameController;
        public static GhostAI ghostAI;
        public static List<GhostAI> ghostAIs;
        public static FuseBox fuseBox;
        public static GhostActivity ghostActivity;
        public static GhostController ghostController;
        public static GhostEventPlayer ghostEventPlayer;
        public static GhostInfo ghostInfo;
        public static LevelController levelController;
        public static Light light;
        public static Player myPlayer;
        public static List<OuijaBoard> ouijaBoards;
        public static PhotonView photonView;
        public static Player player;
        public static List<Player> players;
        public static Animator playerAnim;
        public static ServerManager serverManager;
        public static String ghostNameAge;
        public static String ghostType;
        public static String evidence;
        public static String ghostState;
        public static String ghostIsShy;
        public static String myPlayerSanity;
        private static object coRoutine;
        private static bool canRun = true;
        private static bool isRunning = true;
        private static int initializedScene;
        private static string walkSpeedModifierString;
        private static int walkSpeedModifierInt = 1;
        private static string playerName;
    }
}
