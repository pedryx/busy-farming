using TestGame;
using TestGame.Scenes;


var game = new LDGame();
game.Run<LevelScene>();

/*
idea:

Players start with a small farm and a limited number of seeds. They must plant and
harvest crops, such as apples, carrots, and wheat, and sell them at the market to
earn money. Players can use the money they earn to buy more seeds, upgrade their farm
equipment, and expand their farm.

As they progress through the game, players will need to manage their time carefully to
ensure that their crops are tended to and harvested before they wilt. They may also need
to deal with challenges such as pests, drought, and unpredictable weather.


changing market
inventory:
 - seeds
 - things from plants

debufs:
 - pests: player need to spend some time to remove it otherwise it will grow larger and
          it will occupy one tile
 - drought: more watering time
 - rain: less watering time

money spending:
 - buying seeds
 - upgrading shop so he can buy better seeds
 - expanding farm
 - expanding inventory
 - upgrades:
   - planting speed
   - harvesting speed
   - watering speed
   - larger safe duration

each plat has grow duration if not collected in <safe duration> days after growen up then
plant dies

(special plants which helps with watering adject plants)

 */