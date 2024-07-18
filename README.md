slimy_runner

In this exhilarating runner game, players dash through four uniquely themed environments: silent cottages with street lights, the serene forest filled with lush greenery and sheep, the treacherous desert with its scorching sun and sandy mountains, and the icy tundra featuring frosty paths.

Each environment offers distinct challenges and obstacles, ensuring a dynamic and engaging experience as players race to achieve the highest scores and unlock new levels.

Mechanics
1. Movement and Speed Adjustment
Base Speed and Speed Modifier: The player's movement speed is determined by a base speed (8 units) plus a speed modifier. The speed modifier can be adjusted dynamically during gameplay.

Checkpoint Speed Adjustments: The player's speed increases as they pass certain checkpoints:
- Start Position: 8 units
- Checkpoint 1: 9.5 units
- Checkpoint 2: 10 units
- Checkpoint 3: 10.5 units
  
Speed Reduction on Collision: The player's speed decreases by 1 unit when colliding with obstacles, but it cannot go below a minimum speed (e.g., 4 units).

3. Jump Mechanics
Ground Check: Uses raycasting to determine if the player is grounded.
Jump Buffer: Allows jumping even if the jump button was pressed slightly before the player landed.
Multiple Jumps: Players can perform additional jumps (e.g., double jumps) while in the air.

4. Collision Handling
Obstacle Collisions: Reduces the player's speed and plays specific sound effects depending on the type of obstacle.
Platform Collisions: Increases the player's speed temporarily.
Enemy Collisions: Ends the game and plays a specific sound effect.

5. Saving and Loading Progress
PlayerPrefs: Stores the player's checkpoint, total distance traveled, number of takes, and speed modifier for persistence between sessions.

6. Game Over and Restart
Game Over Handling: Triggers when the player loses all lives or hits a wall. Displays game over UI and stops movement.
Restarting: Resets the player's position and speed, allowing them to continue from the last checkpoint or start from the beginning.

Enemy Summary
1. Player Detection and Following
Line of Sight: The enemy detects the player if the player is within a specified distance (lineOfSight).
Follow Range: The enemy will follow the player if the player's x-position is between startFollow and endFollow.
Flying Height: The enemy maintains a specific height (flyingHeight) while moving.

2. Movement
Speed: The enemy moves towards the player at a base speed, which can be increased temporarily.
Acceleration: The enemy's movement is smooth due to acceleration over a specified time (accelerationTime).
Ground Check: Ensures that the enemy is grounded using raycasting.

3. Attacking
Attack Zone: If the player enters the attack zone, the enemy initiates an attack.
Attack Animation: Triggers the attacking animation when the enemy starts attacking.
Attack Time: Controls the duration of the attack.

4. Collisions
Obstacle Collisions: The enemy temporarily disables the collider of any obstacle it collides with and re-enables it after a delay.

5. Audio
Sound Effects: Plays a sound when the enemy starts following the player and stops when the player leaves the follow range.

![WhatsApp Image 2024-05-29 at 12 10 07 AM](https://github.com/muthee-p/slimy_runner/assets/117924809/259c192c-3520-40f2-ab80-49118534f790)
![WhatsApp Image 2024-05-27 at 11 30 33 PM](https://github.com/muthee-p/slimy_runner/assets/117924809/6fe73a4a-6ac6-4ba5-8c1b-c48796200ea9)
![WhatsApp Image 2024-05-26 at 6 19 22 PM](https://github.com/muthee-p/slimy_runner/assets/117924809/290d2cee-1b08-4d98-8ccd-53b37f17d3fd)

Itch.io Analytics as at 18/07/2024
the Final version was published on 19th June 2024

![image](https://github.com/user-attachments/assets/8e70e9e1-c47c-4006-a8ac-6bdd2ad2cb01)
![image](https://github.com/user-attachments/assets/5c2b0cfe-448c-424e-8f64-a10766eb818d)



Asset Credits

Models
The four enviroments: https://craftpix.net/freebies/filter/3d-game-assets/page/2/

"Low poly rabbit" (https://skfb.ly/o7WyS) by Tin3D is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).

"Low Poly Street Light" (https://skfb.ly/oJHLU) by Fridqeir is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).

"Low poly Golf Flag Animated" (https://skfb.ly/6WZzq) by prorookie123 is licensed under Creative Commons Attribution (http://creativecommons.org/licenses/by/4.0/).

Sound and music:

Music by <a href="https://pixabay.com/users/calvinclavier-16027823/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=201837">Calvin Clavier</a> from <a href="https://pixabay.com/music//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=201837">Pixabay</a>

music: Music by <a href="https://pixabay.com/users/junipersona-35113928/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=153030">junipersona</a> from <a href="https://pixabay.com/music//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=153030">Pixabay</a>

FIREWORKS: Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=24781">Pixabay</a>

SQUEKY SOUND- Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=7044">Pixabay</a>

Cute Squeal = Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=39519">Pixabay</a>

woohoo- Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=82843">Pixabay</a>

Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=45622">Pixabay</a>

Sound Effect by <a href="https://pixabay.com/users/universfield-28281460/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=122256">UNIVERSFIELD</a> from <a href="https://pixabay.com/sound-effects//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=122256">Pixabay</a>

Sound Effect by <a href="https://pixabay.com/users/daviddumaisaudio-41768500/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=195717">David Dumais</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=195717">Pixabay</a>

Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=47639">Pixabay</a>

Sound Effect from <a href="https://pixabay.com/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=90736">Pixabay</a>

Sound Effect by <a href="https://pixabay.com/users/daviddumaisaudio-41768500/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=194036">David Dumais</a> from <a href="https://pixabay.com/sound-effects//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=194036">Pixabay</a>

Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=68694">Pixabay</a>

Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=14584">Pixabay</a>

Sound Effect by <a href="https://pixabay.com/users/edr-1177074/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=9203">EdR</a> from <a href="https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=9203">Pixabay</a>

Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=88478">Pixabay</a>

Sound Effect from <a href="https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=83483">Pixabay</a>


