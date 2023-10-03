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
        BowLayer = 5
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
        SceneLoading,
        SceneMainMenu,
        SceneGame,
        SceneEnd,
        SceneFailed,
        SceneHowToPlay
    }


    //Sounds Enums
    public enum Sound
    {
        Bgm,
        Effect,
        EnumCount
    }

    public enum BGM { 
        BGM,
        BGMCount
    }

    public enum SFX 
    { 
        Button,
        Clear,
        Money,
        Opening,
        TypeWriting,
        SFXCount
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


