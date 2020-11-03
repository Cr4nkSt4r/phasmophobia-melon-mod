using MelonLoader;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using Object = Il2CppSystem.Object;
using Boolean = Il2CppSystem.Boolean;
using Int32 = Il2CppSystem.Int32;

namespace PhasmoMelonMod
{
    class Trolling
    {
        public static void Hunt()
        {
            MelonLogger.Log("[+] Troll->Hunt: Triggered ");
            Main.ghostAI.field_Public_Boolean_0 = true;
            Main.ghostAI.field_Public_Boolean_1 = true;
            Main.ghostAI.field_Public_Boolean_2 = true;
            Main.ghostAI.field_Public_Boolean_3 = true;
            Main.ghostAI.field_Public_Boolean_4 = true;
            Main.ghostAI.field_Public_Boolean_5 = true;
            Main.ghostAI.field_Public_Player_0 = null;
            //field_Public_Player_1  //Testing
            //field_Public_Boolean_6  //Testing
            Main.ghostAI.ChasingPlayer(true);
            Main.ghostAI.field_Public_Animator_0.SetBool("isIdle", false);
            Main.ghostAI.field_Public_Animator_0.SetInteger("WalkType", 1);
            Main.ghostAI.field_Public_GhostAudio_0.TurnOnOrOffAppearSource(true);
            Main.ghostAI.field_Public_GhostAudio_0.PlayOrStopAppearSource(true);
            Main.ghostAI.field_Public_NavMeshAgent_0.speed = Main.ghostAI.field_Public_Single_0;
            Main.ghostAI.field_Public_NavMeshAgent_0.SetDestination(getDestination());
            SetupPhaseController.field_Public_Static_SetupPhaseController_0.ForceEnterHuntingPhase();
            Main.ghostAI.ChangeState(GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.hunting, null, null);
            Main.ghostAI.field_Public_GhostInteraction_0.CreateAppearedEMF(Main.ghostAI.transform.position);
            Main.ghostAI.Appear(true);
            Main.photonView.RPC("Hunting", RpcTarget.All, getRPCObject(1, true));
            Main.photonView.RPC("SyncChasingPlayer", RpcTarget.All, getRPCObject(1, true));
        }

        public static void Idle()
        {
            MelonLogger.Log("[+] Troll->Idle: Triggered ");
            Main.ghostAI.field_Public_Boolean_0 = false;
            Main.ghostAI.field_Public_Boolean_1 = false;
            Main.ghostAI.field_Public_Boolean_2 = false;
            Main.ghostAI.field_Public_Boolean_3 = false;
            Main.ghostAI.field_Public_Boolean_4 = false;
            Main.ghostAI.field_Public_Boolean_5 = false;
            Main.ghostAI.field_Public_Player_0 = null;
            Main.ghostAI.field_Public_Animator_0.SetInteger("IdleNumber", Random.Range(0, 2));
            Main.ghostAI.field_Public_Animator_0.SetBool("isIdle", true);
            Main.ghostAI.field_Public_GhostAudio_0.TurnOnOrOffAppearSource(false);
            Main.ghostAI.field_Public_GhostAudio_0.PlayOrStopAppearSource(false);
            Main.ghostAI.ChasingPlayer(false);
            Main.ghostAI.StopGhostFromHunting();
            Main.ghostAI.UnAppear(false);
            Main.ghostAI.ChangeState(GhostAI.EnumNPublicSealedvaidwahufalidothfuapUnique.idle, null, null);
            Main.photonView.RPC("Hunting", RpcTarget.All, getRPCObject(1, false));
            Main.photonView.RPC("SyncChasingPlayer", RpcTarget.All, getRPCObject(1, false));
        }

        public static void Appear()
        {
            MelonLogger.Log("[+] Troll->Appear: Triggered ");
            Main.photonView.RPC("MakeGhostAppear", RpcTarget.All, getRPCObject(2, true, 0, 3));
        }

        public static void UnAppear()
        {
            MelonLogger.Log("[+] Troll->UnAppear: Triggered ");
            Main.photonView.RPC("MakeGhostAppear", RpcTarget.All, getRPCObject(2, false, 0, 3));
        }

        public static void LockDoors(int lockState, int howMany = 0)
        {
            //Door::field_Public_Boolean_0 = locked
            //Door::field_Public_Boolean_1 = closed
            //Door::field_Public_Boolean_2 = canBeGrabbed
            switch (lockState)
            {
                case 1:
                    CloseAllExitDoors();
                    MelonLogger.Log("[+] Troll->CloseAllExitDoors");
                    break;
                case 2:
                    CloseAllRoomDoors();
                    MelonLogger.Log("[+] Troll->CloseAllRoomDoors");
                    break;
                case 3:
                    OpenAllExitDoors();
                    MelonLogger.Log("[+] Troll->OpenAllExitDoors");
                    break;
                case 4:
                    OpenAllRoomDoors();
                    MelonLogger.Log("[+] Troll->OpenAllRoomDoors");
                    break;
                default:
                    break;
            }
        }

