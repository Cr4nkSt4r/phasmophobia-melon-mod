using System;
using UnityEngine;

namespace PhasmoMelonMod
{
    class BasicInformations
    {
        public static void Enable()
        {
            if (Main.levelController != null && Main.ghostInfo != null && firstRun)
            {
                Main.ghostNameAge = Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_String_0 + " - " + Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_Int32_0.ToString();
                Main.ghostType = Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique_0.ToString();
                Main.ghostIsShy = (Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_Boolean_1 ? "People that are alone" : "Everyone");

                switch (Main.ghostInfo.field_Public_ValueTypePublicSealedObInBoStInBoInInInUnique_0.field_Public_EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique_0)
                {
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Spirit:
                        Main.evidence = "Spirit Box | Fingerprints | Ghost Writing";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Shade:
                        Main.evidence = "EMF 5 | Ghost Orb | Ghost Writing";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Poltergeist:
                        Main.evidence = "Spirit Box | Fingerprints | Ghost Orb";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Jinn:
                        Main.evidence = "EMF 5 | Spirit Box | Ghost Orb";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Mare:
                        Main.evidence = "Spirit Box | Ghost Orb | Freezing Temp.";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Phantom:
                        Main.evidence = "EMF 5 | Ghost Orb | Freezing Temp.";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Wraith:
                        Main.evidence = "Spirit Box | Fingerprints | Freezing Temp.";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Banshee:
                        Main.evidence = "EMF 5 | Fingerprints | Freezing Temp.";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Revenant:
                        Main.evidence = "EMF 5 | Fingerprints | Ghost Writing";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Yurei:
                        Main.evidence = "Ghost Orb | Ghost Writing | Freezing Temp.";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Oni:
                        Main.evidence = "EMF 5 | Spirit Box | Ghost Writing";
                        break;
                    case ValueTypePublicSealedObInBoStInBoInInInUnique.EnumNPublicSealedvanoSpWrPhPoBaJiMaReUnique.Demon:
                        Main.evidence = "Spirit Box | Ghost Writing | Freezing Temp.";
                        break;
                    default:
                        break;
                }
                firstRun = false;
            }

            Main.myPlayerSanity = Math.Round(100 - Main.myPlayer.field_Public_Single_0, 0).ToString();

            if (Main.levelController != null && Main.ghostAI != null)
            {
                switch (Main.ghostAI.field_Public_EnumNPublicSealedvaidwahufalidothfuapUnique_0)
                {
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.idle:
                        Main.ghostState = "Idle";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.wander:
                        Main.ghostState = "Wander";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.hunting:
                        Main.ghostState = "Hunting";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.favouriteRoom:
                        Main.ghostState = "Favourite Room";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.light:
                        Main.ghostState = "Light";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.door:
                        Main.ghostState = "Door";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.throwing:
                        Main.ghostState = "Throwing";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.fusebox:
                        Main.ghostState = "Fusebox";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.appear:
                        Main.ghostState = "Appear";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.doorKnock:
                        Main.ghostState = "Knock Door";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.windowKnock:
                        Main.ghostState = "Knock Window";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.carAlarm:
                        Main.ghostState = "Car Alarm";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.radio:
                        Main.ghostState = "Radio";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.flicker:
                        Main.ghostState = "Flicker";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.lockDoor:
                        Main.ghostState = "Lock Door";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.cctv:
                        Main.ghostState = "CCTV";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.randomEvent:
                        Main.ghostState = "Random Event";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.GhostAbility:
                        Main.ghostState = "Ghost Ability";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.killPlayer:
                        Main.ghostState = "Kill Player";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.sink:
                        Main.ghostState = "Sink";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.sound:
                        Main.ghostState = "Sound";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.painting:
                        Main.ghostState = "Painting";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.mannequin:
                        Main.ghostState = "Mennequin";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.teleportObject:
                        Main.ghostState = "Teleport Object";
                        break;
                    case GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.animationObject:
                        Main.ghostState = "Animate Object";
                        break;
                    default:
                        Main.ghostState = "Idle";
                        break;
                }
            }

            if (Main.levelController != null && MissionManager.field_Public_Static_MissionManager_0.field_Public_List_1_Mission_0 != null)
            {
                int missionNum = 1;
                foreach (Mission mission in MissionManager.field_Public_Static_MissionManager_0.field_Public_List_1_Mission_0)
                {
                    GUI.Label(new Rect(10f, 80f + (float)missionNum * 15f, 600f, 50f), string.Concat(new object[]
                    {
                        ((mission.field_Public_Boolean_0) ? "<color=#CCCCCC>" : "<color=#00FF00>"),
                        "<b>" + missionNum + "</b>",
                        ") ",
                        mission.field_Public_String_0,
                        "</color>"
                    }));
                    missionNum++;
                }
            }
        }
        public static void Reset()
        {
            Main.ghostNameAge = "N/A";
            Main.ghostType = "N/A";
            Main.ghostIsShy = "N/A";
            Main.evidence = "N/A";
            firstRun = true;
        }

        public static bool firstRun = true;
    }
}
