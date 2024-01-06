using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AvatarHolder : MonoBehaviour
{
    public Avatar[] newAvatars;
    public Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        int selectedAvatar = PlayerPrefs.GetInt("selectedCharacter");
        anim.avatar = newAvatars[selectedAvatar];
    }
}
