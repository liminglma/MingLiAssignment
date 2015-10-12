using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MingLiweek05
{
    class Level
    {
        public int minSpawnTime { get; set; }
        public int maxSpawnTime { get; set; }
        // Enemy count variables
        public int numberEnemies { get; set; }
        public float Speed { get; set; }
   
        // Misses
        public int missesAllowed { get; set; }
        public Level(int minSpawnTime, int maxSpawnTime,
        int numberEnemies, float Speed,
        int missesAllowed)
        {
            this.minSpawnTime = minSpawnTime;
            this.maxSpawnTime = maxSpawnTime;
            this.numberEnemies = numberEnemies;
            this.Speed = Speed;
            this.missesAllowed = missesAllowed;
        } 
    }
}
