public class Enums 
{
    // Enum 저장소
    public enum CharacterState
    {
        //공용 state
        None,
        Die,
        Idle,
        Move,
        Run,
        Runaway,
        TakeDamage,
        Attack_1,
        Attack_2,
        Attack_3,

        Dodge,
        Attack_Jump,
        Attack_Run,
        Interaction,
        Climbing,
        Crouch
    }

    
    public enum AttackType
    {
        AD,
        AP,
        Fixed
    }

    public enum ItemType { 
        None = 0,
        Weapon = 1,
        Armor = 2,
        Consumable = 3,
        Useable = 4
    }
    
    public enum WeaponType
    {
        None = 0,
        ToeNail = 1,
        Sword = 2
    }
    public enum ArmorType
    {
        None = 0,
        Helmet = 1,
        Armor = 2,
        Boots = 3,
    }
    public enum ConsumableType
    {
        None = 0 ,
        Potion = 1,
        Food = 2
    }

    public enum UseableType
    {
        None = 0,
        Scroll = 1,
        ThrowingWeapon = 2,
        Crops = 3
    }

    public enum BuffType
    {
        None = 0
    }

    public enum ObjectType
    {
        Unknown,
        Player,
        Interactable,
        Enemy,
    }
    public enum AnimationLayer
    {
        IdleLayer = 0,
        WalkLayer = 1,
        AttackLayer = 2,
        RunLayer = 3,
        HitDamageLayer = 4,
        InterActionLayer = 5
    }
    public enum Layer
    {
        InterActive,
        UI,

    }

    public enum Key
    {
        UP,
        DOWN,
        RIGHT,
        LEFT,
        RUN,
        CROUCH,
        INTERACTION,
        ACTION_1,
        ACTION_2,
        ACTION_3,
        ACTION_4
    }


    public enum UI
    {
        UI_Loading,
        UI_Login,
        UI_MainMenu,
        UI_Game
    }
    public enum Scene
    {
        Unknown,
        SceneLoading,
        SceneLogin,
        SceneMainMenu,
        SceneGame
    }
    //Sounds Enums
    public enum Sound
    {
        Bgm,
        Effect,
        // EnumCount는 모든 Enum 형식에 마지막에 추가 될 요소로 enum 안의 요소의 총 개수를 파악하는 데 도움을 준다.
        EnumCount
    }

    public enum UIEvent
    {
        Click,
        BeginDrag,
        EndDrag,
        Drag,
    }

    public enum MouseEvent 
    {
        Press,
        PointerDown,
        PointerUp,
        Click
    }

    public enum CameraMode
    {
        QuaterView
    }

}