        public static void FuseBox()
        {
            MelonLogger.Log("[+] Troll->FuseBox: Triggered ");
            PhotonView photonView = Main.fuseBox.view;
            photonView.RPC("UseNetworked", RpcTarget.All, getRPCObject(1, false));
        }

        public static void CloseAllExitDoors()
        {
            foreach (Door exitDoor in Main.levelController.field_Public_ArrayOf_Door_1)
            {
                CloseLockDoor(exitDoor);
            }
        }
        public static void CloseAllRoomDoors()
        {
            foreach (Door door in Main.doors)
            {
                CloseLockDoor(door);
            }
        }
        public static void OpenAllExitDoors()
        {
            foreach (Door exitDoor in Main.levelController.field_Public_ArrayOf_Door_1)
            {
                OpenUnlockDoor(exitDoor);
            }
        }
        public static void OpenAllRoomDoors()
        {
            foreach (Door door in Main.doors)
            {
                OpenUnlockDoor(door);
            }
        }

        private static void CloseLockDoor(Door door)
        {
            PhotonView photonView = door.field_Public_PhotonView_0;
            door.DisableOrEnableDoor(false);
            door.LockDoor();
            door.transform.localRotation = Quaternion.identity;
            Quaternion localRotation = door.transform.localRotation;
            Vector3 eulerAngles = localRotation.eulerAngles;
            eulerAngles.y = door.field_Public_Single_0;
            localRotation.eulerAngles = eulerAngles;
            door.transform.localRotation = localRotation;
            photonView.RPC("SyncLockState", RpcTarget.All, getRPCObject(1, true));
            photonView.RPC("NetworkedPlayLockSound", RpcTarget.All, getRPCObject(0, false));
        }

        private static void OpenUnlockDoor(Door door)
        {
            PhotonView photonView = door.field_Public_PhotonView_0;
            door.DisableOrEnableDoor(true);
            door.DisableOrEnableCollider(true);
            door.UnlockDoor();
            photonView.RPC("SyncLockState", RpcTarget.All, getRPCObject(1, false));
        }

        public static void Interact()
        {
            MelonLogger.Log("[+] Troll->Interact: Toggled ");
            Main.ghostActivity.Interact();
        }

        private static Vector3 getDestination()
        {
            Debug.Out("getDestination");
            Vector3 destination = Vector3.zero;
            float num = Random.Range(2f, 15f);
            NavMeshHit navMeshHit;
            if (NavMesh.SamplePosition(Random.insideUnitSphere * num + Main.ghostAI.transform.position, out navMeshHit, num, 1))
            {
                destination = navMeshHit.position;
            }
            else
            {
                destination = Vector3.zero;
            }

            return destination;
        }

        private static Object[] getRPCObject(int i, bool isTrue = true, int rangeMin = 0, int rangeMax = 0, bool rangeFirst = false)
        {
            Debug.Out("getRPCObject");
            Object[] obj = new Object[i];
            if (i > 0)
            {
                Boolean boolean = default(Boolean);
                if (!rangeFirst)
                {
                    if (isTrue)
                        boolean.m_value = true;
                    else
                        boolean.m_value = false;
                    obj[0] = boolean.BoxIl2CppObject();

                    if (i == 2)
                    {
                        Int32 integer = default(Int32);
                        integer.m_value = Random.Range(rangeMin, rangeMax);
                        obj[1] = integer.BoxIl2CppObject();
                    }
                }
                else
                {
                    Int32 integer = default(Int32);
                    integer.m_value = Random.Range(rangeMin, rangeMax);
                    obj[0] = integer.BoxIl2CppObject();
                }
            }

            return obj;
        }

        //this.view.RPC("Hunting", PhotonTargets.All, new object[]
        //{
        //    false
        //});
        //this.view.RPC("SyncChasingPlayer", PhotonTargets.All, new object[]
        //{
        //    isChasing
        //});
        //this.view.RPC("MakeGhostAppear", PhotonTargets.All, new object[]
        //{
        //    true,
        //    Random.Range(0, isEvent ? 2 : 3)
        //});
        //this.view.RPC("UseNetworked", PhotonTargets.AllBuffered, new object[]
        //{
        //    false
        //});
        //this.view.RPC("SyncLockState", PhotonTargets.AllBuffered, new object[]
        //{
        //    this.locked
        //});
        //this.view.RPC("UnlockDoorTimer", PhotonTargets.AllBuffered, new object[]
        //{
        //    UnityEngine.Random.Range(5f, 20f)
        //});
        //this.view.RPC("NetworkedPlayLockSound", PhotonTargets.All, Array.Empty<object>());
    }
}