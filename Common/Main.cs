using MelonLoader;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using Console = System.Console;
using Object = UnityEngine.Object;
using String = System.String;

namespace C4PhasMod
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            BasicInject.Main();
            Debug.Msg("Set console title to: Phasmophobia", 1);
            Console.Title = string.Format("Phasmophobia");

            HandleConfig();
        }

        public override void OnLevelWasLoaded(int index)
        {
            if (initializedScene == 1)
                gameStarted = true;
        }

        public override void OnLevelWasInitialized(int index)
        {
            initializedScene = index;
            if (initializedScene > 0 && canRun)
            {
                canRun = false;
                coRoutine = null;
                isRunning = false;
                new Thread(() =>
                {
                    while (true)
                    {
                        if (!isRunning)
                        {
                            coRoutine = MelonCoroutines.Start(CollectGameObjects());
                        }
                        Thread.Sleep(5000);
                    }
                }).Start();
            }
            Debug.Msg("Initialized Scene: " + mapNames[initializedScene], 1);
            if (gameStarted && initializedScene == 1)
                DisableAll();

            if (initializedScene == 1 && !canRun)
            {
                MelonCoroutines.Stop(coRoutine);
                canRun = true;
            }
        }
        public override void OnUpdate()
        {
            Keyboard keyboard = Keyboard.current;

            if (keyboard.leftArrowKey.wasPressedThisFrame)
            {
                CheatToggles.enableBI = !CheatToggles.enableBI;
                if (CheatToggles.enableBI)
                {
                    CheatToggles.enableBIGhost = true;
                    CheatToggles.enableBIMissions = true;
                    CheatToggles.enableBIPlayer = true;
                }
                else
                {
                    CheatToggles.enableBIGhost = false;
                    CheatToggles.enableBIMissions = false;
                    CheatToggles.enableBIPlayer = false;
                }
                Debug.Msg("Basic informations: Toggled " + (CheatToggles.enableBI ? "On" : "Off"), 1);
            }



            if (keyboard.upArrowKey.wasPressedThisFrame)
            {
                CheatToggles.enableEsp = !CheatToggles.enableEsp;
                if (!CheatToggles.enableEspGhost && !CheatToggles.enableEspPlayer && !CheatToggles.enableEspBone && !CheatToggles.enableEspOuija && !CheatToggles.enableEspEmf && !CheatToggles.enableEspFuseBox)
                {
                    CheatToggles.enableEsp = true;
                }
                if (CheatToggles.enableEsp)
                {
                    CheatToggles.enableEspGhost = true;
                    CheatToggles.enableEspPlayer = true;
                    CheatToggles.enableEspBone = true;
                    CheatToggles.enableEspOuija = true;
                    CheatToggles.enableEspEmf = true;
                    CheatToggles.enableEspFuseBox = true;
                }
                else
                {
                    CheatToggles.enableEspGhost = false;
                    CheatToggles.enableEspPlayer = false;
                    CheatToggles.enableEspBone = false;
                    CheatToggles.enableEspOuija = false;
                    CheatToggles.enableEspEmf = false;
                    CheatToggles.enableEspFuseBox = false;
                }
                Debug.Msg("ESP: Toggled " + (CheatToggles.enableEsp ? "On" : "Off"), 1);
            }

            if (keyboard.downArrowKey.wasPressedThisFrame)
            {
                CheatToggles.enableFullbright = !CheatToggles.enableFullbright;
                Debug.Msg("Fullbright: Toggled " + (CheatToggles.enableFullbright ? "On" : "Off"), 1);
                if (CheatToggles.enableFullbright == true)
                {
                    Fullbright.Enable();
                }
                else
                {
                    Fullbright.Disable();
                }
            }

            if (keyboard.insertKey.wasPressedThisFrame || keyboard.deleteKey.wasPressedThisFrame || keyboard.rightCtrlKey.wasPressedThisFrame || keyboard.rightArrowKey.wasPressedThisFrame)
            {
                CheatToggles.guiEnabled = !CheatToggles.guiEnabled;
                Debug.Msg("GUI: Toggled " + (CheatToggles.guiEnabled ? "On" : "Off"), 1);

                if (CheatToggles.guiEnabled)
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
            if (keyboard.hKey.wasPressedThisFrame && !CheatToggles.guiEnabled && CheatToggles.enableHotkeys)
            {
                Trolling.Hunt();
            }

            if (keyboard.iKey.wasPressedThisFrame && !CheatToggles.guiEnabled && CheatToggles.enableHotkeys)
            {
                Trolling.Interact();
            }

            if (keyboard.oKey.wasPressedThisFrame && !CheatToggles.guiEnabled && CheatToggles.enableHotkeys)
            {
                Trolling.Appear();
            }

            if (keyboard.pKey.wasPressedThisFrame && !CheatToggles.guiEnabled && CheatToggles.enableHotkeys)
            {
                Trolling.Idle();
            }
            if (keyboard.uKey.wasPressedThisFrame && !CheatToggles.guiEnabled && CheatToggles.enableHotkeys)
            {
                Trolling.LockDoors(3);
            }
            if (keyboard.lKey.wasPressedThisFrame && !CheatToggles.guiEnabled && CheatToggles.enableHotkeys)
            {
                Trolling.LockDoors(1);
            }
        }
        public override void OnGUI()
        {
            if (CheatToggles.guiEnabled)
            {
                if (initializedScene > 1)
                {
                    if (GUI.Toggle(new Rect(500f, 2f, 150f, 20f), CheatToggles.guiGhost, "Ghost GUI") != CheatToggles.guiGhost)
                    {
                        CheatToggles.guiGhost = !CheatToggles.guiGhost;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiGhost == true)
                    {
                        if (GUI.Toggle(new Rect(650f, 2f, 150f, 20f), CheatToggles.enableEspGhost, "Ghost ESP") != CheatToggles.enableEspGhost)
                        {
                            CheatToggles.enableEspGhost = !CheatToggles.enableEspGhost;
                            Debug.Msg("ESP: Toggled " + (CheatToggles.enableEspGhost ? "On" : "Off"), 1);

                        }
                        if (GUI.Toggle(new Rect(650f, 22f, 150f, 20f), CheatToggles.guiGhostTroll, "Troll Options") != CheatToggles.guiGhostTroll)
                        {
                            CheatToggles.guiGhostTroll = !CheatToggles.guiGhostTroll;
                        }
                        if (CheatToggles.guiGhostTroll == true)
                        {
                            if (GUI.Button(new Rect(800f, 2f, 150f, 20f), "Hunt") && levelController != null)
                            {
                                Trolling.Hunt();
                            }
                            if (GUI.Button(new Rect(800f, 22f, 150f, 20f), "Idle") && levelController != null)
                            {
                                Trolling.Idle();
                            }
                            if (GUI.Button(new Rect(800f, 42f, 150f, 20f), "Appear") && levelController != null)
                            {
                                Trolling.Appear();
                            }
                            if (GUI.Button(new Rect(800f, 62f, 150f, 20f), "Unappear") && levelController != null)
                            {
                                Trolling.UnAppear();
                            }
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 22f, 150f, 20f), CheatToggles.guiESP, "ESP GUI") != CheatToggles.guiESP)
                    {
                        CheatToggles.guiESP = !CheatToggles.guiESP;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiESP == true)
                    {
                        if (GUI.Toggle(new Rect(650f, 2f, 150f, 20f), CheatToggles.enableEspGhost, "Ghost ESP") != CheatToggles.enableEspGhost)
                        {
                            CheatToggles.enableEspGhost = !CheatToggles.enableEspGhost;
                            Debug.Msg("Ghost ESP: Toggled " + (CheatToggles.enableEspGhost ? "On" : "Off"), 1);

                        }
                        if (GUI.Toggle(new Rect(650f, 22f, 150f, 20f), CheatToggles.enableEspPlayer, "Player ESP") != CheatToggles.enableEspPlayer)
                        {
                            CheatToggles.enableEspPlayer = !CheatToggles.enableEspPlayer;
                            Debug.Msg("Player ESP: Toggled " + (CheatToggles.enableEspPlayer ? "On" : "Off"), 1);

                        }
                        if (GUI.Toggle(new Rect(650f, 42f, 150f, 20f), CheatToggles.enableEspBone, "Bone ESP") != CheatToggles.enableEspBone)
                        {
                            CheatToggles.enableEspBone = !CheatToggles.enableEspBone;
                            Debug.Msg("Bone ESP: Toggled " + (CheatToggles.enableEspBone ? "On" : "Off"), 1);

                        }
                        if (GUI.Toggle(new Rect(650f, 62f, 150f, 20f), CheatToggles.enableEspOuija, "Ouija ESP") != CheatToggles.enableEspOuija)
                        {
                            CheatToggles.enableEspOuija = !CheatToggles.enableEspOuija;
                            Debug.Msg("Ouija ESP: Toggled " + (CheatToggles.enableEspOuija ? "On" : "Off"), 1);

                        }
                        if (GUI.Toggle(new Rect(650f, 82f, 150f, 20f), CheatToggles.enableEspFuseBox, "FuseBox ESP") != CheatToggles.enableEspFuseBox)
                        {
                            CheatToggles.enableEspFuseBox = !CheatToggles.enableEspFuseBox;
                            Debug.Msg("FuseBox ESP: Toggled " + (CheatToggles.enableEspFuseBox ? "On" : "Off"), 1);
                        }
                        if (GUI.Toggle(new Rect(650f, 102f, 150f, 20f), CheatToggles.enableEspEmf, "Emf ESP") != CheatToggles.enableEspEmf)
                        {
                            CheatToggles.enableEspEmf = !CheatToggles.enableEspEmf;
                            Debug.Msg("Emf ESP: Toggled " + (CheatToggles.enableEspEmf ? "On" : "Off"), 1);
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 42f, 150f, 20f), CheatToggles.guiHelper, "Helper GUI") != CheatToggles.guiHelper)
                    {
                        CheatToggles.guiHelper = !CheatToggles.guiHelper;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiHelper == true)
                    {
                        if (GUI.Toggle(new Rect(650f, 2f, 150f, 20f), CheatToggles.guiHelperInfo, "Basic Info") != CheatToggles.guiHelperInfo)
                        {
                            CheatToggles.guiHelperInfo = !CheatToggles.guiHelperInfo;
                        }
                        if (CheatToggles.guiHelperInfo == true)
                        {
                            if (GUI.Toggle(new Rect(800f, 2f, 150f, 20f), CheatToggles.enableBIGhost, "Ghost Info") != CheatToggles.enableBIGhost)
                            {
                                CheatToggles.enableBIGhost = !CheatToggles.enableBIGhost;
                                Debug.Msg("Ghost Info: Toggled " + (CheatToggles.enableBIGhost ? "On" : "Off"), 1);
                            }
                            if (GUI.Toggle(new Rect(800f, 22f, 150f, 20f), CheatToggles.enableBIMissions, "Missions Info") != CheatToggles.enableBIMissions)
                            {
                                CheatToggles.enableBIMissions = !CheatToggles.enableBIMissions;
                                Debug.Msg("Missions Info: Toggled " + (CheatToggles.enableBIMissions ? "On" : "Off"), 1);
                            }
                            if (GUI.Toggle(new Rect(800f, 42f, 150f, 20f), CheatToggles.enableBIPlayer, "Player Info") != CheatToggles.enableBIPlayer)
                            {
                                CheatToggles.enableBIPlayer = !CheatToggles.enableBIPlayer;
                                Debug.Msg("Player Info: Toggled " + (CheatToggles.enableBIPlayer ? "On" : "Off"), 1);
                            }
                        }
                        if (GUI.Toggle(new Rect(650f, 22f, 150f, 20f), CheatToggles.enableFullbright, "Enable Fullbright") != CheatToggles.enableFullbright)
                        {
                            CheatToggles.enableFullbright = !CheatToggles.enableFullbright;
                            Debug.Msg("Fullbright: Toggled " + (CheatToggles.enableFullbright ? "On" : "Off"), 1);
                            if (CheatToggles.enableFullbright)
                            {
                                Fullbright.Enable();
                            }
                            else
                            {
                                Fullbright.Disable();
                            }
                        }
                        if (GUI.Toggle(new Rect(650f, 42f, 150f, 20f), CheatToggles.enableHotkeys, "Enable Troll Hotkeys") != CheatToggles.enableHotkeys)
                        {
                            CheatToggles.enableHotkeys = !CheatToggles.enableHotkeys;
                            Debug.Msg("Troll Hotkeys: Toggled " + (CheatToggles.enableHotkeys ? "On" : "Off"), 1);
                            MelonPrefs.SetBool("Settings", "HotkeysEnabled", CheatToggles.enableHotkeys);
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 62f, 150f, 20f), CheatToggles.guiTroll, "Troll GUI") != CheatToggles.guiTroll)
                    {
                        CheatToggles.guiTroll = !CheatToggles.guiTroll;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiTroll == true)
                    {
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
                        if (GUI.Button(new Rect(650f, 82f, 150f, 20f), "Door knock") && levelController != null)
                        {
                            Trolling.EventDoorKnock();
                        }
                        if (GUI.Button(new Rect(650f, 102f, 150f, 20f), "Random Event") && levelController != null)
                        {
                            Trolling.Interact();
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 82f, 150f, 20f), CheatToggles.guiDebug, "Debug GUI") != CheatToggles.guiDebug)
                    {
                        CheatToggles.guiDebug = !CheatToggles.guiDebug;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiTest = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiDebug == true)
                    {
                        if (GUI.Toggle(new Rect(650f, 2f, 150f, 20f), CheatToggles.enableDebug, "Enable Debug") != CheatToggles.enableDebug)
                        {
                            CheatToggles.enableDebug = !CheatToggles.enableDebug;
                            Debug.Msg("Debug: Toggled " + (CheatToggles.enableDebug ? "On" : "Off"), 1);
                        }
                        if (GUI.Toggle(new Rect(650f, 22f, 150f, 20f), Debug.debugMode1, "Debug Mode 1") != Debug.debugMode1)
                        {
                            Debug.debugMode1 = !Debug.debugMode1;
                            Debug.Msg("Debug Mode 1: Toggled " + (Debug.debugMode1 ? "On" : "Off"), 1);
                        }
                        if (GUI.Toggle(new Rect(650f, 42f, 150f, 20f), Debug.debugMode2, "Debug Mode 2") != Debug.debugMode2)
                        {
                            Debug.debugMode2 = !Debug.debugMode2;
                            Debug.Msg("Debug Mode 2: Toggled " + (Debug.debugMode2 ? "On" : "Off"), 1);
                        }
                        if (GUI.Toggle(new Rect(650f, 62f, 150f, 20f), Debug.debugMode3, "Debug Mode 3") != Debug.debugMode3)
                        {
                            Debug.debugMode3 = !Debug.debugMode3;
                            Debug.Msg("Debug Mode 3: Toggled " + (Debug.debugMode3 ? "On" : "Off"), 1);
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 102f, 150f, 20f), CheatToggles.guiTest, "New Features") != CheatToggles.guiTest)
                    {
                        CheatToggles.guiTest = !CheatToggles.guiTest;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiFeatureCollection = false;
                    }
                    if (CheatToggles.guiTest == true)
                    {
                        if (GUI.Button(new Rect(650f, 2f, 150f, 20f), "Random Light Use") && levelController != null)
                        {
                            LightSwitch lightSwitchr = lightSwitches[new System.Random().Next(0, lightSwitches.Count)];
                            if (lightSwitchr != null)
                            {
                                lightSwitchr.UseLight();
                            }
                            Debug.Msg("Random Light Use", 1);
                        }
                        if (GUI.Button(new Rect(650f, 42f, 150f, 20f), "All Lights Off") && levelController != null)
                        {
                            foreach (LightSwitch lightSwitchaoff in lightSwitches)
                            {
                                lightSwitchaoff.TurnOff();
                                lightSwitchaoff.TurnOffNetworked(true);
                            }
                            Debug.Msg("All Lights Off", 1);
                        }
                        if (GUI.Button(new Rect(650f, 62f, 150f, 20f), "Blinking Lights") && levelController != null)
                        {
                            lightSwitchToggle = !lightSwitchToggle;
                            Debug.Msg("Blinking Lights", 1);
                        }
                        if (GUI.Button(new Rect(650f, 82f, 150f, 20f), "Disable All Features") && levelController != null)
                        {
                            DisableAll();
                            Debug.Msg("Disable All", 1);
                        }
                    }
                    if (GUI.Toggle(new Rect(500f, 122f, 150f, 20f), CheatToggles.guiFeatureCollection, "Feature Coll. GUI") != CheatToggles.guiFeatureCollection)
                    {
                        CheatToggles.guiFeatureCollection = !CheatToggles.guiFeatureCollection;
                        CheatToggles.guiGhost = false;
                        CheatToggles.guiGhostTroll = false;
                        CheatToggles.guiESP = false;
                        CheatToggles.guiHelper = false;
                        CheatToggles.guiHelperInfo = false;
                        CheatToggles.guiTroll = false;
                        CheatToggles.guiDebug = false;
                        CheatToggles.guiTest = false;
                    }
                    if (CheatToggles.guiFeatureCollection == true)
                    {
                        if (GUI.Button(new Rect(650f, 2f, 150f, 20f), "Hunt") && levelController != null)
                        {
                            Trolling.Hunt();
                        }
                        if (GUI.Button(new Rect(650f, 22f, 150f, 20f), "Idle") && levelController != null)
                        {
                            Trolling.Idle();
                        }
                        if (GUI.Button(new Rect(650f, 42f, 150f, 20f), "Appear") && levelController != null)
                        {
                            Trolling.Appear();
                        }
                        if (GUI.Button(new Rect(650f, 62f, 150f, 20f), "Unappear") && levelController != null)
                        {
                            Trolling.UnAppear();
                        }
                        if (GUI.Button(new Rect(800f, 2f, 150f, 20f), "Lock Exit Doors") && levelController != null)
                        {
                            Trolling.LockDoors(1);
                        }
                        if (GUI.Button(new Rect(800f, 22f, 150f, 20f), "Lock All Doors") && levelController != null)
                        {
                            Trolling.LockDoors(2);
                        }
                        if (GUI.Button(new Rect(800f, 42f, 150f, 20f), "Unlock Exit Doors") && levelController != null)
                        {
                            Trolling.LockDoors(3);
                        }
                        if (GUI.Button(new Rect(800f, 62f, 150f, 20f), "Unlock All Doors") && levelController != null)
                        {
                            Trolling.LockDoors(4);
                        }
                        if (GUI.Button(new Rect(800f, 82f, 150f, 20f), "Door knock") && levelController != null)
                        {
                            Trolling.EventDoorKnock();
                        }
                        if (GUI.Button(new Rect(800f, 102f, 150f, 20f), "Random Event") && levelController != null)
                        {
                            Trolling.Interact();
                        }
                        if (GUI.Button(new Rect(950f, 2f, 150f, 20f), "Random Light Use") && levelController != null)
                        {
                            LightSwitch lightSwitchr = lightSwitches[new System.Random().Next(0, lightSwitches.Count)];
                            if (lightSwitchr != null)
                            {
                                lightSwitchr.UseLight();
                            }
                            Debug.Msg("Random Light Use", 1);
                        }
                        if (GUI.Button(new Rect(950f, 42f, 150f, 20f), "All Lights Off") && levelController != null)
                        {
                            foreach (LightSwitch lightSwitchaoff in lightSwitches)
                            {
                                lightSwitchaoff.TurnOff();
                                lightSwitchaoff.TurnOffNetworked(true);
                            }
                            Debug.Msg("All Lights Off", 1);
                        }
                        if (GUI.Button(new Rect(950f, 62f, 150f, 20f), "Blinking Lights") && levelController != null)
                        {
                            lightSwitchToggle = !lightSwitchToggle;
                            Debug.Msg("Blinking Lights", 1);
                        }
                    }
                }
                else
                {
                    if (initializedScene == 1 && playerStatsManager != null)
                    {
                        if (GUI.Button(new Rect(500f, 2f, 150f, 20f), "+ 1.000$") && levelController == null)
                        {
                            var money = FileBasedPrefs.GetInt("PlayersMoney", 0);
                            Debug.Msg("Money set from " + money + " to " + (money + 1000), 1);
                            FileBasedPrefs.SetInt("PlayersMoney", money + 1000);
                            playerStatsManager.UpdateMoney();
                        }
                        if (GUI.Button(new Rect(500f, 22f, 150f, 20f), "- 1.000$") && levelController == null)
                        {
                            var money = FileBasedPrefs.GetInt("PlayersMoney", 0);
                            Debug.Msg("Money set from " + money + " to " + (money - 1000), 1);
                            FileBasedPrefs.SetInt("PlayersMoney", money - 1000);
                            playerStatsManager.UpdateMoney();
                        }
                        if (GUI.Button(new Rect(500f, 42f, 150f, 20f), "+ 100XP") && levelController == null)
                        {
                            FileBasedPrefs.SetInt("myTotalExp", FileBasedPrefs.GetInt("myTotalExp", 0) + 100);
                            playerStatsManager.UpdateExperience();
                            playerStatsManager.UpdateLevel();
                            Debug.Msg("XP: +100", 1);
                        }
                        if (GUI.Button(new Rect(500f, 62f, 150f, 20f), "- 1.000XP") && levelController == null)
                        {
                            FileBasedPrefs.SetInt("myTotalExp", FileBasedPrefs.GetInt("myTotalExp", 0) - 1000);
                            playerStatsManager.UpdateExperience();
                            playerStatsManager.UpdateLevel();
                            Debug.Msg("XP: -1.000", 1);
                        }
                        if (GUI.Toggle(new Rect(500f, 82f, 150f, 20f), CheatToggles.guiDebug, "Debug GUI") != CheatToggles.guiDebug)
                        {
                            CheatToggles.guiDebug = !CheatToggles.guiDebug;
                            CheatToggles.guiGhost = false;
                            CheatToggles.guiGhostTroll = false;
                            CheatToggles.guiESP = false;
                            CheatToggles.guiHelper = false;
                            CheatToggles.guiHelperInfo = false;
                            CheatToggles.guiTroll = false;
                            CheatToggles.guiTest = false;
                            CheatToggles.guiFeatureCollection = false;
                        }
                        if (CheatToggles.guiDebug == true)
                        {
                            if (GUI.Toggle(new Rect(550f, 102f, 150f, 20f), CheatToggles.enableDebug, "Enable Debug") != CheatToggles.enableDebug)
                            {
                                CheatToggles.enableDebug = !CheatToggles.enableDebug;
                                Debug.Msg("Debug: Toggled " + (CheatToggles.enableDebug ? "On" : "Off"), 1);
                            }
                            if (GUI.Toggle(new Rect(550f, 122f, 150f, 20f), Debug.debugMode1, "Debug Mode 1") != Debug.debugMode1)
                            {
                                Debug.debugMode1 = !Debug.debugMode1;
                                Debug.Msg("Debug Mode 1: Toggled " + (Debug.debugMode1 ? "On" : "Off"), 1);
                            }
                            if (GUI.Toggle(new Rect(550f, 142f, 150f, 20f), Debug.debugMode2, "Debug Mode 2") != Debug.debugMode2)
                            {
                                Debug.debugMode2 = !Debug.debugMode2;
                                Debug.Msg("Debug Mode 2: Toggled " + (Debug.debugMode2 ? "On" : "Off"), 1);
                            }
                            if (GUI.Toggle(new Rect(550f, 162f, 150f, 20f), Debug.debugMode3, "Debug Mode 3") != Debug.debugMode3)
                            {
                                Debug.debugMode3 = !Debug.debugMode3;
                                Debug.Msg("Debug Mode 3: Toggled " + (Debug.debugMode3 ? "On" : "Off"), 1);
                            }
                        }

                        GUI.SetNextControlName("changeName");
                        playerName = GUI.TextArea(new Rect(650f, 2f, 150f, 20f), playerName);
                        if (GUI.Button(new Rect(650f, 22f, 150f, 20f), "Change Name"))
                        {
                            GUI.FocusControl("changeName");
                            PhotonNetwork.NickName = playerName;
                            Debug.Msg("Set name: " + playerName, 1);
                        }
                    }
                }
            }

            if (CheatToggles.enableEspGhost || CheatToggles.enableEspPlayer || CheatToggles.enableEspBone || CheatToggles.enableEspOuija || CheatToggles.enableEspEmf || CheatToggles.enableEspFuseBox)
            {
                ESP.Enable();
            }

            if (CheatToggles.enableBIGhost || CheatToggles.enableBIMissions || CheatToggles.enableBIPlayer)
            {
                GUI.Label(new Rect(0f, 0f, 500f, 160f), "", "box");
            }
            if (CheatToggles.enableBIGhost)
            {
                BasicInformations.EnableGhost();
                GUI.Label(new Rect(10f, 2f, 300f, 50f), "<color=#00FF00><b>Ghost Name:</b> " + (ghostNameAge ?? "") + "</color>");
                GUI.Label(new Rect(10f, 17f, 300f, 50f), "<color=#00FF00><b>Ghost Type:</b> " + (ghostType ?? "") + "</color>");
                GUI.Label(new Rect(10f, 47f, 400f, 50f), "<color=#00FF00><b>Evidence:</b> " + (evidence ?? "") + "</color>");
                GUI.Label(new Rect(10f, 32f, 300f, 50f), "<color=#00FF00><b>Ghost State:</b> " + (ghostState ?? "") + "</color>");
                GUI.Label(new Rect(10f, 62f, 400f, 50f), "<color=#00FF00><b>Responds to:</b> " + (ghostIsShy ?? "") + "</color>");
            }
            else
            {
                if (initializedScene > 1)
                {
                    Debug.Msg("BasicInformations.DisableGhost", 3);
                    BasicInformations.DisableGhost();
                }
            }
            if (CheatToggles.enableBIMissions)
            {
                BasicInformations.EnableMissions();
            }
            if (CheatToggles.enableBIPlayer)
            {
                BasicInformations.EnablePlayer();
                GUI.Label(new Rect(10f, 77f, 300f, 50f), "<color=#00FF00><b>My Sanity:</b> " + (myPlayerSanity ?? "N/A") + "</color>");
            }
            if (lightSwitchToggle && !lightSwitchRunning)
            {
                Debug.Msg("lightSwitchRunningTrue", 3);
                lightSwitchRunning = true;
                blinkingLightsRoutine = MelonCoroutines.Start(BlinkingLights());
            }
        }

        IEnumerator BlinkingLights()
        {
            if (lightSwitchRunning)
            {
                foreach (LightSwitch lightSwitchsrlb in lightSwitches)
                {
                    Debug.Msg("FlickerNetworked", 3);
                    lightSwitchsrlb.field_Public_PhotonView_0.RPC("FlickerNetworked", 0, Trolling.getRPCObject(0, false));
                    yield return new WaitForSeconds(0.25f);

                }
                Debug.Msg("FlickerNetworked end", 3);
                yield return new WaitForSeconds(0.15f);
                yield return new WaitForSeconds(0.10f);
                lightSwitchRunning = false;
            }
            yield return null;
        }

        private void DisableAll()
        {
            Debug.Msg("DisableAll", 3);
            CheatToggles.enableBI = false;
            CheatToggles.enableBIGhost = false;
            CheatToggles.enableBIMissions = false;
            CheatToggles.enableBIPlayer = false;

            CheatToggles.enableEsp = false;
            CheatToggles.enableEspGhost = false;
            CheatToggles.enableEspPlayer = false;
            CheatToggles.enableEspBone = false;
            CheatToggles.enableEspOuija = false;
            CheatToggles.enableEspEmf = false;
            CheatToggles.enableEspFuseBox = false;

            CheatToggles.enableFullbright = false;
            Fullbright.Disable();

            BasicInformations.DisableGhost();
        }

        private Player GetLocalPlayer()
        {
            Debug.Msg("GetLocalPlayer", 3);
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

        private void HandleConfig()
        {
            MelonPrefs.RegisterCategory("Settings", "Settings");
            Debug.Msg("Create Category: Settings", 2);

            MelonPrefs.RegisterBool("Settings", "HotkeysEnabled", true, "Hotkeys Enabled");
            Debug.Msg("Create Entry: HotkeysEnabled", 2);

            MelonPrefs.RegisterBool("Settings", "DebugEnabled", true, "Debug Enabled");
            Debug.Msg("Create Entry: DebugEnabled", 2);

            MelonPrefs.RegisterBool("Settings", "DebugM1Enabled", true, "Debug M1 Enabled");
            Debug.Msg("Create Entry: Debug1Enabled", 2);

            MelonPrefs.RegisterBool("Settings", "DebugM2Enabled", true, "Debug M2 Enabled");
            Debug.Msg("Create Entry: Debug2Enabled", 2);

            MelonPrefs.RegisterBool("Settings", "DebugM3Enabled", false, "Debug M3 Enabled");
            Debug.Msg("Create Entry: Debug3Enabled", 2);

            CheatToggles.enableHotkeys = MelonPrefs.GetBool("Settings", "HotkeysEnabled");
            CheatToggles.enableDebug = MelonPrefs.GetBool("Settings", "DebugEnabled");

            Debug.debugMode1 = MelonPrefs.GetBool("Settings", "DebugM1Enabled");
            Debug.debugMode2 = MelonPrefs.GetBool("Settings", "DebugM2Enabled");
            Debug.debugMode3 = MelonPrefs.GetBool("Settings", "DebugM3Enabled");

            MelonPrefs.SaveConfig();
        }

        IEnumerator CollectGameObjects()
        {
            try {
                Debug.Msg("isRunningTrue", 3);
                isRunning = true;
                Debug.Msg("cameraMain", 3);
                cameraMain = Camera.main ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("dnaEvidences", 3);
                dnaEvidences = Object.FindObjectsOfType<DNAEvidence>().ToList<DNAEvidence>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("doors", 3);
                doors = Object.FindObjectsOfType<Door>().ToList<Door>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("fuseBox", 3);
                fuseBox = Object.FindObjectOfType<FuseBox>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("gameController", 3);
                gameController = Object.FindObjectOfType<GameController>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ghostAI", 3);
                ghostAI = Object.FindObjectOfType<GhostAI>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ghostAIs", 3);
                ghostAIs = Object.FindObjectsOfType<GhostAI>().ToList<GhostAI>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ghostActivity", 3);
                ghostActivity = Object.FindObjectOfType<GhostActivity>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ghostInfo", 3);
                ghostInfo = Object.FindObjectOfType<GhostInfo>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("levelController", 3);
                levelController = Object.FindObjectOfType<LevelController>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("lightSwitch", 3);
                lightSwitch = Object.FindObjectOfType<LightSwitch>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("lightSwitches", 3);
                lightSwitches = Object.FindObjectsOfType<LightSwitch>().ToList<LightSwitch>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("soundController", 3);
                soundController = Object.FindObjectOfType<SoundController>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ouijaBoards", 3);
                ouijaBoards = Object.FindObjectsOfType<OuijaBoard>().ToList<OuijaBoard>() ?? null;
                yield return new WaitForSeconds(0.15f);

                Debug.Msg("ouijaBoards", 3);
                windows = Object.FindObjectsOfType<Window>().ToList<Window>() ?? null;
                yield return new WaitForSeconds(0.15f);

                if (Object.FindObjectOfType<Player>() != null)
                {
                    Debug.Msg("player", 3);
                    player = Object.FindObjectOfType<Player>() ?? null;
                    yield return new WaitForSeconds(0.15f);

                    Debug.Msg("players", 3);
                    players = Object.FindObjectsOfType<Player>().ToList<Player>() ?? null;
                    yield return new WaitForSeconds(0.15f);

                    Debug.Msg("playerStatsManager", 3);
                    playerStatsManager = Object.FindObjectOfType<PlayerStatsManager>() ?? null;
                    yield return new WaitForSeconds(0.15f);

                    Debug.Msg("myPlayer", 3);
                    myPlayer = GetLocalPlayer() ?? player;
                    yield return new WaitForSeconds(0.15f);

                    Debug.Msg("playerAnim", 3);
                    playerAnim = myPlayer.field_Public_Animator_0 ?? null;
                    yield return new WaitForSeconds(0.15f);

                    if (playerAnim != null)
                    {
                        Debug.Msg("boneTransform", 3);
                        boneTransform = playerAnim.GetBoneTransform(HumanBodyBones.Head) ?? null;
                        yield return new WaitForSeconds(0.15f);
                    }
                }

                if (levelController != null)
                {
                    Debug.Msg("emf", 3);
                    emf = Object.FindObjectsOfType<EMF>().ToList<EMF>() ?? null;
                    yield return new WaitForSeconds(0.15f);
                }

                isRunning = false;
                yield return new WaitForSeconds(0.15f);
                Debug.Msg("-----------------------------", 3);

                yield return null;
            } finally {
                if(isRunning) {
                    Debug.Msg("Unexpected Error while collecting game objects.");
                    isRunning = false;
                }
            }
        }

        public static Transform boneTransform;
        public static Camera cameraMain;
        public static List<DNAEvidence> dnaEvidences;
        public static List<Door> doors;
        public static GameController gameController;
        public static GhostAI ghostAI;
        public static List<GhostAI> ghostAIs;
        public static List<EMF> emf;
        public static EMFData emfData;
        public static FuseBox fuseBox;
        public static GhostActivity ghostActivity;
        public static GhostInteraction ghostInteraction;
        public static GhostController ghostController;
        public static GhostEventPlayer ghostEventPlayer;
        public static GhostInfo ghostInfo;
        public static List<InventoryItem> items;
        public static LevelController levelController;
        public static Light light;
        public static LightSwitch lightSwitch;
        public static List<LightSwitch> lightSwitches;
        public static Player myPlayer;
        public static List<OuijaBoard> ouijaBoards;
        public static PhotonView photonView;
        public static Player player;
        public static List<Player> players;
        public static LobbyManager lobbyManager;
        public static Animator playerAnim;
        public static PlayerStatsManager playerStatsManager;
        public static ServerManager serverManager;
        public static SoundController soundController;
        public static List<Window> windows;
        public static String ghostNameAge;
        public static String ghostType;
        public static String evidence;
        public static String ghostState;
        public static String ghostIsShy;
        public static String myPlayerSanity;
        public static String[] mapNames = { "Opening Scene", "Lobby", "Tanglewood Street", "Ridgeview Road House", "Edgefield Street House", "Asylum", "Brownstone High School", "Bleasdale Farmhouse", "Grafton Farmhouse", "Prison" };
        public static String inSight = "";
        public static bool settingsExist = false;
        public static int initializedScene;
        private bool lightSwitchToggle = false;
        private bool lightSwitchRunning = false;
        private static bool gameStarted = false;
        private static object coRoutine;
        private static object blinkingLightsRoutine;
        private static bool canRun = true;
        private static bool isRunning = false;
        private static String playerName;

        public static ChallengesManager challengesManager;
        public static int test = 0;
    }
}
