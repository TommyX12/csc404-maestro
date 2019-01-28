# Maestro

An RPG game in which the player must utilize music, rhythm, and composition to defeat enemies and solve puzzles.

## Design Blog Jan 13th - Jan 27th, 2019

##### The Idea

Being the first week that our team started working on Maestro, things were very hectic. At the game jam we wanted to create a jrpg where you attack by doing guitar hero riffs. We prototyped out note hitting and detection at the game jam but found the system lacking. It felt like music was just tacked on as an afterthought, it wasn't _integrated_ properly.

After some soul searching and ideation we came up with a new and better idea before our first team meeting which happened sometime in the middle of that week (I think). Our game idea is heavily inspired off of existing rhythm games like 140 and Crypt of the Necrodancer. In these games the music isn't just an add in that plays the background, it wouldn't be wrong to say that the game would not function without the music, or that they are inseperable. We strove to create a game like this, one where the music and the game were intertwined with eachother.

We decided to create a top down room-clearing action game much in the spirit of Binding of Isaac and Enter the Gungeon. We were really inspired by how 140 uses it's game objects and mechanics to _create_ music. So we wanted to make a game were the music was driven by the gameplay, as opposed to the other way around as in Crypt of the Necrodancer. To this end our game makes use of several mechanics

- The actions of all game objects are to be constrained to happen at rhythmic intervals
- The input that the player gives for each action must be given in time with a predetermined rhythm

So you get things like enemies moving to a rhythm, creating a beat. Or moving walls shifting every downbeat. And the player must tap out a rhythm on his/her controller in order to perform actions. If we wrap this around a background beat for each level and the general paradigm of topdown action shooters then we have a symphony of different game objects all acting in harmony. Or at least that's the goal.

`To create music by interacting with the world`

It would not be wrong to say that this is the core theme behind our game.

---

##### The Pitch

We had our first meeting where we divvied up tasks, talked about and refined ideas, as well as came up with a general look and feel goal for our game expressed as a mood-board. During this time we decided what we would present for the pitch. We needed to make sure that the audience really understood the core concept behind our game.

We decided that three of us would be presenting the pitch, Ray, Thommy and Kristan. Thommy would go first, presenting the mechanics and overall idea behind the game, Kristan would then go over the look and feel of the game, and Ray would go over an example of music and gameplay integration.

We were all really nervous when we went up to present but the delivery of the pitch was excellent. One of the judges said he liked it best.

We also got feedback, criticisms and ideas that we hadn't considered before.

- Potentially a lot going on at once, keeping track of enemy movement/bullets, your own character's movements, as well as item rhythms. Could overwhelm the player.
    - To this end we decided that we would remove the need for players to aim their weapons. And we are also considering removing player movement entirely and going for a tower-defense or on rails concept but we are still undecided.
    - The main issue is that the player might find it very difficult to move along numerous axis as they play music as they fight enemies, etc. Too much to deal with all at once. 
- Large amounts of enemies could drown out the player's sounds. The music could end up being almost entirely enemy-based, while player sounds are just background noise.
    - This is something to be considered heavily in sound design. We should make the player actions iconic, the melody, or the main theme of each song. Failing that a system that ensures that the player's music is the most important.
    - The judge's concern was that control is in the hands of the enemy, not the player. If the enemy or the computer-controlled side is mostly in control of the melody, this means that they dictate how things're going to go, not the player.
- On that note, too many enemies at once would likely make the game too loud, also could result in a blurb of noise rather than smooth, coordinated music.
    - We need a system that ensures volume is kept at a respectable level, and game objects create sound harmoniously.
- Consider looking at the game "Disco is Dead", a previous Level Up winner.
    - Considered
- Keep in mind the idea of using peripheral devices (notably a guitar hero/rock band guitar).
    - We like this idea but we would have to redisgn a large part of our game, we are still undecided. 
- The Level Up convention is likely going to be very loud - think of ways to tackle this (Maybe bring noise-canceling headphones?)
    - Ray will borrow his mom's noise cancelling headphones. (Thanks mom)
- Try to stay consistent with a theme. It won't look good if you have this really vibrant game playing rock music based in a sci-fi age, or some other strange combination.
    - Music designers need to be coordinated with artists
    - They liked the aesthetic but they believe we need to "tighten" it more.
- They reminded us that gameplay can change at the prototype. We may discover that some things work better or don't work at all at the prototype stage.

Keeping these things in mind we are eager and excited to build the next part of our game. We hope to get something minimally playable by Tuesday January 29th.
