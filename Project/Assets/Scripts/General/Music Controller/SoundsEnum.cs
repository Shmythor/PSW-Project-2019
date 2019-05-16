using UnityEngine;
using System.Collections;

public static class SoundsEnum 
{
    public enum soundEffect {
        greedy_hurt1, greedy_hurt2, greedy_hurt3, greedy_hurtClassic, greedy_death, greedy_eat1, greedy_eat2, greedy_eat3,
        ui_heartFill, ui_heartLoose, ui_damageRestored,
        fireworks_launch, fireworks_explosion,
        
    };
    public enum soundTrack {
        main_mainSong,
        menu_mainMenu, menu_pause
    };
}
