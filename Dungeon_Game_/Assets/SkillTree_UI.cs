using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree_UI : MonoBehaviour {

private PlayerSkills playerSkills;

private void Awake(){
}

public void SetPlayerSkills(PlayerSkills playerSkills) {
    this.playerSkills = playerSkills;
}

public void UnlockSkill() {
    playerSkills.UnlockSkill(PlayerSkills.SkillType.BackStab);
}

}

