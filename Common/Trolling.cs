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
            MelonLogger.Log("[+] Troll->Hunt: Toggled ");
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
            //this.view.RPC("Hunting", PhotonTargets.All, new object[]
            //{
            //    true
            //});
            Main.photonView.RPC("Hunting", RpcTarget.All, getRPCObject(1, true));
            //this.view.RPC("SyncChasingPlayer", PhotonTargets.All, new object[]
            //{
            //    isChasing
            //});
            Main.photonView.RPC("SyncChasingPlayer", RpcTarget.All, getRPCObject(1, true));
        }
        public static void Idle()
        {
            MelonLogger.Log("[+] Troll->Idle: Toggled ");
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
            //this.view.RPC("Hunting", PhotonTargets.All, new object[]
            //{
            //    false
            //});
            Main.photonView.RPC("Hunting", RpcTarget.All, getRPCObject(1, false));
            //this.view.RPC("SyncChasingPlayer", PhotonTargets.All, new object[]
            //{
            //    isChasing
            //});
            Main.photonView.RPC("SyncChasingPlayer", RpcTarget.All, getRPCObject(1, false));
        }
        public static void Appear()
        {
            MelonLogger.Log("[+] Troll->Appear: Toggled ");
            //this.view.RPC("MakeGhostAppear", PhotonTargets.All, new object[]
            //{
            //    true,
            //    Random.Range(0, isEvent ? 2 : 3)
            //});
            Main.photonView.RPC("MakeGhostAppear", RpcTarget.All, getRPCObject(2, true));
        }
        public static void UnAppear()
        {
            MelonLogger.Log("[+] Troll->UnAppear: Toggled ");
            //this.view.RPC("MakeGhostAppear", PhotonTargets.All, new object[]
            //{
            //    false,
            //    Random.Range(0, isEvent ? 2 : 3)
            //});
            Main.photonView.RPC("MakeGhostAppear", RpcTarget.All, getRPCObject(2, false));
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

        private static Object[] getRPCObject(int i, bool isTrue)
        {
            Debug.Out("getRPCObject");
            Object[] obj = new Object[i];
            Boolean boolean = default(Boolean);
            if (isTrue)
                boolean.m_value = true;
            obj[0] = boolean.BoxIl2CppObject();

            if (i == 2)
            {
                Int32 integer = default(Int32);
                integer.m_value = Random.Range(0, 3);
                obj[1] = integer.BoxIl2CppObject();
            }

            return obj;
        }
    }
}